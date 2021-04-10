using Microsoft.VisualStudio.TestTools.UnitTesting;
using BureaucraticLibrary;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibraryUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var departmentConfigBuilder = new DepartmentBuilder(10, 10);
            var departmentConfig = departmentConfigBuilder.SetBehavior(1, 1, 1).GetDepartmentConfig();
            Assert.IsNotNull(departmentConfig);
            Checklist checklist = new Checklist(10);
        }
    }
}
