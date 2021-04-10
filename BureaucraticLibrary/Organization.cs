using System;
using System.Collections.Generic;
using BureaucraticLibrary.DataStorage;
using BureaucraticLibrary.Departments;
using BureaucraticLibrary.Solutions;

namespace BureaucraticLibrary
{
    public class Organization
    {
        private readonly ISolution _solution;
        public int StartDepartment => _solution.StartDepartment;
        public int EndDepartment => _solution.EndDepartment;
        public int NumberOfDepartments => _solution.NumberOfDepartments;
        public int NumberOfStamps => _solution.NumberOfStamps;
        public Organization(OrganizationConfig config)
        {
            if (!CheckConfig(config))
            {
                throw new ArgumentException("Config is not correct!");
            }

            IDataStorage storage = null;
            switch (config.StorageType)
            {
                case DataStorageType.FileDataStorage:
                    storage = new FileDataStorage();
                    break;
                case DataStorageType.InMemoryDataStorage:
                    storage = new InMemoryDataStorage();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (config.SolutionType)
            {
                case SolutionTypes.PreCalculatingSolution:
                    _solution = new PreCalculatingSolution(storage, config.Departments,
                        config.StartDepartment, config.EndDepartment, config.NumberOfDepartments, config.NumberOfStamps);
                    break;
                case SolutionTypes.OnlineCashingSolution:
                    _solution = new OnlineCashingSolution(storage, config.Departments,
                        config.StartDepartment, config.EndDepartment, config.NumberOfDepartments, config.NumberOfStamps);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DepartmentResult GetВуDepartmentStates(int departmentIndex)
        {
            return new DepartmentResult(_solution.GetStatus(departmentIndex), _solution.GetChecklists(departmentIndex));
        }

        private static bool CheckConfig(OrganizationConfig config)
        {
            foreach (var department in config.Departments)
            {
                if (department == null)
                    return false;
            }
            return config.StartDepartment > 0 && config.StartDepartment <= config.NumberOfDepartments &&
                   config.EndDepartment > 0 && config.EndDepartment <= config.NumberOfDepartments &&
                   config.NumberOfDepartments > 0 && config.NumberOfStamps > 0 &&
                   config.Departments.Count == config.NumberOfDepartments;
        }
    }
}