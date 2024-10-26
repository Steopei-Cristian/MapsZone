using FontAwesome.Sharp;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.inputs
{
    internal class RoadInput : Panel
    {
        private CityInput start;
        private CityInput end;
        private IconButton button = new IconButton();

        private Label title = new Label();

        public IconButton BUTTON { get => button; set => button = value; }
        public CityInput START { get => start; }
        public CityInput END { get => end; }

        public RoadInput(Control parent)
        {
            Parent = parent;
            Size = new Size(500, 250);
            // this.BorderStyle = BorderStyle.FixedSingle;

            loadTitle();

            start = new CityInput(this, 1);
            start.Location = new Point(5, title.Height + title.Top + 10);

            end = new CityInput(this, 0);
            end.Location = new Point(start.Left + start.Width + 5, start.Location.Y);

            loadIconButton();
        }

        private void loadTitle()
        {
            title.Parent = this;
            title.Size = new Size(250, 30);
            title.Location = new Point((Width - title.Width) / 2, 5);
            title.Text = "Choose your route!";
            title.Text.ToUpper();
            title.Font = new Font("Bold", 13);
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.ForeColor = GeneralMethods.foreColor;
        }
        private void loadIconButton()
        {
            button.Parent = this;
            button.Size = new Size(100, 50);
            button.Location = new Point(Width / 2 - button.Width / 2,
                end.Top + end.Height + 110);
            button.Font = new Font("Bold", 10);
            button.Text = "GO";
            button.IconChar = IconChar.MagnifyingGlassLocation;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextAlign = ContentAlignment.MiddleRight;
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.IconColor = button.ForeColor = GeneralMethods.foreColor;
            button.FlatAppearance.MouseOverBackColor = GeneralMethods.backColor;

        }
    }
}
