using Atestat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using FontAwesome.Sharp;
using System.Windows.Input;

namespace Atestat1.View.cards
{
    internal class CityCard : Panel
    {
        private City city;
        private PictureBox pic = new PictureBox();
        private Label text = new Label();

        public City CITY { get => city; }

        public CityCard(City city, Control parent)
        {
            this.city = city;
            Parent = parent;
        }

        public void Load()
        {
            Size = new Size(100, 100);
            BorderStyle = BorderStyle.FixedSingle;
            Location = new Point(city.X, city.Y);
            loadPictureBox();
            loadText();
        }

        private void loadPictureBox()
        {
            pic.Parent = this;
            pic.Size = new Size(100, 75);
            pic.Image = city.PICTURE;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void loadText()
        {
            text.Parent = this;
            text.Size = new Size(100, 25);
            text.Location = new Point((Width - text.Width) / 2, pic.Height);
            text.Text = city.CITY_NAME;
            text.TextAlign = ContentAlignment.MiddleCenter;
            text.BorderStyle = BorderStyle.FixedSingle;
            text.Font = new Font("Bold", 9);
            text.ForeColor = GeneralMethods.foreColor;
        }
    }
}
