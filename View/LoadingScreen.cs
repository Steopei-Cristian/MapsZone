using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View
{
    internal class LoadingScreen : Panel
    {
        private PictureBox logo = new PictureBox();
        private Label text = new Label();

        private List<Image> images = new List<Image>(10);
        int k = 1;

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public LoadingScreen(Control parent)
        {
            this.Parent = parent;
            this.Size = parent.Size;
            this.BackColor = GeneralMethods.backColor;
            this.BorderStyle = BorderStyle.FixedSingle;
            getImages();
            timer.Enabled = true;
            timer.Interval = 600;
            timer.Tick += new EventHandler(timer_Tick);
            loadLogo();
            loadText();
        }

        private void getImages()
        {
            images.Add(Image.FromFile(Application.StartupPath + "\\LoadingScreenImages\\UP.png"));
            images.Add(Image.FromFile(Application.StartupPath + "\\LoadingScreenImages\\RIGHT.png"));
            images.Add(Image.FromFile(Application.StartupPath + "\\LoadingScreenImages\\DOWN.png"));
            images.Add(Image.FromFile(Application.StartupPath + "\\LoadingScreenImages\\LEFT.png"));
        }
        private void timer_Tick(object ?sender, EventArgs e)
        {
            if (this.Visible == false)
                timer.Enabled = false;
            logo.BackgroundImage = images[k++];
            if (k == 4) k = 0;
        }
        private void loadLogo()
        {
            logo.Parent = this;
            logo.Size = new Size(200, 200);
            logo.Location = new Point((this.Width - logo.Width) / 2,
                (this.Height - logo.Height) / 2);
            // logo.IconChar = IconChar.Spinner;
            // logo.IconColor = ColorTranslator.FromHtml("#BFA181");
            // logo.BorderStyle = BorderStyle.FixedSingle;
            logo.BackgroundImage = images[0];
            logo.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void loadText()
        {
            text.Parent = this;
            text.Size = new Size(200, 100);
            text.Location = new Point((this.Width - text.Width) / 2,
                logo.Location.Y + logo.Height);
            text.Font = new Font("Bold", 15);
            text.Text = "Just a second";
            text.TextAlign = ContentAlignment.MiddleCenter;
            text.ForeColor = ColorTranslator.FromHtml("#BFA181");
        }
    }
}
