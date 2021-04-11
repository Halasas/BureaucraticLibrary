using System;

namespace BureaucraticLibrary.Departments
{
    public class ConditionalDepartment : IDepartment
    {
        public int ConditionIndex { get; }
        public int FirstStampIndex { get; }
        public int FirstEraseIndex { get; }
        public int FirstNextDepartmentIndex { get; }
        public int SecondStampIndex { get; }
        public int SecondEraseIndex { get; }
        public int SecondNextDepartmentIndex { get; }
        public DepartmentStatus Status { get; set; }

        internal ConditionalDepartment(int conditionIndex,
            int firstStampIndex, int firstEraseIndex, int firstNextDepartmentIndex,
            int secondStampIndex, int secondEraseIndex, int secondNextDepartmentIndex)
        {
            ConditionIndex = conditionIndex;

            FirstStampIndex = firstStampIndex;
            FirstEraseIndex = firstEraseIndex;
            FirstNextDepartmentIndex = firstNextDepartmentIndex;

            SecondStampIndex = secondStampIndex;
            SecondEraseIndex = secondEraseIndex;
            SecondNextDepartmentIndex = secondNextDepartmentIndex;
        }

        public int ProcessChecklist(Checklist checklist)
        {
            if (!checklist.GetStamp(ConditionIndex))
            {
                checklist.SetStamp(FirstStampIndex);
                checklist.EraseStamp(FirstEraseIndex);
                return FirstNextDepartmentIndex;
            }
            else
            {
                checklist.SetStamp(SecondStampIndex);
                checklist.EraseStamp(SecondEraseIndex);
                return SecondNextDepartmentIndex;
            }
        }

        public IDepartment Clone()
        {
            return new ConditionalDepartment(ConditionIndex, 
                FirstStampIndex, FirstEraseIndex, FirstNextDepartmentIndex,
                SecondStampIndex, SecondEraseIndex, SecondNextDepartmentIndex);
        }
    }
}