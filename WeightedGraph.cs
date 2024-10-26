using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace DataStructures
{
    public class WeightedGraph
    {
        private int n;
        private List<List<Pair<int, int>>> G_u = new List<List<Pair<int, int>>>(1001);
        private List<List<Pair<int, int>>> G_o = new List<List<Pair<int, int>>>(1001);
        
        private string file = string.Empty;
        private int inf = 0x3f3f3f3f;

        private Pair<int, int>[] p = new Pair<int, int>[1001];
        private int k = 0;

        public int N { get => this.n; }
        public string FILE { get => this.file; }
        public List<List<Pair<int, int>>> G_U { get => this.G_u; } 
        public List<List<Pair<int, int>>> G_O { get => this.G_o; }

        public WeightedGraph(string path)
        {
            this.file = path;
            read();
        }
        
        private void read()
        {
            StreamReader fin = new StreamReader(file);
            this.n = int.Parse(fin.ReadLine()!);
            string line = string.Empty;

            for (int i = 0; i <= n; i++)
            {
                G_u.Add(new List<Pair<int, int>>(1001));
                G_o.Add(new List<Pair<int, int>>(1001));
            }

            while((line = fin.ReadLine()!) != null)
            {
                string[] x = line.Split(' ');
                int a = int.Parse(x[0]), b = int.Parse(x[1]), c = int.Parse(x[2]);

                G_u[a].Add(new Pair<int, int>(b, c));
                G_u[b].Add(new Pair<int, int>(a, c));

                G_o[a].Add(new Pair<int, int>(b, c));

                p[++k] = new Pair<int, int>(a, b);
            }
        }

        public void printPairs()
        {
            for (int i = 1; i <= k; i++)
                Debug.WriteLine(p[i].ToString());
        }
        public int bs_pair(int a, int b)
        {
            int st = 1, dr = k;
            Pair<int, int> x = new Pair<int, int>(Math.Min(a, b), Math.Max(a, b));

            while(st <= dr)
            {
                int mid = (st + dr) / 2;
                Pair<int, int> p_mid = p[mid];

                int comp = x.CompareTo(p_mid);

                if (comp == 0)
                    return mid;
                else if (comp > 0)
                    st = mid + 1;
                else
                    dr = mid - 1;
            }
            return -1;
        }

        public void print(int k)
        {
            if(k == 1)
            {
                for (int i = 1; i <= n; i++)
                {
                    Debug.Write(i + ": ");
                    foreach (var x in G_u[i])
                        Debug.Write(x.First + " ");
                    Debug.WriteLine("");
                }
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    Debug.Write(i + ": ");
                    foreach (var x in G_u[i])
                        Debug.Write(x.First + " ");
                    Debug.WriteLine("");
                }
            }
        }
        
        public int[] dijkstra(int start, int end)
        {
            int[] path = new int[1001];

            MinHeap<Pair<int, int>> q = new MinHeap<Pair<int, int>>();
            q.push(new Pair<int, int>(0, start));

            int[] dist = new int[1001];
            for (int i = 1; i <= n; i++)
                dist[i] = inf;
            dist[start] = 0;

            while (!q.empty())
            {
                int nod = q.top().Second;
                int d = q.top().First;
                q.pop();

                if (d > dist[nod])
                    continue;

                foreach(var x in G_u[nod])
                {
                    int vecin = x.First;
                    int cost = x.Second;

                    if(d + cost < dist[vecin])
                    {
                        path[vecin] = nod;
                        dist[vecin] = d + cost;
                        q.push(new Pair<int, int>(dist[vecin], vecin));
                    }
                }
            }
            path[0] = dist[end];

            return path; 
            // i = 0 -> lungimea, 1 <= i <= n -> parintele
           //  Debug.WriteLine(dist[end]);
        }
        public List<int> getRoadsOnPath(int a, int b, out int length)
        {
            List<int> roads = new List<int>(1001);

            int[] dijk = dijkstra(a, b);
            int parent = dijk[b], child = b;

            length = dijk[0];

            while(child != a)
            {
                roads.Add(bs_pair(parent, child));
                child = parent;
                parent = dijk[child];
            }

            return roads;
        }
    }
}
