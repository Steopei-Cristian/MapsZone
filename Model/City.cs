using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class City
    {
        private int id;
        private string city_name;
        private int x;
        private int y;
        private Image? picture;

        public int ID { get => this.id; }
        public string CITY_NAME { get => this.city_name; }
        public int X { get => this.x; }
        public int Y { get => this.y; }
        public Image PICTURE { get => this.picture!; }

        private void setImage(byte[] img)
        {
            MemoryStream stream = new MemoryStream(img);
            picture = Image.FromStream(stream);
        }

        public City(int Id, string City_name, int X, int Y, byte[] ?image)
        {
            this.id = Id;
            this.city_name = City_name;
            this.x = X;
            this.y = Y;
            if(image != null)
            {
                setImage(image);
            }
        }

        public override string ToString()
        {
            return this.id.ToString() + " | " + this.city_name; 
        }
    }
}
