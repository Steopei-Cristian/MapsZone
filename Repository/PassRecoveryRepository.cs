using Atestat1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Repository
{
    internal class PassRecoveryRepository : Repository<PassRecovery>
    {
        public PassRecoveryRepository() : base()
        {
            getAll();
        }

        protected override void getAll()
        {
            string sql = "select * from passrecoveries";
            all = db.Load_Data<PassRecovery, dynamic>(sql, new { }, connection);
        }

        public override void printAll()
        {
            Debug.WriteLine(all.Count);
            foreach (PassRecovery p in all)
                Debug.WriteLine(p.ToString());
        }

        public bool isActive(int id)
        {
            TimeSpan t = DateTime.Now.Subtract(all[id - 1].CREATED_AT);
            return t.TotalMinutes < 1;
        }

        public List<PassRecovery> getUserRecoveries(int user_id)
        {
            List<PassRecovery> res = new List<PassRecovery>();
            foreach (PassRecovery p in all)
                if (p.USER_ID == user_id)
                    res.Add(p);
            return res;
        }
        public PassRecovery getActiveByUser(int user_id)
        {
            List<PassRecovery> rec = getUserRecoveries(user_id);
            foreach (PassRecovery p in rec)
                if (isActive(p.ID))
                    return p;
            return new PassRecovery(-1, -1, DateTime.Now, "");
        }

        public void addRecovery(int User_id, string New_pass)
        {
            string sql = "insert into passrecoveries(user_id, new_pass)" +
                "values (@User_id, @New_pass);";

            db.Save_Data(sql, new {User_id, New_pass}, connection);
            getAll();
        }
    }
}
