using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemon.Lib.Docruzer.Enum
{
    public enum Operator
    {
        /// <summary>
        /// =
        /// </summary>
        Equals = 0,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan = 1,
        /// <summary>
        /// &lt;=
        /// </summary>
        LessThanEqualTo = 2,
        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan = 3,
        /// <summary>
        /// &gt;=
        /// </summary>
        GreaterThanEqualTo = 4,
        /// <summary>
        /// !=
        /// </summary>
        NotEquals = 5,
        /// <summary>
        /// like
        /// </summary>
        Like = 6,
        /// <summary>
        /// not like
        /// </summary>
        NotLike = 7,
        /// <summary>
        /// in : value에 "단어1,단어2" 혹은 string[] 형태의 데이터를 넣는다.
        /// </summary>
        In = 8,
        /// <summary>
        /// not in : value에 "단어1,단어2" 혹은 string[] 형태의 데이터를 넣는다.
        /// </summary>
        NotIn = 9
    }
}
