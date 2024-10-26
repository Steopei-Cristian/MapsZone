using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Pair<T, U> : IComparable<Pair<T, U>> where T : IComparable<T> where U : IComparable<U> 
    {
        private T a;
        private U b;

        public T First { get => this.a; }
        public U Second { get => this.b; }

        public Pair(T first, U second)
        {
            a = first;
            b = second;
        }

        public int CompareTo(Pair<T, U>? other)
        {
            if (a.CompareTo(other!.a) < 0)
                return -1;
            if (a.CompareTo(other!.a) == 0)
                if (b.CompareTo(other!.b) < 0)
                    return -1;
                else if (b.CompareTo(other!.b) == 0)
                    return 0;
            return 1;
        }
        public override string ToString()
        {
            return a.ToString() + " | " + b.ToString();
        }
    }
}
