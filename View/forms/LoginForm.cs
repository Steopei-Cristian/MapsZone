using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View.inputs;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class LoginForm : CustomForm
    {
        private UserRepository users = new UserRepository();

        private Label title = new Label();
        private UserInput userInput;
        private UserInput passInput;
        private Label forgot = new Label();
        private IconButton button = new IconButton();

        public LoginForm(MainForm mainF, Control parent) : base(mainF, 1)
        {
            TopLevel = false;
            Parent = parent;

            FormBorderStyle = FormBorderStyle.None;
            Size = new Size(500, 500);
            Location = new Point((parent.Width - Width) / 2,
                (parent.Height - Height) / 2);
            BackColor = GeneralMethods.backColor;

            loadTitle();

            userInput = new UserInput(1, this);
            userInput.Location = new Point((Width - userInput.Width) / 2,
                title.Location.Y + title.Height + 25);

            passInput = new UserInput(2, this);
            passInput.Location = new Point(userInput.Location.X,
                userInput.Location.Y + userInput.Height + 25);

            loadForgotPass();
            loadButton();
        }


        private void loadTitle()
        {
            title.Parent = this;
            title.Size = new Size(400, 75);
            title.Location = new Point((Width - title.Width) / 2, 25);
            title.Text = "ENTER YOUR DETAILS AND \nLOGIN INTO YOUR ACCOUNT!";
            title.Text = "E" + title.Text[1..].ToLower();
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);
            title.ForeColor = GeneralMethods.foreColor;
        }

        private void loadForgotPass()
        {
            forgot.Parent = this;
            forgot.Size = new Size(200, 40);
            forgot.Location = new Point(
                (passInput.Location.X + passInput.Width / 2 - 60),
                (passInput.Location.Y + passInput.Height + 20));

            forgot.Font = new Font(GeneralMethods.fontFam, 8, FontStyle.Underline);
            forgot.Text = "Forgot your password?\nRecover it here!";
            forgot.ForeColor = GeneralMethods.foreColor;
            forgot.TextAlign = ContentAlignment.MiddleRight;

            forgot.Click += new EventHandler(this.forgot_Click);
        }
        private void forgot_Click(object ?sender, EventArgs e)
        {
            RecoverPass recover = new RecoverPass(this.Main, this.Parent);
            this.Main.AddChild(recover);
        }

        private void loadButton()
        {
            button.Parent = this;
            button.Size = new Size(180, 50);
            button.Location = new Point((Width - button.Width) / 2,
                forgot.Location.Y + forgot.Height + 20);

            button.ForeColor = GeneralMethods.backColor;
            button.BackColor = GeneralMethods.foreColor;

            button.FlatAppearance.CheckedBackColor = GeneralMethods.foreColor;
            button.FlatStyle = FlatStyle.Popup;
            button.TabStop = false;

            button.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);
            button.Text = "Login";

            button.IconChar = IconChar.ArrowRightToBracket;
            button.IconColor = button.ForeColor;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            

            button.Click += new EventHandler(button_Click);
        }
        private void button_Click(object? sender, EventArgs e)
        {
            string username = userInput.TEXT.ForeColor == Color.Gray ? "" : userInput.TEXT.Text;
            string pass = userInput.TEXT.ForeColor == Color.Gray ? "" : passInput.TEXT.Text;

            User user = users.tryGetByUsername(username);

            if (user.ID == -1 || user.PASSWORD != pass)
                MessageBox.Show("Please check your details again!");
            else
            {
                HomeForm home = new HomeForm(this.Main, this.Parent, user);
                home.FormClosed += new FormClosedEventHandler(this.home_FormClosed);
                this.Main.AddChild(home);
            }


        }
        private void home_FormClosed(object ?sender, FormClosedEventArgs e)
        {
            this.Main.RemoveChild();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen p = new Pen(GeneralMethods.foreColor, 2);
            drawBorder(1, p, e);
            drawBorder(2, p, e);
            drawBorder(3, p, e);
        }
        private void drawBorder(int k, Pen p, PaintEventArgs e)
        {
            if (k == 1)
                e.Graphics.DrawRectangle(p, new Rectangle(userInput.Location, userInput.Size));
            else if (k == 2)
                e.Graphics.DrawRectangle(p, new Rectangle(passInput.Location, passInput.Size));
        }

        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if((userInput.TEXT.Text != "" && userInput.TEXT.Text != userInput.T1)
               || (passInput.TEXT.Text != "" && passInput.TEXT.Text != passInput.T2))
            {
                DialogResult res = MessageBox.Show("All the data will be lost\n" +
                    "Are you sure you want to exit?", "Confirmation",
                    MessageBoxButtons.YesNo);

                if (res == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                    e.Cancel = false;
            }
        }
        
    }
}
