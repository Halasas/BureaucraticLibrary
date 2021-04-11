using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BureaucraticLibrary
{
    public class Checklist : ICloneable, IEnumerable, IEquatable<Checklist>
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

        public List<int> GetAllUncheckedStamps()
        {
            return Enumerable.Range(1, _checklist.Count).Where(i => _checklist[i - 1] == false).ToList();
        }

        public bool GetStamp(int index)
        {
            return _checklist[index - 1];
        }

        public void SetStamp(int index)
        {
            _checklist[index - 1] = true;
        }

        public void EraseStamp(int index)
        {
            _checklist[index - 1] = false;
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

        public bool Equals(Checklist other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (_checklist.Count != other._checklist.Count) return false;
            if (GetHashCode() == other.GetHashCode()) return true;
            for (int i = 0; i < _checklist.Count; i++)
            {
                if (_checklist[i] != other._checklist[i])
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Checklist) obj);
        }

        public override int GetHashCode()
        {
            bool[] bools = new bool[_checklist.Count];
            _checklist.CopyTo(bools,0);
            return (_checklist != null ? bools.GetHashCode() : 0);
        }
    }
}