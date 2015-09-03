using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Lemon.Lib.Docruzer.Enum
{
    public enum AdvancedSearchType
    {
        ///// <summary>
        ///// 없음
        ///// </summary>
        //[EnumDescription("선택:")]
        //None = 0,
        /// <summary>
        /// 자연어 검색
        /// </summary>
        [EnumDescription("자연어 검색")]
        Natural = 1,
        /// <summary>
        /// 아무 단어나 포함
        /// </summary>
        [EnumDescription("아무 단어나 포함")]
        Anyword = 2,
        /// <summary>
        /// 모든 단어 포함(순서없이)
        /// </summary>
        [EnumDescription("모든 단어 포함(순서없이)")]
        Allword = 3,
        /// <summary>
        /// 모든 단어 포함(순서없이, 25 단어 안에)
        /// </summary>
        [EnumDescription("모든 단어 포함(순서없이, 25 단어 안에)")]
        Allin25 = 4,
        /// <summary>
        /// 모든 단어 포함(순서없이, 인접해서)
        /// </summary>
        [EnumDescription("모든 단어 포함(순서없이, 인접해서)")]
        Alladjacent = 5,
        /// <summary>
        /// 모든 단어 포함(순서대로, 인접해서)
        /// </summary>
        [EnumDescription("모든 단어 포함(순서대로, 인접해서)")]
        Allorderadjacent = 6,
        ///// <summary>
        ///// 모든 단어 포함(인덱스 전체에서)
        ///// </summary>
        //[EnumDescription("모든 단어 포함(인덱스 전체에서)")]
        //Allwordthruindex = 7,
        /// <summary>
        /// 최소 몇 단어 이상 포함
        /// </summary>
        [EnumDescription("최소 몇 단어 이상 포함")]
        Someword = 8
        ///// <summary>
        ///// 최소 몇 단어 이상 포함(인덱스 전체에서)
        ///// </summary>
        //[EnumDescription("최소 몇 단어 이상 포함(인덱스 전체에서)")]
        //Somewordthruindex = 9
    }
}
