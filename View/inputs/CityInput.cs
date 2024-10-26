using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View.cards;
using FontAwesome.Sharp;
using static System.Net.Mime.MediaTypeNames;

namespace Atestat1.View.inputs
{
    internal class CityInput : Input
    {
        private int start;
        private string t1 = "From here";
        private string t2 = "To here";

        private CityRepository cityrep = new CityRepository();

        public string T1 { get => this.t1; }
        public string T2 { get => this.t2; }

        public CityInput(Control parent, int start) : base(parent)
        {
            Parent = parent;
            this.start = start;
            Size = new Size(240, 40);
            BorderStyle = BorderStyle.None;

            panel = new AutoCompletePanel(this, new List<string>(), text!);

            // this.SetTopLevel(true);

            strings = cityrep.getNames();

            personalize();

        }

        private void personalize()
        {
            icon!.IconChar = start == 1 ? IconChar.CircleXmark : IconChar.LocationDot;
            text!.Text = start == 1 ? t1 : t2;

            text.BackColor = GeneralMethods.backColor;
            icon.IconColor = GeneralMethods.foreColor;
            text.ForeColor = Color.Gray;
            text.BorderStyle = BorderStyle.None;
            text.Top = 7;

            text.GotFocus += new EventHandler(textbox_GainedFocus);
            text.LostFocus += new EventHandler(textbox_LostFocus);
        }

        private void textbox_LostFocus(object? sender, EventArgs e)
        {
            panel!.Visible = false;
            TextBox txt = (TextBox)sender!;
            if (txt.Text == string.Empty)
            {
                text!.Text = start == 1 ? t1 : t2;
                txt.ForeColor = Color.Gray;
            }
        }
        private void textbox_GainedFocus(object? sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender!;

            if (txt.Text == t1 || txt.Text == t2)
            {
                txt.Text = "";
                txt.ForeColor = GeneralMethods.foreColor;
            }
            else if (txt.Text != string.Empty)
                panel!.Visible = true;
        }


    }
}
