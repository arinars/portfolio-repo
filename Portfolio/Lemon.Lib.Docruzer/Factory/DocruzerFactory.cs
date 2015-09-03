using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemon.Lib.Docruzer.Enum;

namespace Lemon.Lib.Docruzer.Factory
{
    public static class DocruzerFactory
    {
        /// <summary>
        /// 검색타입명을 가져옵니다.
        /// </summary>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        public static string GetSearchType(SearchType aSearchType)
        {
            switch (aSearchType)
            {
                case SearchType.None: return "";
                case SearchType.Natural: return "natural";
                case SearchType.Anyword: return "anyword";
                case SearchType.Allword: return "allword";
                case SearchType.Allin25: return "allin25";
                case SearchType.Alladjacent: return "alladjacent";
                case SearchType.Allorderadjacent: return "allorderadjacent";
                case SearchType.Allwordthruindex: return "allwordthruindex";
                case SearchType.Someword: return "someword";
                case SearchType.Somewordthruindex: return "somewordthruindex";
                case SearchType.Boolean: return "boolean";
                case SearchType.Similar: return "similar";
                case SearchType.Similar2: return "similar2";
                case SearchType.Replicate: return "replicate";
                case SearchType.Proxkeymatch: return "proxkeymatch";
                case SearchType.Synonym: return "synonym";
                default: return "";
            }
        }

        public static SearchType ToSearchType(this AdvancedSearchType aOrgType)
        {
            switch (aOrgType)
            {
                case AdvancedSearchType.Alladjacent:
                    return SearchType.Alladjacent;
                case AdvancedSearchType.Allin25:
                    return SearchType.Allin25;
                case AdvancedSearchType.Allorderadjacent:
                    return SearchType.Allorderadjacent;
                case AdvancedSearchType.Allword:
                    return SearchType.Allword;
                case AdvancedSearchType.Anyword:
                    return SearchType.Anyword;
                case AdvancedSearchType.Natural:
                    return SearchType.Natural;
                case AdvancedSearchType.Someword:
                    return SearchType.Someword;
                default:
                    return SearchType.None;
            }
        }
    }
}
