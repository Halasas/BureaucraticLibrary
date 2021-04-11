using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BureaucraticLibrary.DataContainer;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.Solutions
{
    internal class PreCalculatingSolution : ISolution
    {
        private Mutex precalcMutex = new Mutex();
        private bool isReady = false;
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
            WaitForPreCalculating();
        }

        public List<Checklist> GetChecklists(int departmentIndex)
        {
            WaitForPreCalculating();
            var res = _storage.GetChecklists(departmentIndex);
            return res;
        }

        public DepartmentStatus GetStatus(int departmentIndex)
        {
            WaitForPreCalculating();
            var res = _storage.GetDepartment(departmentIndex).Status;
            return res;
        }

        private void WaitForPreCalculating()
        {
            precalcMutex.WaitOne();
            if (!isReady)
            {
                PreCalculate();
            }
            precalcMutex.ReleaseMutex();
        }

        private void PreCalculate()
        {
            int currentDepartmentIndex = StartDepartment;
            int nextDepartmentIndex = StartDepartment;
            var currentChecklist = new Checklist(NumberOfStamps);
            do
            {
                currentDepartmentIndex = nextDepartmentIndex;
                var currentDepartment = _storage.GetDepartment(currentDepartmentIndex);
                nextDepartmentIndex = currentDepartment.ProcessChecklist(currentChecklist);
                currentDepartment.Status = DepartmentStatus.Visited;

                if (_storage.Contains(currentDepartmentIndex, currentChecklist))
                {
                    currentDepartment.Status = DepartmentStatus.InCycle;
                    currentChecklist.InCycle = true;
                    if (!_storage.IsChecklistInCycle(currentDepartmentIndex, currentChecklist))
                    {
                        _storage.ReplaceChecklist(currentDepartmentIndex, currentChecklist.Clone() as Checklist);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    _storage.AddChecklist(currentDepartmentIndex, currentChecklist.Clone() as Checklist);
                }
            } while (currentDepartmentIndex != EndDepartment);

            foreach (var department in _storage.GetDepartments().Where(department => department.Status == DepartmentStatus.NotVisited))
            {
                department.Status = DepartmentStatus.Inaccessible;
            }

            isReady = true;
        }
    }
}