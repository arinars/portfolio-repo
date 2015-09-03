using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lemon.Lib.Easyui;
using System.Linq;

namespace Lemon.Lib.Tests
{
    [TestClass]
    public class UnitTestEasyui
    {
        [TestMethod]
        public void TestEasyui()
        {
            var lDepts = new UM_Department[] {
                new UM_Department { NO_Dept = 1, NO_ParentDept = null, LV_Dept = 1, Name_Dept = "커리어케어" },
                new UM_Department { NO_Dept = 2, NO_ParentDept = 1, LV_Dept = 2, Name_Dept = "경영기획실" },
                new UM_Department { NO_Dept = 47, NO_ParentDept = 2, LV_Dept = 3, Name_Dept = "경영관리팀" },
                new UM_Department { NO_Dept = 48, NO_ParentDept = 2, LV_Dept = 3, Name_Dept = "정보기술팀" },
                new UM_Department { NO_Dept = 50, NO_ParentDept = 2, LV_Dept = 3, Name_Dept = "기획팀" },
                new UM_Department { NO_Dept = 51, NO_ParentDept = 50, LV_Dept = 4, Name_Dept = "인재센터" },
                new UM_Department { NO_Dept = 54, NO_ParentDept = 2, LV_Dept = 3, Name_Dept = "정보사업팀" },
                new UM_Department { NO_Dept = 53, NO_ParentDept = 54, LV_Dept = 4, Name_Dept = "정보서비스파트" },
                new UM_Department { NO_Dept = 55, NO_ParentDept = 54, LV_Dept = 4, Name_Dept = "정보기술파트" }
            };

            var lTreeData = lDepts.ToEasyuiTree<UM_Department>(
                ReflectionUtil.GetMemberName<UM_Department>(c => c.NO_Dept),
                ReflectionUtil.GetMemberName<UM_Department>(c => c.NO_ParentDept),
                ReflectionUtil.GetMemberName<UM_Department>(c => c.Name_Dept),
                ReflectionUtil.GetMemberName<UM_Department>(c => c.IconClass),
                null, 2, "부서 선택:");

            EasyuiNode<UM_Department> lCareercare = lTreeData.Single(x => Convert.ToInt32(x.id) == 1);
            Assert.AreEqual("커리어케어", lCareercare.text);

            EasyuiNode<UM_Department> lPlan = lCareercare.children.Single(x => Convert.ToInt32(x.id) == 2);
            Assert.AreEqual("경영기획실", lPlan.text);

            EasyuiNode<UM_Department> lInfoBusiness = lPlan.children.Single(x => Convert.ToInt32(x.id) == 54);
            Assert.AreEqual("정보사업팀", lInfoBusiness.text);

            EasyuiNode<UM_Department> lInfoTech = lInfoBusiness.children.Single(x => Convert.ToInt32(x.id) == 55);
            Assert.AreEqual("정보기술파트", lInfoTech.text);
        }
    }


    public class UM_Department
    {
        /// <summary>
        /// 부서번호
        /// </summary>
        public int NO_Dept { get; set; }

        /// <summary>
        /// 상위 부서번호
        /// </summary>
        public int? NO_ParentDept { get; set; }

        /// <summary>
        /// 레벨
        /// </summary>
        public int LV_Dept { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string Name_Dept { get; set; }

        /// <summary>
        /// 부서 아이콘 클래스
        /// </summary>
        public string IconClass { get; set; }
    }
}
