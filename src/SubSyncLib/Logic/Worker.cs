﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Rar;
using SubSyncLib.Logic.Exceptions;

namespace SubSyncLib.Logic
{
    public class Worker : IWorker
    {
        private readonly VideoFile video;
        private readonly ILogger logger;
        private readonly IWorkerQueue workerQueue;
        private readonly ISubtitleProvider subtitleProvider;
        private readonly IStatusReporter<WorkerStatus> statusReporter;
        private readonly HashSet<string> subtitleExtensions;
        private readonly int retryCount;

        private static readonly HashSet<string> FileCompressionExtensions = new HashSet<string>
        {
            ".zip", ".rar", ".gzip", ".gz", ".7z", ".tar", ".tar.gz"
        };

        private TaskCompletionSource<object> taskCompletionSource = null;

        public Worker(
            VideoFile video,
            ILogger logger,
            IWorkerQueue workerQueue,
            ISubtitleProvider subtitleProvider,
            IStatusReporter<WorkerStatus> statusReporter,
            HashSet<string> subtitleExtensions,
            int retryCount = 0)
        {
            this.video = video;
            this.logger = logger;
            this.workerQueue = workerQueue;
            this.subtitleProvider = subtitleProvider;
            this.statusReporter = statusReporter;
            this.subtitleExtensions = subtitleExtensions;
            this.retryCount = retryCount;
        }

        public Task SyncAsync()
        {
            if (taskCompletionSource != null)
            {
                return taskCompletionSource.Task;
            }

            taskCompletionSource = new TaskCompletionSource<object>();

            try
            {
                return taskCompletionSource.Task;
            }
            finally
            {
                Task.Factory.StartNew(async () =>
                {
                    if (retryCount > 0)
                    {
                        await Task.Delay(retryCount * 1000);
                    }

                    //var file = new FileInfo(filePath);
                    logger.WriteLine($"Synchronizing {video.Name}");
                    try
                    {
                        var outputName = await subtitleProvider.GetAsync(video);
                        var extension = Path.GetExtension(outputName);
                        if (IsCompressed(extension))
                        {
                            outputName = await DecompressAsync(outputName);
                        }

                        string nameWithoutExt = Path.GetFileNameWithoutExtension(video.Name);
                        var finalName = await RenameAsync(outputName, nameWithoutExt);
                        logger.WriteLine(
                            $"@gray@Subtitle @white@{Path.GetFileName(finalName)} @green@downloaded!");
                        statusReporter.Report(new WorkerStatus(true, video));
                        taskCompletionSource.SetResult(true);
                    }
                    catch (NestedArchiveNotSupportedException nexc)
                    {
                        logger.Error($"Synchronization of {video.Name} failed with: {nexc.Message}");
                        statusReporter.Report(new WorkerStatus(false, video));
                        taskCompletionSource.SetException(nexc);
                    }
                    catch (Exception exc)
                    {
                        logger.Error($"Synchronization of {video.Name} failed with: {exc.Message}");
                        if (!workerQueue.Enqueue(video)) // (this);
                        {
                            statusReporter.Report(new WorkerStatus(false, video));
                        }
                        taskCompletionSource.SetException(exc);
                    }

                }, TaskCreationOptions.LongRunning);

            }
        }

        public void Dispose() { }

        private async Task<string> RenameAsync(string fileToRename, string newFilaNameWithoutExtension, bool retried = false)
        {
            var inFile = new FileInfo(fileToRename);
            var directory = inFile.Directory?.FullName ?? "./";
            var destFileName = Path.Combine(directory, newFilaNameWithoutExtension + Path.GetExtension(fileToRename));

            if (fileToRename == destFileName)
            {
                //it's the same file, do not do anything
                return destFileName;
            }

            if (!inFile.Exists)
            {
                return null;
            }
            if (System.IO.File.Exists(destFileName))
            {
                System.IO.File.Delete(destFileName);
            }

            try
            {
                inFile.MoveTo(destFileName);
            }
            catch (FileNotFoundException exc)
            {
                // r/quityourbullshit
                // file exists, problem is that we accessed it to quickly.
                if (retried)
                {
                    return null;
                }
                await Task.Delay(100);
                return await RenameAsync(fileToRename, newFilaNameWithoutExtension, true);
            }

            return destFileName;
        }

        private Task<string> DecompressAsync(string filename)
        {
            switch (Path.GetExtension(filename)?.ToLower())
            {
                case ".rar": return DecompressRarAsync(filename);
                case ".zip": return DecompressZipAsync(filename);
                case ".7z": return Decompress7ZipAsync(filename);
                case ".tar": return DecompressTarAsync(filename);
                case ".gz":
                case ".gzip":
                    return DecompressGZipAsync(filename);

                default:
                    throw new NotImplementedException($"The archive extension: {Path.GetExtension(filename)?.ToLower()} has not yet been implemented.");
            }
        }

