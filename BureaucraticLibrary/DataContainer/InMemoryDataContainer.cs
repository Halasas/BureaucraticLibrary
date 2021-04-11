using System;
using System.Collections.Generic;
using System.Linq;
using BureaucraticLibrary;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.DataContainer
{
    public class InMemoryDataContainer : IDataContainer
    {
        private readonly int numberOfDepartments;
        private readonly List<IDepartment> _departments;
        private readonly List<HashSet<Checklist>> _checklistContainer;

        public InMemoryDataContainer(List<IDepartment> departments)
        {
            if (departments == null || departments.Contains(null))
                throw new NullReferenceException("departments is null or contains null");
            numberOfDepartments = departments.Count;
            _checklistContainer = new List<HashSet<Checklist>>(numberOfDepartments);
            _departments = departments;
        }

        public IDepartment GetDepartment(int departmentIndex)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            return _departments[departmentIndex];
        }

        public List<IDepartment> GetDepartments()
        {
            return _departments;
        }

        public List<Checklist> GetChecklists(int departmentIndex)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            return _checklistContainer[departmentIndex].ToList();
        }

        public Checklist GetSameChecklist(int departmentIndex, Checklist checklist)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            if (checklist == null)
                throw new NullReferenceException("Checklist is null");
            Checklist res = null;
            _checklistContainer[departmentIndex].TryGetValue(checklist, out res);
            return res;
        }

        public bool Contains(int departmentIndex, Checklist checklist)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            if (checklist == null)
                throw new NullReferenceException("Checklist is null");
            return _checklistContainer[departmentIndex].TryGetValue(checklist, out _);
        }

        public void AddChecklist(int departmentIndex, Checklist checklist)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            if (checklist == null)
                throw new NullReferenceException("Checklist is null");
            _checklistContainer[departmentIndex].Add(checklist);
        }

        public void ReplaceChecklist(int departmentIndex, Checklist checklist)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            if (checklist == null)
                throw new NullReferenceException("Checklist is null");
            _checklistContainer[departmentIndex].Remove(checklist);
            _checklistContainer[departmentIndex].Add(checklist);
        }
    }
}