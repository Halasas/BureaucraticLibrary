using System.Collections.Generic;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.DataContainer
{
    public class FileDataContainer : IDataContainer
    {
        public FileDataContainer(List<IDepartment> departments)
        {
            throw new System.NotImplementedException();
        }
        public IDepartment GetDepartment(int departmentIndex)
        {
            throw new System.NotImplementedException();
        }

        public List<IDepartment> GetDepartments()
        {
            throw new System.NotImplementedException();
        }

        public Checklist GetSameChecklist(int departmentIndex, Checklist checklist)
        {
            throw new System.NotImplementedException();
        }

        public List<Checklist> GetChecklists(int departmentIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(int departmentIndex, Checklist checklist)
        {
            throw new System.NotImplementedException();
        }

        public void AddChecklist(int departmentIndex, Checklist checklist)
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceChecklist(int departmentIndex, Checklist checklist)
        {
            throw new System.NotImplementedException();
        }
    }
}