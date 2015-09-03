using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lemon.Lib.Docruzer.Query;
using Lemon.Lib.Docruzer.Enum;

namespace Lemon.Lib.Tests
{
    public class AppModel
    {
        public string code { get; set; }
        public string username { get; set; }
        public string gender { get; set; }
    }

    [TestClass]
    public class UnitTestDocruzer
    {
        [TestMethod]
        public void TestDynaQuery()
        {
            var lQuery = DynaQuery<AppModel>.Instance()
                                            .GroupStart()
                                            .Command(x => x.code, Operator.GreaterThan, "0", SearchType.None)
                                            .And()
                                            .Command(x => x.username, Operator.In, new string[] { "김민준", "강민" }, SearchType.None)
                                            .GroupEnd()
                                            .Append("or")
                                            .Command(x => x.username, Operator.Like, "김민*", SearchType.Boolean)
                                            .Grouping()
                                            .OrderBy(x => x.username)
                                            .ThenByDescending(x => x.code)
                                            .ThenBy(x => x.code)
                                            .ThenByDescending(x => x.gender);

            Assert.AreEqual("( ( code > 0 and username in {김민준,강민} ) or username like '김민*' boolean )", lQuery.GetQuery());
            Assert.AreEqual("order by username asc, code desc, code asc, gender desc", lQuery.GetOrderBy());
        }
    }
}
