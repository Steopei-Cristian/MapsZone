using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class PassRecovery
    {
        private int id;
        private int user_id;
        private DateTime created_at;
        private string new_pass;

        public int ID { get => this.id; }
        public int USER_ID { get => this.user_id; }
        public DateTime CREATED_AT { get => this.created_at; }
        public string NEW_PASS { get => this.new_pass; }

        public PassRecovery(int Id, int User_id, DateTime Created_at, string New_pass)
        {
            this.id = Id;
            this.user_id = User_id;
            this.created_at = Created_at;
            this.new_pass = New_pass;
        }

        public override string ToString()
        {
            return id.ToString() + "|" + user_id.ToString() + "|" + 
                created_at.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }
}
