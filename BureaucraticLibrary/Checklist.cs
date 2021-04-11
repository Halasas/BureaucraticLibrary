using System;
using System.Collections;

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

        public bool Equals(Checklist other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (_checklist.Count != other._checklist.Count) return false;
            if (GetHashCode() != other.GetHashCode()) return false;
            foreach (bool b in _checklist.Xor(other._checklist))
            {
                if (b) return false;
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
            return (_checklist != null ? _checklist.GetHashCode() : 0);
        }
    }
}