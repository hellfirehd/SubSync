﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SubSync
{
    internal class SubtitleLanguage
    {
        public string LanguageId { get; }
        public string Iso639 { get; }
        public string Name { get; }

        public SubtitleLanguage(string languageId, string iso639, string name)
        {
            LanguageId = languageId;
            Iso639 = iso639;
            Name = name;
        }

        public static SubtitleLanguage Find(string nameIsoOrId)
        {
            var possibleMatches = new List<SubtitleLanguage>();

            foreach (var item in All)
            {
                if (item.LanguageId.Equals(nameIsoOrId, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }

                if (!string.IsNullOrEmpty(item.Iso639) && item.Iso639.Equals(nameIsoOrId, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }

                if (item.Name.IndexOf(nameIsoOrId, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (item.Name.Equals(nameIsoOrId, StringComparison.OrdinalIgnoreCase))
                    {
                        return item;
                    }

                    possibleMatches.Add(item);
                }
            }

            if (possibleMatches.Count == 1)
            {
                return possibleMatches[0];
            }

            return possibleMatches
                .OrderByDescending(x => x.LanguageId)
                .FirstOrDefault();
        }

        public static SubtitleLanguage[] All = {
            new SubtitleLanguage("aar","aa","Afar, afar"),
            new SubtitleLanguage("abk","ab","Abkhazian"),
            new SubtitleLanguage("ace","","Achinese"),
            new SubtitleLanguage("ach","","Acoli"),
            new SubtitleLanguage("ada","","Adangme"),
            new SubtitleLanguage("ady","","adyghé"),
            new SubtitleLanguage("afa","","Afro-Asiatic (Other)"),
            new SubtitleLanguage("afh","","Afrihili"),
            new SubtitleLanguage("afr","af","Afrikaans"),
            new SubtitleLanguage("ain","","Ainu"),
            new SubtitleLanguage("aka","ak","Akan"),
            new SubtitleLanguage("akk","","Akkadian"),
            new SubtitleLanguage("alb","sq","Albanian"),
            new SubtitleLanguage("ale","","Aleut"),
            new SubtitleLanguage("alg","","Algonquian languages"),
            new SubtitleLanguage("alt","","Southern Altai"),
            new SubtitleLanguage("amh","am","Amharic"),
            new SubtitleLanguage("ang","","English, Old (ca.450-1100)"),
            new SubtitleLanguage("apa","","Apache languages"),
            new SubtitleLanguage("ara","ar","Arabic"),
            new SubtitleLanguage("arc","","Aramaic"),
            new SubtitleLanguage("arg","an","Aragonese"),
            new SubtitleLanguage("arm","hy","Armenian"),
            new SubtitleLanguage("arn","","Araucanian"),
            new SubtitleLanguage("arp","","Arapaho"),
            new SubtitleLanguage("art","","Artificial (Other)"),
            new SubtitleLanguage("arw","","Arawak"),
            new SubtitleLanguage("asm","as","Assamese"),
            new SubtitleLanguage("ast","at","Asturian"),
            new SubtitleLanguage("ath","","Athapascan languages"),
            new SubtitleLanguage("aus","","Australian languages"),
            new SubtitleLanguage("ava","av","Avaric"),
            new SubtitleLanguage("ave","ae","Avestan"),
            new SubtitleLanguage("awa","","Awadhi"),
            new SubtitleLanguage("aym","ay","Aymara"),
            new SubtitleLanguage("aze","az","Azerbaijani"),
            new SubtitleLanguage("bad","","Banda"),
            new SubtitleLanguage("bai","","Bamileke languages"),
            new SubtitleLanguage("bak","ba","Bashkir"),
            new SubtitleLanguage("bal","","Baluchi"),
            new SubtitleLanguage("bam","bm","Bambara"),
            new SubtitleLanguage("ban","","Balinese"),
            new SubtitleLanguage("baq","eu","Basque"),
            new SubtitleLanguage("bas","","Basa"),
            new SubtitleLanguage("bat","","Baltic (Other)"),
            new SubtitleLanguage("bej","","Beja"),
            new SubtitleLanguage("bel","be","Belarusian"),
            new SubtitleLanguage("bem","","Bemba"),
            new SubtitleLanguage("ben","bn","Bengali"),
            new SubtitleLanguage("ber","","Berber (Other)"),
            new SubtitleLanguage("bho","","Bhojpuri"),
            new SubtitleLanguage("bih","bh","Bihari"),
            new SubtitleLanguage("bik","","Bikol"),
            new SubtitleLanguage("bin","","Bini"),
            new SubtitleLanguage("bis","bi","Bislama"),
            new SubtitleLanguage("bla","","Siksika"),
            new SubtitleLanguage("bnt","","Bantu (Other)"),
            new SubtitleLanguage("bos","bs","Bosnian"),
            new SubtitleLanguage("bra","","Braj"),
            new SubtitleLanguage("bre","br","Breton"),
            new SubtitleLanguage("btk","","Batak (Indonesia)"),
            new SubtitleLanguage("bua","","Buriat"),
            new SubtitleLanguage("bug","","Buginese"),
            new SubtitleLanguage("bul","bg","Bulgarian"),
            new SubtitleLanguage("bur","my","Burmese"),
            new SubtitleLanguage("byn","","Blin"),
            new SubtitleLanguage("cad","","Caddo"),
            new SubtitleLanguage("cai","","Central American Indian (Other)"),
            new SubtitleLanguage("car","","Carib"),
            new SubtitleLanguage("cat","ca","Catalan"),
            new SubtitleLanguage("cau","","Caucasian (Other)"),
            new SubtitleLanguage("ceb","","Cebuano"),
            new SubtitleLanguage("cel","","Celtic (Other)"),
            new SubtitleLanguage("cha","ch","Chamorro"),
            new SubtitleLanguage("chb","","Chibcha"),
            new SubtitleLanguage("che","ce","Chechen"),
            new SubtitleLanguage("chg","","Chagatai"),
            new SubtitleLanguage("chi","zh","Chinese (simplified)"),
            new SubtitleLanguage("chk","","Chuukese"),
            new SubtitleLanguage("chm","","Mari"),
            new SubtitleLanguage("chn","","Chinook jargon"),
            new SubtitleLanguage("cho","","Choctaw"),
            new SubtitleLanguage("chp","","Chipewyan"),
            new SubtitleLanguage("chr","","Cherokee"),
            new SubtitleLanguage("chu","cu","Church Slavic"),
            new SubtitleLanguage("chv","cv","Chuvash"),
            new SubtitleLanguage("chy","","Cheyenne"),
            new SubtitleLanguage("cmc","","Chamic languages"),
            new SubtitleLanguage("cop","","Coptic"),
            new SubtitleLanguage("cor","kw","Cornish"),
            new SubtitleLanguage("cos","co","Corsican"),
            new SubtitleLanguage("cpe","","Creoles and pidgins, English based (Other)"),
            new SubtitleLanguage("cpf","","Creoles and pidgins, French-based (Other)"),
            new SubtitleLanguage("cpp","","Creoles and pidgins, Portuguese-based (Other)"),
            new SubtitleLanguage("cre","cr","Cree"),
            new SubtitleLanguage("crh","","Crimean Tatar"),
            new SubtitleLanguage("crp","","Creoles and pidgins (Other)"),
            new SubtitleLanguage("csb","","Kashubian"),
            new SubtitleLanguage("cus","","Cushitic (Other)' couchitiques, autres langues"),
            new SubtitleLanguage("cze","cs","Czech"),
            new SubtitleLanguage("dak","","Dakota"),
            new SubtitleLanguage("dan","da","Danish"),
            new SubtitleLanguage("dar","","Dargwa"),
            new SubtitleLanguage("day","","Dayak"),
            new SubtitleLanguage("del","","Delaware"),
            new SubtitleLanguage("den","","Slave (Athapascan)"),
            new SubtitleLanguage("dgr","","Dogrib"),
            new SubtitleLanguage("din","","Dinka"),
            new SubtitleLanguage("div","dv","Divehi"),
            new SubtitleLanguage("doi","","Dogri"),
            new SubtitleLanguage("dra","","Dravidian (Other)"),
            new SubtitleLanguage("dua","","Duala"),
            new SubtitleLanguage("dum","","Dutch, Middle (ca.1050-1350)"),
            new SubtitleLanguage("dut","nl","Dutch"),
            new SubtitleLanguage("dyu","","Dyula"),
            new SubtitleLanguage("dzo","dz","Dzongkha"),
            new SubtitleLanguage("efi","","Efik"),
            new SubtitleLanguage("egy","","Egyptian (Ancient)"),
            new SubtitleLanguage("eka","","Ekajuk"),
            new SubtitleLanguage("elx","","Elamite"),
            new SubtitleLanguage("eng","en","English"),
            new SubtitleLanguage("enm","","English, Middle (1100-1500)"),
            new SubtitleLanguage("epo","eo","Esperanto"),
            new SubtitleLanguage("est","et","Estonian"),
            new SubtitleLanguage("ewe","ee","Ewe"),
            new SubtitleLanguage("ewo","","Ewondo"),
            new SubtitleLanguage("fan","","Fang"),
            new SubtitleLanguage("fao","fo","Faroese"),
            new SubtitleLanguage("fat","","Fanti"),
            new SubtitleLanguage("fij","fj","Fijian"),
            new SubtitleLanguage("fil","","Filipino"),
            new SubtitleLanguage("fin","fi","Finnish"),
            new SubtitleLanguage("fiu","","Finno-Ugrian (Other)"),
            new SubtitleLanguage("fon","","Fon"),
            new SubtitleLanguage("fre","fr","French"),
            new SubtitleLanguage("frm","","French, Middle (ca.1400-1600)"),
            new SubtitleLanguage("fro","","French, Old (842-ca.1400)"),
            new SubtitleLanguage("fry","fy","Frisian"),
            new SubtitleLanguage("ful","ff","Fulah"),
            new SubtitleLanguage("fur","","Friulian"),
            new SubtitleLanguage("gaa","","Ga"),
            new SubtitleLanguage("gay","","Gayo"),
            new SubtitleLanguage("gba","","Gbaya"),
            new SubtitleLanguage("gem","","Germanic (Other)"),
            new SubtitleLanguage("geo","ka","Georgian"),
            new SubtitleLanguage("ger","de","German"),
            new SubtitleLanguage("gez","","Geez"),
            new SubtitleLanguage("gil","","Gilbertese"),
            new SubtitleLanguage("gla","gd","Gaelic"),
            new SubtitleLanguage("gle","ga","Irish"),
            new SubtitleLanguage("glg","gl","Galician"),
            new SubtitleLanguage("glv","gv","Manx"),
            new SubtitleLanguage("gmh","","German, Middle High (ca.1050-1500)"),
            new SubtitleLanguage("goh","","German, Old High (ca.750-1050)"),
            new SubtitleLanguage("gon","","Gondi"),
            new SubtitleLanguage("gor","","Gorontalo"),
            new SubtitleLanguage("got","","Gothic"),
            new SubtitleLanguage("grb","","Grebo"),
            new SubtitleLanguage("grc","","Greek, Ancient (to 1453)"),
            new SubtitleLanguage("ell","el","Greek"),
            new SubtitleLanguage("grn","gn","Guarani"),
            new SubtitleLanguage("guj","gu","Gujarati"),
            new SubtitleLanguage("gwi","","Gwich´in"),
            new SubtitleLanguage("hai","","Haida"),
            new SubtitleLanguage("hat","ht","Haitian"),
            new SubtitleLanguage("hau","ha","Hausa"),
            new SubtitleLanguage("haw","","Hawaiian"),
            new SubtitleLanguage("heb","he","Hebrew"),
            new SubtitleLanguage("her","hz","Herero"),
            new SubtitleLanguage("hil","","Hiligaynon"),
            new SubtitleLanguage("him","","Himachali"),
            new SubtitleLanguage("hin","hi","Hindi"),
            new SubtitleLanguage("hit","","Hittite"),
            new SubtitleLanguage("hmn","","Hmong"),
            new SubtitleLanguage("hmo","ho","Hiri Motu"),
            new SubtitleLanguage("hrv","hr","Croatian"),
            new SubtitleLanguage("hun","hu","Hungarian"),
            new SubtitleLanguage("hup","","Hupa"),
            new SubtitleLanguage("iba","","Iban"),
            new SubtitleLanguage("ibo","ig","Igbo"),
            new SubtitleLanguage("ice","is","Icelandic"),
            new SubtitleLanguage("ido","io","Ido"),
            new SubtitleLanguage("iii","ii","Sichuan Yi"),
            new SubtitleLanguage("ijo","","Ijo"),
            new SubtitleLanguage("iku","iu","Inuktitut"),
            new SubtitleLanguage("ile","ie","Interlingue"),
            new SubtitleLanguage("ilo","","Iloko"),
            new SubtitleLanguage("ina","ia","Interlingua (International Auxiliary Language Asso"),
            new SubtitleLanguage("inc","","Indic (Other)"),
            new SubtitleLanguage("ind","id","Indonesian"),
            new SubtitleLanguage("ine","","Indo-European (Other)"),
            new SubtitleLanguage("inh","","Ingush"),
            new SubtitleLanguage("ipk","ik","Inupiaq"),
            new SubtitleLanguage("ira","","Iranian (Other)"),
            new SubtitleLanguage("iro","","Iroquoian languages"),
            new SubtitleLanguage("ita","it","Italian"),
            new SubtitleLanguage("jav","jv","Javanese"),
            new SubtitleLanguage("jpn","ja","Japanese"),
            new SubtitleLanguage("jpr","","Judeo-Persian"),
            new SubtitleLanguage("jrb","","Judeo-Arabic"),
            new SubtitleLanguage("kaa","","Kara-Kalpak"),
            new SubtitleLanguage("kab","","Kabyle"),
            new SubtitleLanguage("kac","","Kachin"),
            new SubtitleLanguage("kal","kl","Kalaallisut"),
            new SubtitleLanguage("kam","","Kamba"),
            new SubtitleLanguage("kan","kn","Kannada"),
            new SubtitleLanguage("kar","","Karen"),
            new SubtitleLanguage("kas","ks","Kashmiri"),
            new SubtitleLanguage("kau","kr","Kanuri"),
            new SubtitleLanguage("kaw","","Kawi"),
            new SubtitleLanguage("kaz","kk","Kazakh"),
            new SubtitleLanguage("kbd","","Kabardian"),
            new SubtitleLanguage("kha","","Khasi"),
            new SubtitleLanguage("khi","","Khoisan (Other)"),
            new SubtitleLanguage("khm","km","Khmer"),
            new SubtitleLanguage("kho","","Khotanese"),
            new SubtitleLanguage("kik","ki","Kikuyu"),
            new SubtitleLanguage("kin","rw","Kinyarwanda"),
            new SubtitleLanguage("kir","ky","Kirghiz"),
            new SubtitleLanguage("kmb","","Kimbundu"),
            new SubtitleLanguage("kok","","Konkani"),
            new SubtitleLanguage("kom","kv","Komi"),
            new SubtitleLanguage("kon","kg","Kongo"),
            new SubtitleLanguage("kor","ko","Korean"),
            new SubtitleLanguage("kos","","Kosraean"),
            new SubtitleLanguage("kpe","","Kpelle"),
            new SubtitleLanguage("krc","","Karachay-Balkar"),
            new SubtitleLanguage("kro","","Kru"),
            new SubtitleLanguage("kru","","Kurukh"),
            new SubtitleLanguage("kua","kj","Kuanyama"),
            new SubtitleLanguage("kum","","Kumyk"),
            new SubtitleLanguage("kur","ku","Kurdish"),
            new SubtitleLanguage("kut","","Kutenai"),
            new SubtitleLanguage("lad","","Ladino"),
            new SubtitleLanguage("lah","","Lahnda"),
            new SubtitleLanguage("lam","","Lamba"),
            new SubtitleLanguage("lao","lo","Lao"),
            new SubtitleLanguage("lat","la","Latin"),
            new SubtitleLanguage("lav","lv","Latvian"),
            new SubtitleLanguage("lez","","Lezghian"),
            new SubtitleLanguage("lim","li","Limburgan"),
            new SubtitleLanguage("lin","ln","Lingala"),
            new SubtitleLanguage("lit","lt","Lithuanian"),
            new SubtitleLanguage("lol","","Mongo"),
            new SubtitleLanguage("loz","","Lozi"),
            new SubtitleLanguage("ltz","lb","Luxembourgish"),
            new SubtitleLanguage("lua","","Luba-Lulua"),
            new SubtitleLanguage("lub","lu","Luba-Katanga"),
            new SubtitleLanguage("lug","lg","Ganda"),
            new SubtitleLanguage("lui","","Luiseno"),
            new SubtitleLanguage("lun","","Lunda"),
            new SubtitleLanguage("luo","","Luo (Kenya and Tanzania)"),
            new SubtitleLanguage("lus","","lushai"),
            new SubtitleLanguage("mac","mk","Macedonian"),
            new SubtitleLanguage("mad","","Madurese"),
            new SubtitleLanguage("mag","","Magahi"),
            new SubtitleLanguage("mah","mh","Marshallese"),
            new SubtitleLanguage("mai","","Maithili"),
            new SubtitleLanguage("mak","","Makasar"),
            new SubtitleLanguage("mal","ml","Malayalam"),
            new SubtitleLanguage("man","","Mandingo"),
            new SubtitleLanguage("mao","mi","Maori"),
            new SubtitleLanguage("map","","Austronesian (Other)"),
            new SubtitleLanguage("mar","mr","Marathi"),
            new SubtitleLanguage("mas","","Masai"),
            new SubtitleLanguage("may","ms","Malay"),
            new SubtitleLanguage("mdf","","Moksha"),
            new SubtitleLanguage("mdr","","Mandar"),
            new SubtitleLanguage("men","","Mende"),
            new SubtitleLanguage("mga","","Irish, Middle (900-1200)"),
            new SubtitleLanguage("mic","","Mi'kmaq"),
            new SubtitleLanguage("min","","Minangkabau"),
            new SubtitleLanguage("mis","","Miscellaneous languages"),
            new SubtitleLanguage("mkh","","Mon-Khmer (Other)"),
            new SubtitleLanguage("mlg","mg","Malagasy"),
            new SubtitleLanguage("mlt","mt","Maltese"),
            new SubtitleLanguage("mnc","","Manchu"),
            new SubtitleLanguage("mni","ma","Manipuri"),
            new SubtitleLanguage("mno","","Manobo languages"),
            new SubtitleLanguage("moh","","Mohawk"),
            new SubtitleLanguage("mol","mo","Moldavian"),
            new SubtitleLanguage("mon","mn","Mongolian"),
            new SubtitleLanguage("mos","","Mossi"),
            new SubtitleLanguage("mwl","","Mirandese"),
            new SubtitleLanguage("mul","","Multiple languages"),
            new SubtitleLanguage("mun","","Munda languages"),
            new SubtitleLanguage("mus","","Creek"),
            new SubtitleLanguage("mwr","","Marwari"),
            new SubtitleLanguage("myn","","Mayan languages"),
            new SubtitleLanguage("myv","","Erzya"),
            new SubtitleLanguage("nah","","Nahuatl"),
            new SubtitleLanguage("nai","","North American Indian"),
            new SubtitleLanguage("nap","","Neapolitan"),
            new SubtitleLanguage("nau","na","Nauru"),
            new SubtitleLanguage("nav","nv","Navajo"),
            new SubtitleLanguage("nbl","nr","Ndebele, South"),
            new SubtitleLanguage("nde","nd","Ndebele, North"),
            new SubtitleLanguage("ndo","ng","Ndonga"),
            new SubtitleLanguage("nds","","Low German"),
            new SubtitleLanguage("nep","ne","Nepali"),
            new SubtitleLanguage("new","","Nepal Bhasa"),
            new SubtitleLanguage("nia","","Nias"),
            new SubtitleLanguage("nic","","Niger-Kordofanian (Other)"),
            new SubtitleLanguage("niu","","Niuean"),
            new SubtitleLanguage("nno","nn","Norwegian Nynorsk"),
            new SubtitleLanguage("nob","nb","Norwegian Bokmal"),
            new SubtitleLanguage("nog","","Nogai"),
            new SubtitleLanguage("non","","Norse, Old"),
            new SubtitleLanguage("nor","no","Norwegian"),
            new SubtitleLanguage("nso","","Northern Sotho"),
            new SubtitleLanguage("nub","","Nubian languages"),
            new SubtitleLanguage("nwc","","Classical Newari"),
            new SubtitleLanguage("nya","ny","Chichewa"),
            new SubtitleLanguage("nym","","Nyamwezi"),
            new SubtitleLanguage("nyn","","Nyankole"),
            new SubtitleLanguage("nyo","","Nyoro"),
            new SubtitleLanguage("nzi","","Nzima"),
            new SubtitleLanguage("oci","oc","Occitan"),
            new SubtitleLanguage("oji","oj","Ojibwa"),
            new SubtitleLanguage("ori","or","Oriya"),
            new SubtitleLanguage("orm","om","Oromo"),
            new SubtitleLanguage("osa","","Osage"),
            new SubtitleLanguage("oss","os","Ossetian"),
            new SubtitleLanguage("ota","","Turkish, Ottoman (1500-1928)"),
            new SubtitleLanguage("oto","","Otomian languages"),
            new SubtitleLanguage("paa","","Papuan (Other)"),
            new SubtitleLanguage("pag","","Pangasinan"),
            new SubtitleLanguage("pal","","Pahlavi"),
            new SubtitleLanguage("pam","","Pampanga"),
            new SubtitleLanguage("pan","pa","Panjabi"),
            new SubtitleLanguage("pap","","Papiamento"),
            new SubtitleLanguage("pau","","Palauan"),
            new SubtitleLanguage("peo","","Persian, Old (ca.600-400 B.C.)"),
            new SubtitleLanguage("per","fa","Persian"),
            new SubtitleLanguage("phi","","Philippine (Other)"),
            new SubtitleLanguage("phn","","Phoenician"),
            new SubtitleLanguage("pli","pi","Pali"),
            new SubtitleLanguage("pol","pl","Polish"),
            new SubtitleLanguage("pon","","Pohnpeian"),
            new SubtitleLanguage("por","pt","Portuguese"),
            new SubtitleLanguage("pra","","Prakrit languages"),
            new SubtitleLanguage("pro","","Provençal, Old (to 1500)"),
            new SubtitleLanguage("pus","ps","Pushto"),
            new SubtitleLanguage("que","qu","Quechua"),
            new SubtitleLanguage("raj","","Rajasthani"),
            new SubtitleLanguage("rap","","Rapanui"),
            new SubtitleLanguage("rar","","Rarotongan"),
            new SubtitleLanguage("roa","","Romance (Other)"),
            new SubtitleLanguage("roh","rm","Raeto-Romance"),
            new SubtitleLanguage("rom","","Romany"),
            new SubtitleLanguage("run","rn","Rundi"),
            new SubtitleLanguage("rup","","Aromanian"),
            new SubtitleLanguage("rus","ru","Russian"),
            new SubtitleLanguage("sad","","Sandawe"),
            new SubtitleLanguage("sag","sg","Sango"),
            new SubtitleLanguage("sah","","Yakut"),
            new SubtitleLanguage("sai","","South American Indian (Other)"),
            new SubtitleLanguage("sal","","Salishan languages"),
            new SubtitleLanguage("sam","","Samaritan Aramaic"),
            new SubtitleLanguage("san","sa","Sanskrit"),
            new SubtitleLanguage("sas","","Sasak"),
            new SubtitleLanguage("sat","","Santali"),
            new SubtitleLanguage("scc","sr","Serbian"),
            new SubtitleLanguage("scn","","Sicilian"),
            new SubtitleLanguage("sco","","Scots"),
            new SubtitleLanguage("sel","","Selkup"),
            new SubtitleLanguage("sem","","Semitic (Other)"),
            new SubtitleLanguage("sga","","Irish, Old (to 900)"),
            new SubtitleLanguage("sgn","","Sign Languages"),
            new SubtitleLanguage("shn","","Shan"),
            new SubtitleLanguage("sid","","Sidamo"),
            new SubtitleLanguage("sin","si","Sinhalese"),
            new SubtitleLanguage("sio","","Siouan languages"),
            new SubtitleLanguage("sit","","Sino-Tibetan (Other)"),
            new SubtitleLanguage("sla","","Slavic (Other)"),
            new SubtitleLanguage("slo","sk","Slovak"),
            new SubtitleLanguage("slv","sl","Slovenian"),
            new SubtitleLanguage("sma","","Southern Sami"),
            new SubtitleLanguage("sme","se","Northern Sami"),
            new SubtitleLanguage("smi","","Sami languages (Other)"),
            new SubtitleLanguage("smj","","Lule Sami"),
            new SubtitleLanguage("smn","","Inari Sami"),
            new SubtitleLanguage("smo","sm","Samoan"),
            new SubtitleLanguage("sms","","Skolt Sami"),
            new SubtitleLanguage("sna","sn","Shona"),
            new SubtitleLanguage("snd","sd","Sindhi"),
            new SubtitleLanguage("snk","","Soninke"),
            new SubtitleLanguage("sog","","Sogdian"),
            new SubtitleLanguage("som","so","Somali"),
            new SubtitleLanguage("son","","Songhai"),
            new SubtitleLanguage("sot","st","Sotho, Southern"),
            new SubtitleLanguage("spa","es","Spanish"),
            new SubtitleLanguage("srd","sc","Sardinian"),
            new SubtitleLanguage("srr","","Serer"),
            new SubtitleLanguage("ssa","","Nilo-Saharan (Other)"),
            new SubtitleLanguage("ssw","ss","Swati"),
            new SubtitleLanguage("suk","","Sukuma"),
            new SubtitleLanguage("sun","su","Sundanese"),
            new SubtitleLanguage("sus","","Susu"),
            new SubtitleLanguage("sux","","Sumerian"),
            new SubtitleLanguage("swa","sw","Swahili"),
            new SubtitleLanguage("swe","sv","Swedish"),
            new SubtitleLanguage("syr","sy","Syriac"),
            new SubtitleLanguage("tah","ty","Tahitian"),
            new SubtitleLanguage("tai","","Tai (Other)"),
            new SubtitleLanguage("tam","ta","Tamil"),
            new SubtitleLanguage("tat","tt","Tatar"),
            new SubtitleLanguage("tel","te","Telugu"),
            new SubtitleLanguage("tem","","Timne"),
            new SubtitleLanguage("ter","","Tereno"),
            new SubtitleLanguage("tet","","Tetum"),
            new SubtitleLanguage("tgk","tg","Tajik"),
            new SubtitleLanguage("tgl","tl","Tagalog"),
            new SubtitleLanguage("tha","th","Thai"),
            new SubtitleLanguage("tib","bo","Tibetan"),
            new SubtitleLanguage("tig","","Tigre"),
            new SubtitleLanguage("tir","ti","Tigrinya"),
            new SubtitleLanguage("tiv","","Tiv"),
            new SubtitleLanguage("tkl","","Tokelau"),
            new SubtitleLanguage("tlh","","Klingon"),
            new SubtitleLanguage("tli","","Tlingit"),
            new SubtitleLanguage("tmh","","Tamashek"),
            new SubtitleLanguage("tog","","Tonga (Nyasa)"),
            new SubtitleLanguage("ton","to","Tonga (Tonga Islands)"),
            new SubtitleLanguage("tpi","","Tok Pisin"),
            new SubtitleLanguage("tsi","","Tsimshian"),
            new SubtitleLanguage("tsn","tn","Tswana"),
            new SubtitleLanguage("tso","ts","Tsonga"),
            new SubtitleLanguage("tuk","tk","Turkmen"),
            new SubtitleLanguage("tum","","Tumbuka"),
            new SubtitleLanguage("tup","","Tupi languages"),
            new SubtitleLanguage("tur","tr","Turkish"),
            new SubtitleLanguage("tut","","Altaic (Other)"),
            new SubtitleLanguage("tvl","","Tuvalu"),
            new SubtitleLanguage("twi","tw","Twi"),
            new SubtitleLanguage("tyv","","Tuvinian"),
            new SubtitleLanguage("udm","","Udmurt"),
            new SubtitleLanguage("uga","","Ugaritic"),
            new SubtitleLanguage("uig","ug","Uighur"),
            new SubtitleLanguage("ukr","uk","Ukrainian"),
            new SubtitleLanguage("umb","","Umbundu"),
            new SubtitleLanguage("und","","Undetermined"),
            new SubtitleLanguage("urd","ur","Urdu"),
            new SubtitleLanguage("uzb","uz","Uzbek"),
            new SubtitleLanguage("vai","","Vai"),
            new SubtitleLanguage("ven","ve","Venda"),
            new SubtitleLanguage("vie","vi","Vietnamese"),
            new SubtitleLanguage("vol","vo","Volapük"),
            new SubtitleLanguage("vot","","Votic"),
            new SubtitleLanguage("wak","","Wakashan languages"),
            new SubtitleLanguage("wal","","Walamo"),
            new SubtitleLanguage("war","","Waray"),
            new SubtitleLanguage("was","","Washo"),
            new SubtitleLanguage("wel","cy","Welsh"),
            new SubtitleLanguage("wen","","Sorbian languages"),
            new SubtitleLanguage("wln","wa","Walloon"),
            new SubtitleLanguage("wol","wo","Wolof"),
            new SubtitleLanguage("xal","","Kalmyk"),
            new SubtitleLanguage("xho","xh","Xhosa"),
            new SubtitleLanguage("yao","","Yao"),
            new SubtitleLanguage("yap","","Yapese"),
            new SubtitleLanguage("yid","yi","Yiddish"),
            new SubtitleLanguage("yor","yo","Yoruba"),
            new SubtitleLanguage("ypk","","Yupik languages"),
            new SubtitleLanguage("zap","","Zapotec"),
            new SubtitleLanguage("zen","","Zenaga"),
            new SubtitleLanguage("zha","za","Zhuang"),
            new SubtitleLanguage("znd","","Zande"),
            new SubtitleLanguage("zul","zu","Zulu"),
            new SubtitleLanguage("zun","","Zuni"),
            new SubtitleLanguage("rum","ro","Romanian"),
            new SubtitleLanguage("pob","pb","Portuguese (BR)"),
            new SubtitleLanguage("mne","me","Montenegrin"),
            new SubtitleLanguage("zht","zt","Chinese (traditional)"),
            new SubtitleLanguage("zhe","ze","Chinese bilingual"),
            new SubtitleLanguage("pom","pm","Portuguese (MZ)"),
            new SubtitleLanguage("ext","ex","Extremadura"),
        };
    }
}