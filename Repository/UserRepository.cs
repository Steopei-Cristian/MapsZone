using Atestat1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Repository
{
    internal class UserRepository : Repository<User>
    {
        public UserRepository()
        {
            getAll();
        }

        protected override void getAll()
        {
            string sql = "select * from users";
            all = db.Load_Data<User, dynamic>(sql, new { }, this.connection);
        }

        public override void printAll()
        {
            foreach (User user in all)
                Debug.WriteLine(user.ToString());
        }

        public User tryGetByUsername(string username)
        {
            getAll();
            foreach (User user in all)
                if (user.USERNAME == username)
                    return user;
            return new User(-1, "", "", "");
        }

        public User tryGetBYEmail(string email)
        {
            foreach (User user in all)
                if (user.EMAIL == email)
                    return user;
            return new User(-1, "", "", "");
        }

        public void modifyPass(string Username, string New_pass)
        {
            User u = tryGetByUsername(Username);
            if(u.ID != -1)
            {
                string sql = "update users set password = @New_pass " +
                    "where username = @Username";
                db.Save_Data(sql, new { Username, New_pass}, connection);
                getAll();
            }
        }

        public void AddUser(string Username, string Password, string Email)
        {
            string sql = "insert into users (username, password, email)" +
                "values (@Username, @Password, @Email)";
            db.Save_Data(sql, new { Username, Password, Email }, connection);
            getAll();
        }

        public void changeEmail(string Username, string NewEmail)
        {
            string sql = "update users set email = @NewEmail where username = @Username";
            db.Save_Data(sql, new { Username, NewEmail }, connection);
            getAll();
        }
    }
}
