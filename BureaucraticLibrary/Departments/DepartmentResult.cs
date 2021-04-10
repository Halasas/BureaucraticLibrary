using System.Collections.Generic;

namespace BureaucraticLibrary.Departments
{
    public class DepartmentResult
    {
        public DepartmentStatus Status { get; }
        public List<Checklist> Checklists { get; }
        internal DepartmentResult(DepartmentStatus status, List<Checklist> checklists)
        {
            status = Status;
            checklists = Checklists;
        }
    }
}