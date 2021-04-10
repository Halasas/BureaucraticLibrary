namespace BureaucraticLibrary.Departments
{   
    public enum DepartmentStatus { NotVisited, Visited, InCycle, Inaccessible }

    public interface IDepartment
    {
        DepartmentStatus Status { get; }
        /// <summary>
        /// Process checklist and return next department index.
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns></returns>
        internal int ProcessChecklist(Checklist checklist);
    }
}
