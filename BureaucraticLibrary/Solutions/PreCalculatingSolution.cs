using System;
using System.Collections.Generic;
using BureaucraticLibrary.DataStorage;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.Solutions
{
    internal class PreCalculatingSolution : ISolution
    {
        private IDataStorage _storage;
        private List<IDepartment> _departments;
        public int StartDepartment { get; }
        public int EndDepartment { get; }
        public int NumberOfDepartments { get; }
        public int NumberOfStamps { get; }

        public PreCalculatingSolution(IDataStorage storage, List<IDepartment> departments, int startDepartment, int endDepartment, int numberOfDepartments, int numberOfStamps)
        {
            _storage = storage;
            _departments = departments;
            StartDepartment = startDepartment;
            EndDepartment = endDepartment;
            NumberOfDepartments = numberOfDepartments;
            NumberOfStamps = numberOfStamps;
        }

        public List<Checklist> GetChecklists(int departmentIndex)
        {
            throw new System.NotImplementedException();
        }

        public DepartmentStatus GetStatus(int departmentIndex)
        {
            throw new System.NotImplementedException();
        }

        private void PreCalculate()
        {
            throw new System.NotImplementedException();
        }
    }
}