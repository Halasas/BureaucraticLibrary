using System.Collections.Generic;
using BureaucraticLibrary.DataContainer;
using BureaucraticLibrary.Departments;

namespace BureaucraticLibrary.Solutions
{
    public enum SolutionTypes { PreCalculatingSolution }
    internal interface ISolution
    {
        public int StartDepartment { get; }
        public int EndDepartment { get; }
        public int NumberOfDepartments { get; }
        public int NumberOfStamps { get; }
        /// <summary> 
        /// Gets all states of Checklist, that appear in this department
        /// </summary>
        /// <param name="departmentIndex"></param>
        /// <returns></returns>
        public abstract List<Checklist> GetChecklists(int departmentIndex);

        public abstract DepartmentStatus GetStatus(int departmentIndex);
    }
}