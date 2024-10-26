using Atestat1.Model;
using Atestat1.Repository;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Atestat1.View.forms
{
    internal class SettingsForm : Form
    {
        private User user;
        private UserRepository userRepo = new UserRepository();

        private IconButton changePass = new IconButton();
        private IconButton changeEmail = new IconButton();
        private IconButton close = new IconButton();

        private Panel next = new Panel();
        private int k = 0; 

        public SettingsForm(Control parent, User user)
        {
            this.TopLevel = false;
            this.Parent = parent;
            this.user = user;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = parent.Size;
            this.BackColor = GeneralMethods.backColor;

            loadCommon();
            loadChangeEmail();
            loadChangePass();
            loadClose();
        }

        private void loadCommon()
        {
            changeEmail = new IconButton();
            changePass = new IconButton();

            changePass.Parent = changeEmail.Parent = this;
            changeEmail.Size = changePass.Size = new Size(330, 60);

            changeEmail.TabStop = changePass.TabStop = false;
            changeEmail.BackColor = changePass.BackColor = GeneralMethods.foreColor;
            changePass.ForeColor = changeEmail.ForeColor = GeneralMethods.backColor;

            changeEmail.FlatStyle = changePass.FlatStyle = FlatStyle.Popup;
            changeEmail.FlatAppearance.BorderColor = changePass.FlatAppearance.BorderColor =
                changeEmail.FlatAppearance.MouseOverBackColor =
                changePass.FlatAppearance.MouseOverBackColor =
                changeEmail.FlatAppearance.MouseDownBackColor =
                changePass.FlatAppearance.MouseDownBackColor =
                GeneralMethods.foreColor;

            changeEmail.Font = changePass.Font = new Font(GeneralMethods.fontFam, 11,
                FontStyle.Bold);
            changeEmail.TextAlign = ContentAlignment.MiddleRight;

            changeEmail.ImageAlign = changePass.ImageAlign = ContentAlignment.MiddleLeft;
            changePass.IconColor = changeEmail.IconColor = GeneralMethods.backColor;
        }

        private void loadChangeEmail()
        {
            changeEmail.Location = new Point(this.Width / 2 - 340,
                (this.Height - changeEmail.Height) / 2);
            changeEmail.Text = "Change your email adress";
            changeEmail.IconChar = IconChar.Envelope;

            changeEmail.Click += new EventHandler(this.changeEmail_Click);
        }
        private void loadChangePass()
        {
            changePass.Location = new Point(this.Width / 2 + 10,
                (this.Height - changePass.Height) / 2);
            changePass.Text = "Change your password";
            changePass.IconChar = IconChar.Unlock;

            changePass.Click += new EventHandler(this.changePass_Click);
        }

        private void changeEmail_Click(object ?sender, EventArgs e)
        {
            k = 1;
            this.Controls.Clear();
            loadClose();

            loadNext();
        }
        private void changePass_Click(object? sender, EventArgs e)
        {
            k = 2;
            this.Controls.Clear();
            loadClose();

            loadNext();
        }

        private void loadClose()
        {
            close = new IconButton();
            close.Parent = this;
            close.Size = new Size(50, 50);
            close.Location = new Point(this.Width - 100, 20);

            close.BackColor = GeneralMethods.backColor;
            close.ForeColor = GeneralMethods.foreColor;

            close.TabStop = false;
            close.FlatAppearance.MouseOverBackColor =
                close.FlatAppearance.MouseDownBackColor =
                close.FlatAppearance.BorderColor = close.BackColor;
            close.FlatStyle = FlatStyle.Flat;

            close.IconChar = IconChar.CircleXmark;
            close.IconColor = close.ForeColor;
            close.IconSize = 48;

            close.Click += new EventHandler(this.close_Click);
        }
        private void close_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (update != null)
            {
                if (update.Text != "" && update.Text != s1 && update.Text != s2)
                {
                    DialogResult res = MessageBox.Show("Your input will be lost\n" +
                        "are your sure you want to exit?", "Confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (res == DialogResult.Yes)
                    {
                        e.Cancel = true;

                        update = null;
                        this.Controls.Remove(next);
                        loadCommon();
                        loadChangeEmail();
                        loadChangePass();
                    }

                    else
                        e.Cancel = true;
                }
                else if (next != null)
                {
                    e.Cancel = true;

                    update = null;
                    this.Controls.Remove(next);
                    loadCommon();
                    loadChangeEmail();
                    loadChangePass();
                }
            }
        }

        #region Next-related controls

        private Label current = new Label();
        private TextBox ?update;
        private string s1 = "Type your new email adress";
        private string s2 = "Type your new password";
        private IconButton confirm = new IconButton();

        private void loadNext()
        {
            next = new Panel();
            next.Parent = this;
            next.Size = new Size(500, 500);
            next.Location = new Point((this.Width - next.Width) / 2,
                (this.Height - next.Height) / 2);

            // next.BorderStyle = BorderStyle.FixedSingle;
            
            loadCurrent();
            loadUpdate();
            loadConfirm();

            paintUpdateBorder();
        }

        private void loadCurrent()
        {
            current = new Label();
            current.Parent = next;
            current.Size = new Size(400, 100);
            current.Location = new Point((next.Width - current.Width) / 2,
                150);

            current.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);
            current.ForeColor = GeneralMethods.foreColor;

            current.Text = "This is your current " +
                (k == 1 ? "email" : "password") + ": " +
                (k == 1 ? user.EMAIL : user.PASSWORD);
            current.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void loadUpdate()
        {
            update = new TextBox();
            update.Parent = next;
            update.Size = new Size(400, 40);
            update.Location = new Point((next.Width - update.Width) / 2,
                current.Location.Y + current.Height + 30);

            update.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);
            update.Text = k == 1 ? s1 : s2;
            update.ForeColor = GeneralMethods.foreColor;

            update.GotFocus += new EventHandler(this.focus);
            update.LostFocus += new EventHandler(this.lost_focus);
            update.BackColor = GeneralMethods.backColor;

            update.BorderStyle = BorderStyle.None;
        }
        private void paintUpdateBorder()
        {
            Pen p = new Pen(GeneralMethods.foreColor, 4);
            PaintEventArgs e = new PaintEventArgs(next.CreateGraphics(),
                new Rectangle(new Point(0, 0), next.Size));
            e.Graphics.DrawRectangle(p, new Rectangle(update.Location, update.Size));
        }
        private void focus(object ?sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender!;

            if((k == 1 && txt.Text == s1) 
                || (k == 2 && txt.Text == s2))
            {
                txt.Text = "";
                if (k == 2)
                    txt.PasswordChar = '*';
            }
        }
        private void lost_focus(object ?sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender!;

            if(txt.Text == "")
            {
                if (k == 1)
                    txt.Text = s1;
                else
                {
                    txt.PasswordChar = '\0';
                    txt.Text = s2;
                } 
                    
            }
        }
        
        private void loadConfirm()
        {
            confirm = new IconButton();
            confirm.Parent = next;
            confirm.Size = new Size(250, 50);
            confirm.Location = new Point((next.Width - confirm.Width) / 2,
                update.Location.Y + update.Height + 30);

            confirm.BackColor = GeneralMethods.foreColor;
            confirm.ForeColor = GeneralMethods.backColor;

            confirm.TabStop = false;
            confirm.FlatStyle = FlatStyle.Popup;
            confirm.FlatAppearance.BorderColor = confirm.FlatAppearance.MouseOverBackColor =
                confirm.FlatAppearance.MouseDownBackColor = GeneralMethods.foreColor;

            confirm.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);
            confirm.Text = "Apply changes";

            confirm.IconColor = GeneralMethods.backColor;
            confirm.ImageAlign = ContentAlignment.MiddleLeft;
            confirm.IconChar = IconChar.ClipboardCheck;

            confirm.Click += new EventHandler(confirm_Click);
        }
        private void confirm_Click(object ?sender, EventArgs e)
        {
            if(update!.Text == "" || update.Text == s1 || update.Text == s2)
            {
                MessageBox.Show("Please check your input again!");
            }
            else if(k == 1)
            {
                bool ok = GeneralMethods.okGmailAdress(update.Text);
                if (!ok)
                {
                    MessageBox.Show("Invalid email adress");
                }
                else
                {
                    userRepo.changeEmail(this.user.USERNAME, update.Text);
                    user = userRepo.tryGetByUsername(user.USERNAME);
                    MessageBox.Show("Email successfully changed");

                    update = null;
                    this.Controls.Remove(next);
                    loadCommon();
                    loadChangeEmail();
                    loadChangePass();
                }
            }
            else
            {
                userRepo.modifyPass(user.USERNAME, update.Text);
                user = userRepo.tryGetByUsername(user.USERNAME);
                MessageBox.Show("Password successfully changed");

                update = null;
                this.Controls.Remove(next);
                loadCommon();
                loadChangeEmail();
                loadChangePass();
            }
        }

        #endregion
    }
}
