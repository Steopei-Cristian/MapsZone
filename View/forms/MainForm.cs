using Atestat1.Model;
using Atestat1.Repository;
using FontAwesome.Sharp;
using K4os.Compression.LZ4.Engine;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class MainForm : Form
    {
        private string logoPath = Application.StartupPath + 
            "\\LOGO\\logo_transp.png";

        private Panel top = new Panel();
        private Panel content = new Panel();

        public Panel TOP { get => this.top; set => this.top = value; }
        public Panel CONTENT { get => this.content; set => this.content = value; }

        public MainForm()
        {
            Size = new Size(1920, 1010);
            this.MaximumSize = this.MinimumSize = this.Size;
            // this.FormBorderStyle = FormBorderStyle.None;
            CenterToScreen();
            BackColor = GeneralMethods.backColor;

            loadLayout();

            //UserRepository userRep = new UserRepository();
            //User u = userRep.tryGetByUsername("cristi1");
            //AddChild(new HomeForm(this, content, u));
        }

        private void loadLayout()
        {
            top.Parent = this;
            top.Size = new Size(Width, 250);
            top.BorderStyle = BorderStyle.FixedSingle;

            content.Parent = this;
            content.Size = new Size(Width, Height - top.Height);
            content.Location = new Point(0, top.Height);
            content.Top = top.Height;
            // content.BackColor = Color.Gray;

            loadTop();
            loadContent();
        }

        #region Top-related

        private PictureBox logo = new PictureBox();
        private IconButton back = new IconButton();

        private void loadTop()
        {
            loadLogo();
            loadBack();
        }

        private void loadLogo()
        {
            logo.Parent = top;
            logo.Size = new Size(300, 240);
            logo.Location = new Point((top.Width - logo.Width) / 2,
                (top.Height - logo.Height) / 2);

            logo.Image = Image.FromFile(logoPath);
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        
        private void loadBack()
        {
            back.Parent = top;
            back.Size = new Size(50, 50);
            back.Location = new Point(50, (top.Height - back.Height) / 2);

            back.BackColor = GeneralMethods.backColor;

            back.FlatStyle = FlatStyle.Flat;
            back.FlatAppearance.MouseOverBackColor = back.BackColor;
            back.FlatAppearance.BorderColor = back.BackColor;
            back.FlatAppearance.MouseDownBackColor = GeneralMethods.roadColor;
            back.TabStop = false;

            back.IconChar = IconChar.Backward;
            back.ImageAlign = ContentAlignment.MiddleCenter;
            back.IconColor = GeneralMethods.foreColor;

            back.Click += new EventHandler(this.back_Click);
            back.Visible = false;
        }
        private void back_Click(object ?sender, EventArgs e)
        {
            RemoveChild();
        }

        #endregion

        #region Content-related

        private CustomForm[] childs = new CustomForm[1001];
        private int k = 0, i = 0;

        private Label text = new Label();
        private IconButton loginBtn = new IconButton();
        private IconButton signUpBtn = new IconButton();

        private void loadContent()
        {
            loadText();
            loadLoginBtn();
            loadSignUpBtn();
        }

        private void loadText()
        {
            text.Parent = content;
            text.Size = new Size(600, 150);
            text.Location = new Point((content.Width - text.Width) / 2,
                100);

            text.TextAlign = ContentAlignment.MiddleCenter;
            text.Font = new Font(GeneralMethods.fontFam, 15, FontStyle.Bold);
            text.Text = "Welcome to MapsZone!\n" +
                "Login to start planning your trip!\n" +
                "Don't have an account? Sign up for free!";

            text.ForeColor = GeneralMethods.foreColor;
        }

        private void loadLoginBtn()
        {
            loginBtn = new IconButton();
            loginBtn.Parent = content;
            loginBtn.Size = new Size(200, 50);
            loginBtn.Location = new Point(
                text.Location.X + (text.Width / 2 - loginBtn.Width - 5),
                text.Location.Y + text.Height + 10);

            loginBtn.ForeColor = GeneralMethods.backColor;
            loginBtn.BackColor = GeneralMethods.foreColor;

            loginBtn.FlatStyle = FlatStyle.Popup;
            loginBtn.TabStop = false;

            loginBtn.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);
            loginBtn.Text = "Login";

            loginBtn.IconChar = IconChar.ArrowRightToBracket;
            loginBtn.IconColor = GeneralMethods.backColor;
            loginBtn.ImageAlign = ContentAlignment.MiddleLeft;

            loginBtn.Click += new EventHandler(loginBtn_Click);
        }
        private void loginBtn_Click(object? sender, EventArgs e)
        {
            content.Controls.Clear();

            LoginForm login = new LoginForm(this, content);
            login.FormClosed += new FormClosedEventHandler(this.loginOrsignup_FormClosed);

            AddChild(login);
        }
        private void loginOrsignup_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (i == 1)
            {
                back.Visible = false;
                loadContent();
                k--;
                i--;
            }
        }

        private void loadSignUpBtn()
        {
            signUpBtn = new IconButton();
            signUpBtn.Parent = content;
            signUpBtn.Size = new Size(200, 50);
            signUpBtn.Location = new Point(text.Location.X + text.Width / 2 + 5,
                loginBtn.Location.Y);

            signUpBtn.BackColor = GeneralMethods.foreColor;
            signUpBtn.ForeColor = GeneralMethods.backColor;

            signUpBtn.Text = "Sign up";
            signUpBtn.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);

            signUpBtn.TabStop = false;
            signUpBtn.FlatStyle = FlatStyle.Popup;

            signUpBtn.IconChar = IconChar.UserPlus;
            signUpBtn.ImageAlign = ContentAlignment.MiddleLeft;
            signUpBtn.IconColor = GeneralMethods.backColor;

            signUpBtn.Click += new EventHandler(this.signUpBtn_Click);
        }
        private void signUpBtn_Click(object ?sender, EventArgs e)
        {
            content.Controls.Clear();

            SignUpForm signUp = new SignUpForm(this, content);
            signUp.FormClosed += new FormClosedEventHandler(this.loginOrsignup_FormClosed);

            AddChild(signUp);
        }
        


        public void AddChild(CustomForm f)
        {
            childs[++k] = f;
            i = k;
            if(i > 1)
                childs[i - 1].Hide();
            
            changeLayoutBasedOnChild();
            childs[i].Show();
        }
        public void RemoveChild()
        {
            if (i > 1)
            {
                childs[i].Hide();
                k--;
                i--;
                changeLayoutBasedOnChild();
                childs[i].Show();
            }
            else
            {
                childs[i].Close();
            }
        }

        private void changeLayoutBasedOnChild()
        {
            // MessageBox.Show(childs[i].ToString());
            int lv = childs[i].Level;

            if (lv > 0 && lv < 4)
                back.Visible = true;
            
            if(lv == 1 || lv == 2)
            {
                top.Height = 250;
                loadLogo();
                back.Location = new Point(back.Location.X, (top.Height - back.Height) / 2);

                content.Size = new Size(this.Width, this.Height - top.Height);
                content.Location = new Point(0, top.Height);
            }
            else if(lv == 4)
            {
                top.Height = 150;

                logo.Size = new Size(180, 140);
                logo.Location = new Point((top.Width - logo.Width) / 2,
                    (top.Height - logo.Height) / 2);

                back.Location = new Point(50, (top.Height - back.Height) / 2);

                content.Size = new Size(this.Width, this.Height - top.Height);
                content.Location = new Point(0, top.Height);

                back.Visible = false;
            }
        }

        #endregion
    }
}
