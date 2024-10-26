using Atestat1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Repository
{
    internal class PrevTripRepository : Repository<PrevTrip>
    {
        public PrevTripRepository()
        {
            getAll();
        }

        protected override void getAll()
        {
            string sql = "select * from prevtrips";
            all = db.Load_Data<PrevTrip, dynamic>(sql, new { }, this.connection);
        }

        public override void printAll()
        {
            foreach (PrevTrip t in all)
                Debug.Write(t);
        }

        public void addTrip(int User_id, string Roads, int Length,
            int Start, int End)
        {
            string sql = "insert into prevtrips(user_id, roads, length, start, end)" +
                "values (@User_id, @Roads, @Length, @Start, @End)";
            db.Save_Data(sql, new { User_id, Roads, Length, Start, End}, connection);
            getAll();
        }

        public List<PrevTrip> getByUser(int User_id)
        {
            string sql = "select * from prevtrips where user_id = @User_id";
            return db.Load_Data<PrevTrip, dynamic>(sql, new { User_id }, connection);
        }
    }
}
