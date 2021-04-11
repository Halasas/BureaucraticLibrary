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
        private readonly List<List<Checklist>> _checklistContainer;

        public InMemoryDataContainer(List<IDepartment> departments)
        {
            if (departments == null || departments.Contains(null))
                throw new NullReferenceException("departments is null or contains null");
            numberOfDepartments = departments.Count;
            _checklistContainer = new List<List<Checklist>>();
            for (int i = 0; i < numberOfDepartments; i++)
            {
                _checklistContainer.Add(new List<Checklist>());
            }
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

        public bool IsChecklistInCycle(int departmentIndex, Checklist checklist)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            if (checklist == null)
                throw new NullReferenceException("Checklist is null");
            int i =_checklistContainer[departmentIndex].FindIndex(item => item.Equals(checklist));
            return i != -1 && _checklistContainer[departmentIndex][i].InCycle;
        }

        public bool Contains(int departmentIndex, Checklist checklist)
        {
            if (departmentIndex < 0 || departmentIndex > numberOfDepartments)
                throw new ArgumentException("Wrong department index");
            if (checklist == null)
                throw new NullReferenceException("Checklist is null");
            return _checklistContainer[departmentIndex].Contains(checklist);
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