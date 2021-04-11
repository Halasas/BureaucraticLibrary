namespace BureaucraticLibrary.Departments
{
    public class UnconditionalDepartment : IDepartment
    {
        public int StampIndex { get; }
        public int EraseIndex { get; }
        public int NextDepartmentIndex { get; }
        public DepartmentStatus Status { get; internal set; }

        internal UnconditionalDepartment(int stampIndex, int eraseIndex, int nextDepartmentIndex)
        {
            StampIndex = stampIndex;
            EraseIndex = eraseIndex;
            NextDepartmentIndex = nextDepartmentIndex;
        }

        public int ProcessChecklist(Checklist checklist)
        {
            checklist.SetStamp(StampIndex);
            checklist.EraseStamp(EraseIndex);
            return NextDepartmentIndex;
        }

        public IDepartment Clone()
        {
            return new UnconditionalDepartment(StampIndex, EraseIndex, NextDepartmentIndex);
        }
    }
}