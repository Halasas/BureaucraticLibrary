using System;
using System.Collections;

namespace BureaucraticLibrary
{
    public class Checklist : ICloneable, IEnumerable
    {
        private readonly BitArray _checklist;
        public bool InCycle { get; set; }
        public int Count => _checklist.Count;

        internal Checklist(int numberOfStamps)
        {
            _checklist = new BitArray(numberOfStamps);
            InCycle = false;
        }

        private Checklist(BitArray array, bool inCycle)
        {
            _checklist = array.Clone() as BitArray;
            InCycle = inCycle;
        }

        public bool GetStamp(int index)
        {
            return _checklist[index];
        }

        public void SetStamp(int index)
        {
            _checklist[index] = true;
        }

        public void EraseStamp(int index)
        {
            _checklist[index] = false;
        }

        public IEnumerator GetEnumerator()
        {
            return _checklist.GetEnumerator();
        }

        public object Clone()
        {
            return new Checklist(_checklist, InCycle);
        }

        public void CopyTo(Array array, int index)
        {
            _checklist.CopyTo(array, index);
        }
    }
}