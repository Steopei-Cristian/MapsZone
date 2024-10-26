using Atestat1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Repository
{
    internal class CityRepository : Repository<City>
    {
        private Dictionary<string, int> idByName = new Dictionary<string, int>();

        public Dictionary<string, int> ID_BY_NAME { get => this.idByName; }

        public CityRepository()
        {
            getAll();
        }

        protected override void getAll()
        {
            string sql = "select * from cities";
            all = db.Load_Data<City, dynamic>(sql, new { }, connection);
        }

        public override void printAll()
        {
            foreach (City city in all)
                Debug.WriteLine(city.ToString());
        }

        public List<string> getNames()
        {
            List<string> res = new List<string>(1001);
            foreach (var x in all)
                res.Add(x.CITY_NAME);
            return res;
        }

        public bool contains(string name, out int val)
        {
            bool found = idByName.ContainsKey(name);
            if(found && idByName[name] != 0)
            {
                val = idByName[name];
                return true;
            }
            foreach(City c in all)
                if (c.CITY_NAME.Equals(name))
                {
                    idByName.Add(name, c.ID);
                    val = idByName[name];
                    return true;
                }
            val = -1;
            return false;
        }

        public City getById(int id)
        {
            foreach (var x in all)
                if (x.ID == id)
                    return x;
            return new City(-1, "", 0, 0, null);
        }
    }
}
