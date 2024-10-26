using Atestat1.Repository;
using DataStructures;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Atestat1.Model;
using System.Collections.Specialized;
using Atestat1.View.inputs;
using FontAwesome.Sharp;
using System.Net.WebSockets;
using System.ComponentModel;
using Atestat1.View.cards;

namespace Atestat1.View.forms
{
    internal class MapForm : CustomForm
    {
        private int user_id;

        private CityRepository cityrep = new CityRepository();
        private PointRepository pointrep = new PointRepository();

        private CityCard[] cards = new CityCard[1001];

        private WeightedGraph graph = new WeightedGraph(GeneralMethods.g1);
        private int n;

        private LoadingScreen loading;
        private System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer t2 = new System.Windows.Forms.Timer();
        private int ct = 0;

        private RoadInput input;
        private int id1, id2;
        private int clicked = 0;

        private IconButton close = new IconButton();

        PrevTripRepository tripRepo = new PrevTripRepository();

        private bool viewOnly = false;
        private int start, end;
        public MyRoadsForm Prev;

        public MapForm(int User_id, MainForm mainF, Control parent, bool viewOnly,
            int start, int end) : base(mainF, 5)
        {
            this.TopLevel = false;
            this.user_id = User_id;
            this.Parent = parent;
            this.viewOnly = viewOnly;
            this.start = start;
            this.end = end;

            n = graph.N;

            this.FormBorderStyle = FormBorderStyle.None;

            Size = new Size(1930, 1010);
            // CenterToScreen();
            BackColor = GeneralMethods.backColor;

            //loading = new LoadingScreen(this);
            //t1.Enabled = true;
            //t1.Interval = 600;
            //t1.Tick += new EventHandler(this.t1_Tick);

            loadInput();
            loadCities();
            loadClose();
        }

        private void loadInput()
        {
            input = new RoadInput(this);
            input.Location = new Point(30, 10);
            input.BUTTON.Click += new EventHandler(inputButton_Click);

            if(viewOnly == true)
            {
                input.Visible = false;
            }
        }
        private void inputButton_Click(object? sender, EventArgs e)
        {
            string c1 = input.START.TEXT.Text;
            string c2 = input.END.TEXT.Text;

            bool ok1 = cityrep.contains(c1, out id1);
            bool ok2 = cityrep.contains(c2, out id2);

            if (ok1 && ok2)
            {
                handleBothOK();
            }
            else
            {
                MessageBox.Show("The locations you entered are not present" +
                    "in our database, please check again that they are correct");
            }
        }
        private void handleBothOK()
        {
            clicked++;
            ct = 0;

            for (int i = 1; i <= n; i++)
                cards[i].Visible = false;
            input.Visible = false;

            loading = new LoadingScreen(this);
            t2.Enabled = true;
            t2.Interval = 600;
            t2.Tick += new EventHandler(t2_Tick);
        }
        private void t2_Tick(object? sender, EventArgs e)
        {
            ct += 600;
            if (ct == 1800)
            {
                Controls.Remove(loading);
                t2.Enabled = false;

                input.Visible = true;
                for (int i = 1; i <= n; i++)
                    cards[i].Visible = true;


                PaintEventArgs p = new PaintEventArgs(CreateGraphics(),
                    new Rectangle(new Point(0, 0), Size));
                initMap(GeneralMethods.darkGray, p);
                colorRoad(id1, id2, p);

            }
        }


        private void loadCities()
        {
            for (int i = 1; i <= n; i++)
            {
                cards[i] = new CityCard(cityrep.ALL[i - 1], this);
                cards[i].Load();
            }
        }
        private void t1_Tick(object? sender, EventArgs e)
        {
            //this.loading.Visible = false;
            ct += 600;
            if (ct == 3000)
            {
                Controls.Remove(loading);
                t1.Enabled = false;
            }
            else if (ct == 2400)
                loadCities();

        }


        private void drawBorder(PaintEventArgs e)
        {
            Pen p = new Pen(GeneralMethods.foreColor, 6);
            e.Graphics.DrawRectangle(p, new Rectangle(input.Location, input.Size));
        }
        private void initMap(Color color, PaintEventArgs e)
        {
            Graphics gr = CreateGraphics();
            gr.Clear(GeneralMethods.backColor);

            if(viewOnly == false)
            {
                drawBorder(e);
            }
            

            Pen rotring = new Pen(color, 4);
            int road = 0;
            List<List<Pair<int, int>>> g = graph.G_O;

            for (int i = 1; i <= n; i++)
            {
                foreach (var x in g[i])
                {
                    Road r = pointrep.getRoad(++road);
                    if (r.POINTS.Count() != 0)
                        e.Graphics.DrawCurve(rotring, r.POINTS);
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (clicked == 0)
            {
                if(viewOnly == false)
                {
                    initMap(GeneralMethods.foreColor, e);
                }
                else
                {
                    initMap(GeneralMethods.darkGray, e);
                    colorRoad(start, end, e);
                }
            }
        }
        

        private void colorRoad(int a, int b, PaintEventArgs e)
        {
            Pen rotring = new Pen(GeneralMethods.roadColor, 7);
            List<int> roads = graph.getRoadsOnPath(a, b, out int length);

            string res = "";

            foreach (var x in roads)
            {
                res += x + " ";
                Road r = pointrep.getRoad(x);
                e.Graphics.DrawCurve(rotring, r.POINTS);
            }

            if(viewOnly == false)
            {
                res.Remove(res.Length - 1);
                tripRepo.addTrip(user_id, res, length, a, b);
            }
        }
        public void colorGivenRoad(int a, int b)
        {
            colorRoad(a, b, new PaintEventArgs(this.CreateGraphics(), 
                new Rectangle(new Point(0, 0), this.Size)));
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
        private void close_Click(object ?sender, EventArgs e)
        {
            this.Close();
            if(viewOnly == true)
            {
                Prev.Show();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (input.START.TEXT.Text != input.START.T1
                || input.END.TEXT.Text != input.END.T2)
            {
                DialogResult res = MessageBox.Show("Your input will be lost.\n" +
                    "Are you sure you want to exit?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.No)
                    e.Cancel = true;
                else
                    e.Cancel = false;

            }
        }
    }
}
