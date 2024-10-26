using System.Diagnostics;

namespace DataStructures
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private int n;
        private T[] a;
        private int len = 0;

        public MinHeap()
        {
            a = new T[1001];
        }

        private void swap(int i, int j)
        {
            T aux = a[i];
            a[i] = a[j];
            a[j] = aux;
        }
        
        private void go_up(int i) 
        {
            int parent = i / 2;
            while(parent >= 1 && a[i].CompareTo(a[parent]) < 0)
            {
                swap(i, parent);
                i = parent;
                parent = i / 2;
            }
        }
        public void push(T x)
        {
            a[++len] = x;
            if (len > 1) go_up(len);
        }

        private void go_down(int i)
        {
            int l = 2 * i;
            int r = 2 * i + 1;
            while((l < len && a[i].CompareTo(a[l]) > 0) 
                || (r < len && a[i].CompareTo(a[r]) > 0))
            {
                T min = (r > len || a[l].CompareTo(a[r]) < 0) ? a[l] : a[r];

                if (min.Equals(a[l]))
                {
                    swap(i, l);
                    i = l;
                }
                else
                {
                    swap(i, r);
                    i = r;
                }

                l = i * 2;
                r = i * 2 + 1;
            }
        }
        public void pop()
        {
            swap(1, len--);
            go_down(1);
        }

        public bool empty()
        {
            return len == 0;
        }
        public T top()
        {
            return a[1];
        }
        
        public void print()
        {
            for (int i = 1; i <= len; i++)
                Debug.WriteLine(a[i].ToString());
        }
    }
}