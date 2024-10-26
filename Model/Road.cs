using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class Road
    {
        private Point[] points;
        private int k = 0;

        public Point[] POINTS { get => this.points; }
        public int K { get => this.k; }

        public Road(int size) 
        {
            points = new Point[size];
        }

        public void add(int i, int j)
        {
            points[k++] = new Point(i, j);
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < k; i++)
                res += points[k].ToString() + "\n";
            return res;
        }
    }
}
