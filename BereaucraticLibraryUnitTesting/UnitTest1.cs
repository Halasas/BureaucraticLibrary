using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BureaucraticLibrary;
using BureaucraticLibrary.DataContainer;
using BureaucraticLibrary.Departments;
using BureaucraticLibrary.Solutions;

namespace BureaucraticLibraryUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void JustLine10000()
        {
            int n = 10000;

            var departmentBuilder = new DepartmentBuilder(n, n);
            var organizationConfig = new OrganizationConfig()
            {
                StartDepartment = 1,
                EndDepartment = n,
                NumberOfDepartments = n,
                NumberOfStamps = n,
            };

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                organizationConfig.Departments.Add
                (
                    departmentBuilder.GetDepartment(i + 1, j + 1, j + 1)
                );
            }

            var organization = new Organization(organizationConfig);
            for (int i = 1; i <= n; i++)
            {
                var a = organization.GetResult(i);
                Assert.IsNotNull(a);
                Assert.IsNotNull(a.Checklists);
                Assert.IsTrue(a.Checklists.Count == 1);
                var uncheckedStamps = a.Checklists[0].GetAllUncheckedStamps();
                if (i == n) Assert.IsTrue(uncheckedStamps.Count == 1 && uncheckedStamps[0] == 1);
                else
                {
                    foreach (var stamp in uncheckedStamps)
                    {
                        Assert.IsTrue(stamp > i);
                    }
                }

                Assert.IsTrue(a.Status == DepartmentStatus.Visited);
            }

        }

        [TestMethod]
        public void JustLine100000WithTasks()
        {
            int n = 100000;

            var departmentBuilder = new DepartmentBuilder(n, n);
            var organizationConfig = new OrganizationConfig()
            {
                StartDepartment = 1,
                EndDepartment = n,
                NumberOfDepartments = n,
                NumberOfStamps = n,
            };

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                organizationConfig.Departments.Add
                (
                    departmentBuilder.GetDepartment(i + 1, j + 1, j + 1)
                );
            }

            var organization = new Organization(organizationConfig);
            List<Task> tasks = new List<Task>();
            for (int i = 1; i <= n/100; i++)
            {
                var i1 = i;
                tasks.Add(new Task(() =>
                {
                    var a = organization.GetResult(i1);
                    Assert.IsNotNull(a);
                    Assert.IsNotNull(a.Checklists);
                    Assert.IsTrue(a.Checklists.Count == 1);
                    var uncheckedStamps = a.Checklists[0].GetAllUncheckedStamps();
                    if (i1 == n) Assert.IsTrue(uncheckedStamps.Count == 1 && uncheckedStamps[0] == 1);
                    else
                    {
                        foreach (var stamp in uncheckedStamps)
                        {
                            Assert.IsTrue(stamp > i1);
                        }
                    }
                    Assert.IsTrue(a.Status == DepartmentStatus.Visited);
                }));
                tasks[i - 1].Start();
            }
            Task.WaitAll(tasks.ToArray());
        }

        [TestMethod]
        public void SimpleCycle()
        {
            var departmentBuilder = new DepartmentBuilder(6, 5);
            var organizationConfig = new OrganizationConfig()
            {
                StartDepartment = 1,
                EndDepartment = 6,
                NumberOfDepartments = 6,
                NumberOfStamps = 5,
            };
            for (int i = 0; i < 4; i++)
            {
                int j = (i + 1) % 5;
                organizationConfig.Departments.Add
                (
                    departmentBuilder.GetDepartment(i + 1, j + 1, j + 1)
                );
            }

            organizationConfig.Departments.Add
            (
                departmentBuilder.GetDepartment(5,
                    5, 1, 1,
                    5, 1, 6)
            );

            organizationConfig.Departments.Add(departmentBuilder.GetDepartment(1, 2, 1));

            var organization = new Organization(organizationConfig);
            for (int i = 1; i <= 6; i++)
            {
                var a = organization.GetResult(i);
                Assert.IsNotNull(a);
                Assert.IsNotNull(a.Checklists);
                foreach (var aChecklist in a.Checklists)
                {
                    var ch = a.Checklists[0].GetAllUncheckedStamps();
                    if (a.Status == DepartmentStatus.InCycle)
                    {
                        Assert.IsNotNull(ch);
                        Assert.IsTrue(a.Checklists.Count == 2 || i == 4 || i == 5);
                    }
                }

                Assert.IsTrue(a.Status == DepartmentStatus.InCycle || a.Status == DepartmentStatus.Inaccessible);
            }
        }

        [TestMethod]
        public void Cycle8()
        {
            var depBuilder = new DepartmentBuilder(9, 3);
            var orgConfig = new OrganizationConfig()
            {
                StartDepartment = 1,
                EndDepartment = 9,
                NumberOfDepartments = 9,
                NumberOfStamps = 5,
            };
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 2));
            orgConfig.Departments.Add(depBuilder.GetDepartment(2,
                1, 2, 3,
                3, 2, 6));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 4));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 5));
            orgConfig.Departments.Add(depBuilder.GetDepartment(2, 1, 2));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 7));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 8));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 2));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 1));
            var organization = new Organization(orgConfig);

            var result = organization.GetResult(1);
            Assert.IsTrue(result.Status == DepartmentStatus.Visited);

            result = organization.GetResult(9);
            Assert.IsTrue(result.Status == DepartmentStatus.Inaccessible);

            for (int i = 2; i <= 8; i++)
            {
                result = organization.GetResult(i);
                Assert.IsTrue(result.Status == DepartmentStatus.InCycle);
            }
        }

        [TestMethod]
        public void TaskRequests()
        {
            var depBuilder = new DepartmentBuilder(9, 3);
            var orgConfig = new OrganizationConfig()
            {
                StartDepartment = 1,
                EndDepartment = 9,
                NumberOfDepartments = 9,
                NumberOfStamps = 5,
            };
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 2));
            orgConfig.Departments.Add(depBuilder.GetDepartment(2,
                1, 2, 3,
                3, 2, 6));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 4));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 5));
            orgConfig.Departments.Add(depBuilder.GetDepartment(2, 1, 2));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 7));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 8));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 2));
            orgConfig.Departments.Add(depBuilder.GetDepartment(1, 2, 1));
            var organization = new Organization(orgConfig);

            var result = organization.GetResult(1);
            Assert.IsTrue(result.Status == DepartmentStatus.Visited);

            result = organization.GetResult(9);
            Assert.IsTrue(result.Status == DepartmentStatus.Inaccessible);

            List<Task<DepartmentResult>> tasks = new List<Task<DepartmentResult>>();
            for (int i = 2; i <= 8; i++)
            {
                tasks.Add(new Task<DepartmentResult>(() => organization.GetResult(i)));
                tasks[i - 2].Start();
            }
            Task.WaitAll(tasks.ToArray());
            foreach (var task in tasks)
            {
                Assert.IsTrue(task.Result.Status == DepartmentStatus.Inaccessible);
            }
        }

    }
}
