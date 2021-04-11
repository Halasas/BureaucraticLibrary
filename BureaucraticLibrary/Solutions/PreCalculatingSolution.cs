using System;
using System.Collections.Generic;
using System.Threading;
using BureaucraticLibrary.DataContainer;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.Solutions
{
    internal class PreCalculatingSolution : ISolution
    {
        private Checklist currentChecklist;

        private readonly IDataContainer _storage;
        public int StartDepartment { get; }
        public int EndDepartment { get; }
        public int NumberOfDepartments { get; }
        public int NumberOfStamps { get; }

        public PreCalculatingSolution(IDataContainer storage, int startDepartment, int endDepartment, int numberOfDepartments, int numberOfStamps)
        {
            if (storage == null)
                throw new NullReferenceException("Storage is null");
            _storage = storage;
            StartDepartment = startDepartment;
            EndDepartment = endDepartment;
            NumberOfDepartments = numberOfDepartments;
            NumberOfStamps = numberOfStamps;
            currentChecklist = new Checklist(numberOfStamps);
            PreCalculate();
        }

        public List<Checklist> GetChecklists(int departmentIndex)
        {
            return _storage.GetChecklists(departmentIndex);
        }

        public DepartmentStatus GetStatus(int departmentIndex)
        {
            return _storage.GetDepartment(departmentIndex).Status;
        }

        private void PreCalculate()
        {
            int currentDepartmentIndex = StartDepartment;
            while (currentDepartmentIndex != EndDepartment)
            {
                var currentDepartment = _storage.GetDepartment(currentDepartmentIndex);

            }
        }
    }
}