        private async Task<string> DecompressArchive(string filename, Func<Stream, IArchive> archiveOpener)
        {
            var file = new FileInfo(filename);
            var targetFile = string.Empty;
            try
            {
                var fileDirectory = file.Directory;
                var directory = fileDirectory?.FullName ?? "./";
                using (var fileReader = file.OpenRead())
                using (var reader = archiveOpener(fileReader))
                {
                    if (reader.IsSolid)
                    {
                        var entryReader = reader.ExtractAllEntries();
                        while (entryReader.MoveToNextEntry())
                        {
                            var result = await UnpackSubtitleEntryAsync(entryReader, entryReader.Entry, directory);
                            if (result.SubtitleFound)
                            {
                                targetFile = result.Filename;
                                return result.Filename;
                            }
                        }
                    }
                    else
                    {
                        foreach (var entry in reader.Entries)
                        {
                            var result = await UnpackSubtitleEntryAsync(entry, directory);
                            if (result.SubtitleFound)
                            {
                                targetFile = result.Filename;
                                return result.Filename;
                            }
                        }
                    }
                    // TODO: This need to match the subtitles to determine whether its for the right video or not.
                    // check if any of the entries are archives and unpack it if one exists.
                    var archive = reader.Entries.FirstOrDefault(x => IsCompressed(Path.GetExtension(x.Key)));
                    if (archive != null)
                    {
                        logger.WriteLine($"@yel@Warning: Nested archive found inside '{filename}', output subtitle may not be correct!");
                        var result = await UnpackEntryAsync(archive, directory);
                        targetFile = result.Filename;
                        return await DecompressAsync(result.Filename);
                        //throw new NestedArchiveNotSupportedException(filename);
                    }
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(targetFile))
                {
                    file.Delete();
                }
            }

            throw new FileNotFoundException($"No suitable subtitle found in the downloaded archive, {filename}. Archive kept just in case.");
        }

        private async Task<EntryUnpackResult> UnpackSubtitleEntryAsync(IReader reader, IEntry entry, string directory)
        {
            if (!TestEntryAsSubtitle(entry, out var unpackSubtitleEntryAsync)) return unpackSubtitleEntryAsync;

            return await UnpackEntryAsync(reader, entry, directory);
        }

        private async Task<EntryUnpackResult> UnpackSubtitleEntryAsync(IArchiveEntry entry, string directory)
        {
            if (!TestEntryAsSubtitle(entry, out var unpackSubtitleEntryAsync)) return unpackSubtitleEntryAsync;

            return await UnpackEntryAsync(entry, directory);
        }

        private bool TestEntryAsSubtitle(IEntry entry, out EntryUnpackResult unpackSubtitleEntryAsync)
        {
            unpackSubtitleEntryAsync =
                new EntryUnpackResult(subtitleFound: false, filename: null, entry: entry.Key);

            var ext = Path.GetExtension(entry.Key);
            if (entry.Key.ToLower().EndsWith(".srt.txt"))
            {
                ext = ".srt";
            }

            var subtitleFound = subtitleExtensions.Contains(ext?.ToLower());
            if (!subtitleFound)
            {
                return false;
            }

            return true;
        }

        private Task<EntryUnpackResult> UnpackEntryAsync(IReader reader, IEntry entry, string directory)
        {
            return this.UnpackEntryAsync(reader.OpenEntryStream, entry, directory);
        }

        private Task<EntryUnpackResult> UnpackEntryAsync(IArchiveEntry entry, string directory)
        {
            return this.UnpackEntryAsync(entry.OpenEntryStream, entry, directory);
        }

        private async Task<EntryUnpackResult> UnpackEntryAsync(Func<Stream> entryStreamProvider, IEntry entry, string directory)
        {
            var ext = Path.GetExtension(entry.Key);
            if (entry.Key.ToLower().EndsWith(".srt.txt")) ext = ".srt";
            var subtitleFound = subtitleExtensions.Contains(ext?.ToLower());
            var targetFile = Path.Combine(directory, Path.ChangeExtension(entry.Key.Replace("?", ""), ext));
            var dir = new FileInfo(targetFile).Directory;
            if (dir != null && !dir.Exists)
            {
                dir.Create();
            }

            var targetFileInfo = new FileInfo(targetFile);
            using (var entryStream = entryStreamProvider())
            using (var sw = targetFileInfo.Create())//new FileStream(targetFile, FileMode.Create))
            {
                var read = 0;
                var buffer = new byte[4096];
                while ((read = await entryStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    await sw.WriteAsync(buffer, 0, read);
                }

                return new EntryUnpackResult(subtitleFound: subtitleFound, filename: targetFile, entry: entry.Key);
            }
        }

        private Task<string> DecompressRarAsync(string filename) => DecompressArchive(filename, x => SharpCompress.Archives.Rar.RarArchive.Open(x));
        private Task<string> DecompressZipAsync(string filename) => DecompressArchive(filename, x => SharpCompress.Archives.Zip.ZipArchive.Open(x));
        private Task<string> DecompressGZipAsync(string filename) => DecompressArchive(filename, x => SharpCompress.Archives.GZip.GZipArchive.Open(x));
        private Task<string> Decompress7ZipAsync(string filename) => DecompressArchive(filename, x => SharpCompress.Archives.SevenZip.SevenZipArchive.Open(x));
        private Task<string> DecompressTarAsync(string filename) => DecompressArchive(filename, x => SharpCompress.Archives.Tar.TarArchive.Open(x));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsCompressed(string extension) => FileCompressionExtensions.Contains(extension.ToLower());

        private struct EntryUnpackResult
        {
            public readonly bool SubtitleFound;
            public readonly string Filename;
            public readonly string Entry;

            public EntryUnpackResult(bool subtitleFound, string filename, string entry)
            {
                SubtitleFound = subtitleFound;
                Filename = filename;
                Entry = entry;
            }
        }
    }
}