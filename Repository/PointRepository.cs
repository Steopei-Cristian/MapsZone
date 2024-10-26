using Atestat1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Repository
{
    internal class PointRepository : Repository<Point>
    {
        public PointRepository()
        {

        }

        protected override void getAll()
        {
            base.getAll();
        }

        public override void printAll()
        {
            base.printAll();
        }

        public Road getRoad(int i)
        {
            string sql1 = "select x from points1 where road_id = " + i.ToString();
            List<int> xs = db.Load_Data<int, dynamic>(sql1, new { }, connection);
            string sql2 = "select y from points1 where road_id = " + i.ToString();
            List<int> ys = db.Load_Data<int, dynamic>(sql2, new { }, connection);
            Road res = new Road(xs.Count);
            for (int j = 0; j < xs.Count; j++)
                res.add(xs[j], ys[j]);
            return res;
        }
    }
}
