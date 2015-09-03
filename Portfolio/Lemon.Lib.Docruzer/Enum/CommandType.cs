using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemon.Lib.Docruzer.Enum
{
    public enum CommandType
    {
        /// <summary>
        /// 명령
        /// </summary>
        Command = 0,
        /// <summary>
        /// 논리연산자
        /// </summary>
        LogicalOperator = 1,
        /// <summary>
        /// 그룹 시작
        /// </summary>
        GroupStart = 2,
        /// <summary>
        /// 그룹 종료
        /// </summary>
        GroupEnd = 3
    }
}
