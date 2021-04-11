using System.Collections;
using System.Collections.Generic;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.DataContainer
{
    public enum DataContainerType { InMemoryDataStorage }
    internal interface IDataContainer
    {
        public IDepartment GetDepartment(int departmentIndex);
        public List<IDepartment> GetDepartments();
        public bool IsChecklistInCycle(int departmentIndex, Checklist checklist);
        public List<Checklist> GetChecklists(int departmentIndex);
        public bool Contains(int departmentIndex, Checklist checklist);
        public void AddChecklist(int departmentIndex, Checklist checklist);
        public void ReplaceChecklist(int departmentIndex, Checklist checklist);
    }
}