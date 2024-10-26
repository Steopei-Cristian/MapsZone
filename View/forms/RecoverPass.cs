using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View.inputs;
using FontAwesome.Sharp;
using Org.BouncyCastle.Asn1.Cms;
using PasswordGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class RecoverPass : CustomForm
    {
        private UserRepository userRep = new UserRepository();
        private PassRecoveryRepository passRecRep = new PassRecoveryRepository();

        private Label text = new Label();
        private UserInput ?email;
        private IconButton sendBtn = new IconButton();
        private TextBox passTxt = new TextBox();
        private IconButton recoverBtn = new IconButton();

        private string def = "Enter your new password";

        public RecoverPass(MainForm mainF, Control parent) : base(mainF, 3)
        {
            this.TopLevel = false;
            this.Parent = parent;

            this.Size = new Size(750, 750);
            this.Location = new Point((parent.Width - this.Width) / 2,
                (parent.Height - this.Height) / 2);
            this.FormBorderStyle = FormBorderStyle.None;

            this.BackColor = GeneralMethods.backColor;

            loadText();
            loadEmail();
            loadSendBtn();
            loadNewPassTxt();
            loadRecoverBtn();
        }

        private void loadText()
        {
            text.Parent = this;
            text.Size = new Size(500, 150);
            text.Location = new Point((this.Width - text.Width) / 2,
                150);

            text.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);
            text.Text = "You will receive a new password via email\n" +
                "IMPORTANT NOTE!\n" +
                "You must complete the New Password field within 1 minute.";
            text.ForeColor = GeneralMethods.foreColor;
            text.TextAlign = ContentAlignment.MiddleCenter;

            text.BorderStyle = BorderStyle.FixedSingle;

        }
        private void loadEmail()
        {
            email = new UserInput(3, this);
            email.Location = new Point((this.Width - email.Width) / 2,
                text.Location.Y + text.Height + 20);
        }

        private void loadSendBtn()
        {
            sendBtn.Parent = this;
            sendBtn.Size = new Size(400, 50);
            sendBtn.Location = new Point((this.Width - sendBtn.Width) / 2,
                email!.Location.Y + email.Height + 20);

            sendBtn.BackColor = GeneralMethods.foreColor;
            sendBtn.ForeColor = GeneralMethods.backColor;

            sendBtn.FlatAppearance.MouseOverBackColor = sendBtn.BackColor;
            sendBtn.FlatStyle = FlatStyle.Popup;
            sendBtn.TabStop = false;

            sendBtn.Text = "Get me my new password!";
            sendBtn.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);

            sendBtn.IconChar = IconChar.PaperPlane;
            sendBtn.IconColor = GeneralMethods.backColor;
            sendBtn.ImageAlign = ContentAlignment.MiddleLeft;

            sendBtn.Click += new EventHandler(this.sendBtn_Click);
        }
        private void sendBtn_Click(object? sender, EventArgs e)
        {
            User user = userRep.tryGetBYEmail(email!.TEXT.Text);

            if (user.ID == -1)
            {
                MessageBox.Show("Invalid email adress.\nCheck it again!");
            }
            else
            {
                PassRecovery p = passRecRep.getActiveByUser(user.ID);
                if (p.ID != -1)
                    MessageBox.Show("You already have an active password sent " +
                        "in your email inbox");
                else
                {
                    EmailSender emailSender = new EmailSender();

                    var pwd = new Password(8)
                        .IncludeLowercase()
                        .IncludeUppercase()
                        .IncludeNumeric();
                    string pass = pwd.Next();

                    emailSender.sendMail(user.EMAIL,
                        "Mapszone account new password",
                        "This is your new password: " + pass +
                        "\nEnter it in the app to recover your account");

                    passRecRep.addRecovery(user.ID, pass);
                }
            }
        }

        private void loadNewPassTxt()
        {
            passTxt.Parent = this;
            passTxt.Width = 300;
            passTxt.Location = new Point((this.Width - passTxt.Width) / 2,
                sendBtn.Location.Y + sendBtn.Height + 20);

            passTxt.Font = new Font(GeneralMethods.fontFam, 11, FontStyle.Bold);
            passTxt.Text = def;

            passTxt.TabStop = false;
            passTxt.BackColor = GeneralMethods.backColor;
            passTxt.ForeColor = Color.Gray;
            passTxt.BorderStyle = BorderStyle.None;

            passTxt.GotFocus += new EventHandler(this.passTxt_Focus);
            passTxt.LostFocus += new EventHandler(this.passTxt_LostFocus);
        }
        private void passTxt_Focus(object ?sender, EventArgs e)
        {
            if(passTxt.Text == def)
            {
                passTxt.PasswordChar = '*';
                passTxt.Text = "";
                passTxt.ForeColor = GeneralMethods.foreColor;
            }
        }
        private void passTxt_LostFocus(object ?sender, EventArgs e)
        {
            if(passTxt.Text == "")
            {
                passTxt.PasswordChar = '\0';
                passTxt.Text = def;
                passTxt.ForeColor = Color.Gray;
            }
        }

        private void loadRecoverBtn()
        {
            recoverBtn.Parent = this;
            recoverBtn.Size = sendBtn.Size;
            recoverBtn.Location = new Point((this.Width - recoverBtn.Width) / 2,
                passTxt.Location.Y + passTxt.Height + 20);

            recoverBtn.BackColor = GeneralMethods.foreColor;
            recoverBtn.ForeColor = GeneralMethods.backColor;

            recoverBtn.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);
            recoverBtn.Text = "Recover my account";

            recoverBtn.FlatStyle = FlatStyle.Popup;
            recoverBtn.FlatAppearance.MouseOverBackColor = recoverBtn.BackColor;
            recoverBtn.TabStop = false;

            recoverBtn.IconChar = IconChar.ArrowsSpin;
            recoverBtn.IconColor = GeneralMethods.backColor;
            recoverBtn.ImageAlign = ContentAlignment.MiddleLeft;

            recoverBtn.Click += new EventHandler(this.recoverBtn_Click);
        }
        private void recoverBtn_Click(object ?sender, EventArgs e)
        {
            string s = passTxt.Text;
            User u = userRep.tryGetBYEmail(email!.TEXT.Text);
            PassRecovery p = passRecRep.getActiveByUser(u.ID);

            if(p.ID == -1 || p.NEW_PASS != s)
            {
                MessageBox.Show("Please check again!");
            }
            else
            {
                TimeSpan t = DateTime.Now.Subtract(p.CREATED_AT);
                if (t.TotalMinutes < 1)
                {
                    userRep.modifyPass(u.USERNAME, p.NEW_PASS);
                    MessageBox.Show("Succesfully updated your password\nGo back to login and type it in");
                }
                else
                    MessageBox.Show("Session expired");
            }
        }
    }
}
