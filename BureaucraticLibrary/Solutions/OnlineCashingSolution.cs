using System.Collections.Generic;
using BureaucraticLibrary.DataContainer;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.Solutions
{
    internal class OnlineCashingSolution : ISolution
    {
        private IDataContainer _storage;
        private List<IDepartment> _departments;

        public int StartDepartment { get; }
        public int EndDepartment { get; }
        public int NumberOfDepartments { get; }
        public int NumberOfStamps { get; }

        public OnlineCashingSolution(IDataContainer storage,
            int startDepartment, int endDepartment, int numberOfDepartments, int numberOfStamps)
        {
            _storage = storage;
            StartDepartment = startDepartment;
            EndDepartment = endDepartment;
            NumberOfDepartments = numberOfDepartments;
            NumberOfStamps = numberOfStamps;
            throw new System.NotImplementedException();
        }

        public List<Checklist> GetChecklists(int departmentIndex)
        {
            throw new System.NotImplementedException();
        }

        public DepartmentStatus GetStatus(int departmentIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}