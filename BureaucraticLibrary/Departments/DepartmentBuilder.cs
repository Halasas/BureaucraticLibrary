using System;

namespace BureaucraticLibrary.Departments
{
    public class DepartmentBuilder
    {
        private bool firstCheck = false, secondCheck = false;

        public int NumberOfDepartments { get; }
        public int NumberOfStamps { get; }

        public DepartmentBuilder(int numberOfDepartments, int numberOfStamps)
        {
            if (numberOfDepartments < 0)
            {
                throw new ArgumentException("numberOfDepartments less then zero");
            }
            if (numberOfStamps < 0)
            {
                throw new ArgumentException("numberOfStamps less then zero");
            }
            NumberOfDepartments = numberOfDepartments;
            NumberOfStamps = numberOfStamps;
        }

        public IDepartment GetDepartment(int stampIndex, int eraseIndex, int nextDepartmentIndex)
        {
            if (CheckBehavior(stampIndex, eraseIndex, nextDepartmentIndex) != true)
            {
                throw new ArgumentException("One or more indexes out of range");
            }

            return new UnconditionalDepartment(stampIndex, eraseIndex, nextDepartmentIndex - 1);
        }

        public IDepartment GetDepartment(int conditionIndex,
            int firstStampIndex, int firstEraseIndex, int firstNextDepartmentIndex,
            int secondStampIndex, int secondEraseIndex, int secondNextDepartmentIndex)
        {
            if (!CheckBehavior(firstStampIndex, firstEraseIndex, firstNextDepartmentIndex) ||
                !CheckBehavior(secondStampIndex, secondEraseIndex, secondNextDepartmentIndex) ||
                conditionIndex < 0 || conditionIndex > NumberOfStamps)
            {
                throw new ArgumentException("One or more indexes out of range");
            }

            return new ConditionalDepartment(conditionIndex,
                firstStampIndex, firstEraseIndex, firstNextDepartmentIndex - 1,
                secondStampIndex, secondEraseIndex, secondNextDepartmentIndex - 1);
        }

        private bool CheckBehavior(int stamp, int erase, int nextDepartment)
        {
            return stamp > 0 && erase > 0 && nextDepartment > 0 &&
                   stamp <= NumberOfStamps && erase <= NumberOfStamps &&
                   nextDepartment <= NumberOfDepartments;
        }
    }
}