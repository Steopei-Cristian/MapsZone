using DataAccess.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Repository
{
    abstract class Repository<T>
    {
        protected string connection = GeneralMethods.getConnectionString();
        protected MySqlDataAccess db = new MySqlDataAccess();
        protected List<T> all = new List<T>();

        public List<T> ALL { get => this.all; }

        protected virtual void getAll() { }
        public virtual void printAll() { }

    }
}
