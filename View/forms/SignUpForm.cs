using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View.inputs;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class SignUpForm : CustomForm
    {
        private UserRepository userRep = new UserRepository();

        private Label text = new Label();
        private UserInput username;
        private UserInput pass;
        private UserInput passOk;
        private UserInput mail;
        private IconButton confirm = new IconButton();

        
        public SignUpForm(MainForm mainF, Control parent) : base(mainF, 2)
        {
            this.TopLevel = false;
            this.Parent = parent;

            this.Size = new Size(500, 500);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point((parent.Width - this.Width) / 2,
                (parent.Height - this.Height) / 2);
            this.BackColor = GeneralMethods.backColor;

            loadText();

            username = new UserInput(1, this);
            username.Location = new Point((this.Width - username.Width) / 2, 100);
            
            pass = new UserInput(2, this);
            pass.Location = new Point((this.Width - pass.Width) / 2,
                username.Location.Y + username.Height + 25);
            
            passOk = new UserInput(2, this);
            passOk.Location = new Point((this.Width - passOk.Width) / 2,
                pass.Location.Y + pass.Height + 25);
            passOk.T2 = "Confirm your password";
            passOk.Conf = 1;
            
            mail = new UserInput(3, this);
            mail.Location = new Point((this.Width - mail.Width) / 2,
                passOk.Location.Y + passOk.Height + 25);


            loadButton();
        }

        private void loadText()
        {
            text.Parent = this;
            text.Size = new Size(450, 100);
            text.Location = new Point((this.Width - text.Width) / 2, 0);
            text.Text = "Create your account!" +
                "\n\t*We only work with Gmail accounts";
            text.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);
            text.ForeColor = GeneralMethods.foreColor;
            text.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void loadButton()
        {
            confirm.Parent = this;
            confirm.Size = new Size(250, 75);
            confirm.Location = new Point((this.Width - confirm.Width) / 2,
                mail.Location.Y + mail.Height + 50);

            confirm.BackColor = GeneralMethods.foreColor;
            confirm.ForeColor = GeneralMethods.backColor;

            confirm.Text = "Register me!";
            confirm.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);

            confirm.TabStop = false;
            confirm.FlatStyle = FlatStyle.Popup;
            confirm.IconChar = IconChar.CheckCircle;
            confirm.IconColor = confirm.ForeColor;
            confirm.ImageAlign = ContentAlignment.MiddleLeft;

            confirm.Click += new EventHandler(this.confirm_Click);
        }
        private void confirm_Click(object ?sender, EventArgs e)
        {
            User u1 = userRep.tryGetByUsername(username.TEXT.Text);
            User u2 = userRep.tryGetBYEmail(mail.TEXT.Text);

            //MessageBox.Show(u1.ToString());
            //MessageBox.Show(u2.ToString());

            string msg = "Please check the following fields:\n\n", aux = msg;

            if (u1.ID != -1)
            {
                username.TEXT.ForeColor = Color.Red;
                msg += "Username - already used\n";
            }
            else if(username.TEXT.Text == username.T1)
            {
                username.TEXT.ForeColor = Color.Red;
                msg += "Username - empty\n";
            }

            if (u2.ID != -1)
            {
                mail.TEXT.ForeColor = Color.Red;
                msg += "Email - already used\n";
            }
            else if(mail.TEXT.Text == mail.T3)
            {
                mail.TEXT.ForeColor = Color.Red;
                msg += "Email - empty\n";
            }
            else if(GeneralMethods.okGmailAdress(mail.TEXT.Text) == false)
            {
                mail.TEXT.ForeColor = Color.Red;
                msg += "Email - invalid\n";
            }

            if(pass.TEXT.Text == pass.T2)
            {
                pass.TEXT.ForeColor = Color.Red;
                msg += "Password - empty";
            }
            else if (pass.TEXT.Text != passOk.TEXT.Text)
            {
                pass.TEXT.ForeColor = Color.Red;
                passOk.TEXT.ForeColor = Color.Red;
                msg += "Password - not matching";
            }

            if(msg != aux)
            {
                MessageBox.Show(msg, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                userRep.AddUser(username.TEXT.Text, pass.TEXT.Text, mail.TEXT.Text);
                HomeForm home = new HomeForm(this.Main, this.Parent,
                    userRep.tryGetByUsername(username.TEXT.Text));
                home.FormClosed += new FormClosedEventHandler(this.home_FormClosed);
                Main.AddChild(home);
            }
        }

        private void home_FormClosed(object ?sender, FormClosedEventArgs e)
        {
            this.Main.RemoveChild();
        }
    }
}
