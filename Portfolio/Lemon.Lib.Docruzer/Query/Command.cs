using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemon.Lib.Docruzer.Enum;

namespace Lemon.Lib.Docruzer.Query
{
    public class Command
    {
        /// <summary>
        /// 명령 종류
        /// </summary>
        public CommandType Type { get; set; }
        /// <summary>
        /// 명령 값
        /// </summary>
        public string Value { get; set; }
    }
}
