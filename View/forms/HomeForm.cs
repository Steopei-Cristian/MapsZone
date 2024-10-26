using Atestat1.Model;
using Atestat1.Repository;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class HomeForm : CustomForm
    {
        private User user;
        private UserRepository userRep = new UserRepository();

        private Panel aside = new Panel();
        private Panel content = new Panel();


        public HomeForm(MainForm mainF, Control parent, User u) : base(mainF, 4)
        {
            this.TopLevel = false;
            this.Parent = parent;
            this.user = u;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(parent.Width, parent.Height + 100);
            this.BackColor = GeneralMethods.backColor;

            loadAside();
            loadContent();
        }

        #region Aside-related controls

        private IconButton profile = new IconButton();
        private IconButton myRoads = new IconButton();
        private IconButton settings = new IconButton();
        private IconButton logout = new IconButton();

        private void loadAside()
        {
            aside.Parent = this;
            aside.Size = new Size(400, this.Height);

            aside.BorderStyle = BorderStyle.FixedSingle;

            loadProfile();
            loadMyRoads();
            loadSettings();
            loadLogout();
        }
        
        private void loadProfile()
        {
            profile.Parent = aside;
            profile.Size = new Size(aside.Width - 3, 50);
            profile.Location = new Point(0, 200);

            profile.BackColor = GeneralMethods.backColor;
            profile.ForeColor = GeneralMethods.foreColor;

            profile.TabStop = false;
            profile.FlatAppearance.MouseDownBackColor = profile.BackColor;
            profile.FlatStyle = FlatStyle.Flat;
            profile.FlatAppearance.MouseOverBackColor = profile.BackColor;
            profile.FlatAppearance.BorderColor = profile.BackColor;

            profile.Font = new Font(GeneralMethods.fontFam, 12, FontStyle.Bold);
            profile.Text = "Welcome, " + user.USERNAME;
            profile.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void loadMyRoads()
        {
            myRoads = new IconButton();
            myRoads.Parent = aside;
            myRoads.Size = new Size(250, 50);
            myRoads.Location = new Point((aside.Width - myRoads.Width) / 2,
                profile.Location.Y + profile.Height + 100);

            myRoads.BackColor = GeneralMethods.backColor;
            myRoads.ForeColor = GeneralMethods.foreColor;

            myRoads.TabStop = false;
            myRoads.FlatAppearance.MouseOverBackColor = myRoads.BackColor;
            myRoads.FlatAppearance.BorderColor = GeneralMethods.foreColor;
            myRoads.FlatStyle = FlatStyle.Flat;

            myRoads.Font = new Font(GeneralMethods.fontFam, 15, FontStyle.Bold);
            myRoads.Text = "My roads";

            myRoads.IconChar = IconChar.RoadCircleCheck;
            myRoads.ImageAlign = ContentAlignment.MiddleLeft;
            myRoads.IconColor = GeneralMethods.foreColor;

            myRoads.Click += new EventHandler(myRoads_Click);
        }
        private void myRoads_Click(object ?sender, EventArgs e)
        {
            openRoadsForm();
            content.Controls.Clear();

            MyRoadsForm myRoads = new MyRoadsForm(this.Main, content, user.ID);
            myRoads.Show();
            myRoads.FormClosed += new FormClosedEventHandler(this.roadsForm_Close);
        }
        private void openRoadsForm()
        {
            aside.Width = 0;
            content.Size = this.Size;
            content.Location = new Point(0, 0);
        }
        private void roadsForm_Close(object ?sender, FormClosedEventArgs e)
        {
            aside.Width = 400;
            content.Size = new Size(this.Width - aside.Width, this.Height);
            content.Location = new Point(aside.Width, 0);
            loadContent();
        }

        private void loadSettings()
        {
            settings = new IconButton();
            settings.Parent = aside;
            settings.Size = myRoads.Size;
            settings.Width += 20;
            settings.Location = new Point(myRoads.Location.X - 10,
                myRoads.Location.Y + myRoads.Height + 30);

            settings.BackColor = GeneralMethods.backColor;
            settings.ForeColor = GeneralMethods.foreColor;

            settings.TabStop = false;
            settings.FlatAppearance.MouseOverBackColor = settings.BackColor;
            settings.FlatStyle = FlatStyle.Flat;
            settings.FlatAppearance.BorderColor = GeneralMethods.foreColor;

            settings.Font = myRoads.Font;
            settings.Text = "My settings";

            settings.IconChar = IconChar.Gears;
            settings.IconColor = settings.ForeColor;
            settings.ImageAlign = ContentAlignment.MiddleLeft;

            settings.Click += new EventHandler(this.settings_Click);
        }
        private void settings_Click(object ?sender, EventArgs e)
        {
            openRoadsForm();
            content.Controls.Clear();

            SettingsForm settings = new SettingsForm(content, 
                userRep.tryGetByUsername(user.USERNAME));
            settings.FormClosed += new FormClosedEventHandler(this.roadsForm_Close);
            settings.Show();
        }




        private void loadLogout()
        {
            logout = new IconButton();
            logout.Parent = aside;
            logout.Size = myRoads.Size;
            logout.Location = new Point(myRoads.Location.X,
                settings.Location.Y + settings.Height + 30);

            logout.BackColor = GeneralMethods.backColor;
            logout.ForeColor = GeneralMethods.foreColor;

            logout.TabStop = false;
            logout.FlatStyle = FlatStyle.Flat;
            logout.FlatAppearance.MouseOverBackColor = logout.BackColor;
            logout.FlatAppearance.BorderColor = GeneralMethods.foreColor;

            logout.Font = myRoads.Font;
            logout.Text = "Logout";

            logout.IconChar = IconChar.ArrowRightFromBracket;
            logout.ImageAlign = ContentAlignment.MiddleLeft;
            logout.IconColor = GeneralMethods.foreColor;

            logout.Click += new EventHandler(this.logOut_Click);
        }
        private void logOut_Click(object ?sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Content-related contents

        private Label text = new Label();
        private IconButton create = new IconButton();

        private void loadContent()
        {
            content.Parent = this;
            content.Size = new Size(this.Width - aside.Width, this.Height);
            content.Location = new Point(aside.Width, 0);

            loadText();
            loadCreate();
        }

        private void loadText()
        {
            text.Parent = content;
            text.Size = new Size(300, 100);
            text.Location = new Point((content.Width - text.Width) / 2,
                200);

            text.Font = new Font(GeneralMethods.fontFam, 15, FontStyle.Bold);
            text.Text = "Bored?\nStart another trip!";
            text.TextAlign = ContentAlignment.MiddleCenter;

            text.ForeColor = GeneralMethods.foreColor;
        }

        private void loadCreate()
        {
            create = new IconButton();
            create.Parent = content;
            create.Size = new Size(200, 200);
            create.Location = new Point((content.Width - create.Width) / 2,
                text.Location.Y + text.Height + 20);

            create.BackColor = GeneralMethods.backColor;

            create.TabStop = false;
            create.FlatAppearance.BorderColor =
                create.FlatAppearance.MouseOverBackColor = 
                create.FlatAppearance.MouseDownBackColor = create.BackColor;
            create.FlatStyle = FlatStyle.Flat;

            create.IconChar = IconChar.SquarePlus;
            create.IconColor = GeneralMethods.foreColor;
            create.IconSize = 180;

            create.Click += new EventHandler(this.create_Click);
        }
        private void create_Click(object ?sender, EventArgs e)
        {
            openMap();
            MapForm map = new MapForm(user.ID, Main, content, false, 0, 0);
            map.FormClosed += new FormClosedEventHandler(this.map_FormClosed);
            map.Show();
        }

        private void openMap()
        {
            content.Controls.Clear();
            aside.Width = 0;

            Main.TOP.Height = 0;

            Main.CONTENT.Size = Main.Size;
            Main.CONTENT.Location = new Point(0, 0);

            this.Size = this.Parent.Size;
            this.Location = new Point(0, 0);

            content.Size = this.Size;
            content.Location = new Point(0, 0);
        }
        private void map_FormClosed(object? sender, FormClosedEventArgs e)
        {
            aside.Width = 400;

            Main.TOP.Height = 150;

            Main.CONTENT.Size = new Size(Main.Width, Main.Height - Main.TOP.Height);
            Main.CONTENT.Location = new Point(0, Main.TOP.Height);

            this.Size = this.Parent.Size;
            this.Location = new Point(0, 0);

            content.Size = new Size(this.Width - aside.Width, this.Height);
            content.Location = new Point(0, 0);
            loadContent();
        }

        #endregion
    }
}
