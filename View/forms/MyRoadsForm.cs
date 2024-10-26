using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View.cards;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Google.Protobuf.WellKnownTypes;
using FontAwesome.Sharp;
using System.Runtime.CompilerServices;

namespace Atestat1.View.forms
{
    internal class MyRoadsForm : CustomForm
    {
        private int user_id;

        private Label text = new Label();

        private PrevTripRepository tripRepo = new PrevTripRepository();
        private List<PrevTrip> trips = new List<PrevTrip>();
        private List<TripCard> cards = new List<TripCard>();

        private int y = 50, k = 0;

        private IconButton ?more = new IconButton();

        private IconButton close = new IconButton();
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        public MyRoadsForm(MainForm mainF, Control parent, int User_id) : base(mainF, 6)
        {
            this.TopLevel = false;
            this.Parent = parent;
            user_id = User_id;

            this.BackColor = GeneralMethods.backColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(parent.Width - 86, 360);
            this.Location = new Point(43, (Parent.Height - this.Height) / 2 - 50);
            this.AutoScroll = true;

            trips = tripRepo.getByUser(user_id);
            if(trips.Count == 0)
            {
                Label msg = new Label();
                msg.Parent = this;
                msg.Size = new Size(400, 350);
                msg.Location = new Point((this.Width - msg.Width) / 2,
                    (this.Height - msg.Height) / 2);

                msg.TextAlign = ContentAlignment.MiddleCenter;
                msg.ForeColor = GeneralMethods.foreColor;
                msg.Font = new Font(GeneralMethods.fontFam, 13, FontStyle.Bold);
                msg.Text = "You don't have any past trips\n" +
                    "Why don't you create some? ;)";
            }
            else
            {
                loadText();
                y = text.Location.Y + text.Height + 30;
                for (k = 0; k < 3 && k < trips.Count; k++)
                {
                    TripCard card = new TripCard(trips[trips.Count - k - 1], this);
                    card.VIEW.Click += new EventHandler(this.card_Click);
                    cards.Add(card);
                    if (k > 0)
                        card.Location = new Point((this.Width - card.Width) / 2,
                            cards[k - 1].Location.Y + cards[k - 1].Height + 10);
                    else card.Location = new Point((this.Width - card.Width) / 2,
                            y);
                }

                loadMore();

                t.Enabled = true;
                t.Interval = 10;
                t.Tick += new EventHandler(this.t_Tick);
            }

            loadClose();
        }

        private void loadText() 
        {
            text.Parent = this;
            text.Size = new Size(350, 75);
            text.Location = new Point((this.Width - text.Width) / 2, 0);

            text.Font = new Font(GeneralMethods.fontFam, 11, FontStyle.Bold);
            text.TextAlign = ContentAlignment.MiddleCenter;
            text.Text = "These are your trips until now\n" +
                "Tap on any of them to visualize it!";
            text.ForeColor = GeneralMethods.foreColor;
        }
        
        private void loadMore()
        {
            more = null;
            more = new IconButton();
            more.Parent = this;
            more.Size = new Size(230, 50);
            more.Location = new Point((this.Width - more.Width) / 2,
                cards[k-1].Location.Y + cards[k-1].Height + 30);

            more.BackColor = GeneralMethods.foreColor;
            more.ForeColor = GeneralMethods.backColor;

            more.FlatStyle = FlatStyle.Popup;
            more.FlatAppearance.BorderColor = more.FlatAppearance.MouseOverBackColor =
                more.FlatAppearance.MouseDownBackColor = GeneralMethods.foreColor;

            more.Font = new Font(GeneralMethods.fontFam, 10, FontStyle.Bold);
            more.Text = "Load more...";

            more.IconChar = IconChar.ListCheck;
            more.IconColor = GeneralMethods.backColor;
            more.ImageAlign = ContentAlignment.MiddleLeft;

            more.Click += new EventHandler(this.more_Click);
        }
        private void more_Click(object ?sender, EventArgs e)
        {
            this.Controls.Remove(more);
            int aux = k;

            for(int i = aux; i<= aux + 2 && i < trips.Count; i++)
            {
                TripCard card = new TripCard(trips[trips.Count - k - 1], this);
                card.VIEW.Click += new EventHandler(this.card_Click);
                cards.Add(card);
                card.Location = new Point((this.Width - card.Width) / 2, 
                    cards[i-1].Location.Y + cards[i-1].Height + 10);
                k++;
            }

            if (k < trips.Count)
                loadMore();   
        }

        private void loadClose()
        {
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
            this.t.Enabled = false;
        }
        private void t_Tick(object ?sender, EventArgs e)
        {
            close.Top = 10;
        }

        private void card_Click(object ?sender, EventArgs e)
        {
            TripCardIconButton b = (TripCardIconButton)sender!;
            TripCard card = b.TRIP;

            MapForm map = new MapForm(user_id, this.Main, this.Parent, 
                true, card.TRIP.START, card.TRIP.END);
            map.Prev = this;
            map.FormClosed += new FormClosedEventHandler(this.map_Closed);

            this.Hide();
            openMap();
            map.Show();
        }
        private void openMap()
        {
            this.Main.TOP.Height = 0;

            this.Main.CONTENT.Size = this.Main.Size;
            this.Main.CONTENT.Location = new Point(0, 0);

            this.Parent.Parent.Size = Main.CONTENT.Size;
            this.Parent.Parent.Location = new Point(0, 0);

            this.Parent.Size = this.Parent.Parent.Size;
            this.Parent.Location = new Point(0, 0);

            this.Location = new Point(0, 0);
        }
        private void map_Closed(object ?sender, FormClosedEventArgs e)
        {
            this.Main.TOP.Height = 150;

            this.Main.CONTENT.Size = new Size(Main.Width, Main.Height - Main.TOP.Height);
            this.Main.CONTENT.Location = new Point(0, Main.TOP.Height);

            this.Parent.Parent.Location = new Point(0, 0);
            this.Parent.Parent.Size = Main.CONTENT.Size;

            this.Parent.Location = new Point(0, 0);
            this.Parent.Size = this.Parent.Parent.Size;

            this.Size = new Size(Parent.Width - 86, 360);
            this.Location = new Point(43, (this.Parent.Height - this.Height) / 2);
        }
    }
}
