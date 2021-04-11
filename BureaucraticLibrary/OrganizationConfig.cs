using System;
using System.Collections.Generic;
using BureaucraticLibrary.DataContainer;
using BureaucraticLibrary.Departments;
using BureaucraticLibrary.Solutions;

namespace BureaucraticLibrary
{
    public class OrganizationConfig
    {
        public int StartDepartment { get; set; }
        public int EndDepartment { get; set; }
        public int NumberOfDepartments { get; set; }
        public int NumberOfStamps { get; set; }
        public SolutionTypes SolutionType { get; set; } = SolutionTypes.PreCalculatingSolution;
        public DataContainerType ContainerType { get; set; } = DataContainerType.InMemoryDataStorage;
        public List<IDepartment> Departments { get; set; } = new List<IDepartment>();
    }

}