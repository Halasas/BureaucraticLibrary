using System;

namespace BureaucraticLibrary.Departments
{
    public class DepartmentBuilder
    {
        private bool firstCheck = false, secondCheck = false;

        public int NumberOfDepartments { get; }
        public int NumberOfStamps { get; }
        public int ConditionIndex { get; private set; }
        public int FirstStampIndex { get; private set; }
        public int FirstEraseIndex { get; private set; }
        public int FirstNextDepartmentIndex { get; private set; }
        public int SecondStampIndex { get; private set; }
        public int SecondEraseIndex { get; private set; }
        public int SecondNextDepartmentIndex { get; private set; }

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

            Reset();
        }

        public DepartmentBuilder SetCondition(int index)
        {
            if (index < 0 || index > NumberOfStamps)
            {
                throw new ArgumentException("Index out of range");
            }
            ConditionIndex = index;
            return this;
        }
        public DepartmentBuilder SetBehavior(int stampIndex, int eraseIndex, int nextDepartmentIndex)
        {
            if (CheckBehavior(stampIndex, eraseIndex, nextDepartmentIndex) != true)
            {
                throw new ArgumentException("One or more indexes out of range");
            }
            FirstStampIndex = stampIndex;
            FirstEraseIndex = eraseIndex;
            FirstNextDepartmentIndex = nextDepartmentIndex;
            firstCheck = true;
            return this;
        }
        public DepartmentBuilder SetSecondBehavior(int stampIndex, int eraseIndex, int nextDepartmentIndex)
        {
            if (CheckBehavior(stampIndex, eraseIndex, nextDepartmentIndex) != true)
            {
                throw new ArgumentException("One or more indexes out of range");
            }
            SecondStampIndex = stampIndex;
            SecondEraseIndex = eraseIndex;
            SecondNextDepartmentIndex = nextDepartmentIndex;
            secondCheck = true;
            return this;
        }

        public IDepartment GetDepartmentConfig()
        {
            if (ConditionIndex == -1 && firstCheck)
            {
                return new UnconditionalDepartment(FirstStampIndex, FirstEraseIndex, FirstNextDepartmentIndex);
            }
            else if (ConditionIndex != -1 && firstCheck && secondCheck)
            {
                return new ConditionalDepartment(ConditionIndex,
                    FirstStampIndex, FirstEraseIndex, FirstNextDepartmentIndex,
                    SecondStampIndex, SecondEraseIndex, SecondNextDepartmentIndex);
            }
            else
            {
                throw new InvalidOperationException("Building isn't completed correctly.");
            }
        }

        public DepartmentBuilder Reset()
        {
            ConditionIndex = -1;

            FirstStampIndex = FirstEraseIndex = FirstNextDepartmentIndex = -1;
            SecondStampIndex = SecondEraseIndex = SecondNextDepartmentIndex = -1;

            firstCheck = secondCheck = false;
            return this;
        }

        private bool CheckBehavior(int stamp, int erase, int nextDepartment)
        {
            return stamp > 0 && erase > 0 && nextDepartment > 0 &&
                   stamp <= NumberOfStamps && erase <= NumberOfStamps &&
                   nextDepartment <= NumberOfDepartments;
        }
    }
}