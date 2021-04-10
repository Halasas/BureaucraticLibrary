using System;
using System.Collections.Generic;
using BureaucraticLibrary.DataStorage;
using BureaucraticLibrary.Departments;
using BureaucraticLibrary.Solutions;

namespace BureaucraticLibrary
{
    public class OrganizationConfig
    {
        public OrganizationConfig(DataStorageType storageType)
        {
            StorageType = storageType;
        }

        public int StartDepartment { get; set; }    
        public int EndDepartment { get; set; }
        public int NumberOfDepartments { get; set; }
        public int NumberOfStamps { get; set; }
        public SolutionTypes SolutionType { get; set; } = SolutionTypes.PreCalculatingSolution;
        public DataStorageType StorageType { get; set; } = DataStorageType.InMemoryDataStorage;
        public List<IDepartment> Departments { get; set; } = new List<IDepartment>();
    }

}