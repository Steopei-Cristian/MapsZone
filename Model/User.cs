using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class User
    {
        private int id;
        private string username;
        private string password;
        private string email;

        public int ID { get => this.id; }
        public string USERNAME { get => this.username; }
        public string PASSWORD { get => this.password; set => this.PASSWORD = value; }
        public string EMAIL { get => this.email; }

        public User(int Id, string Username, string Password, string Email)
        {
            this.id = Id;
            this.username = Username;
            this.password = Password;
            this.email = Email;
        }

        public override string ToString()
        {
            return this.id.ToString() + " | " + this.username; 
        }
    }
}
