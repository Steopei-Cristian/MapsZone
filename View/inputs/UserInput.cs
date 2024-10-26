using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;

namespace Atestat1.View.inputs
{
    internal class UserInput : Input
    {
        private string t1 = "Enter your username";
        private string t2 = "Enter your password";
        private string t3 = "Enter your email adress";
        private int k = 0;

        public string T1 { get => t1; }
        public string T2 { get => t2; set => text!.Text = value; }
        public string T3 { get => t3; }

        public int Conf;

        public UserInput(int K, Control parent) : base(parent)
        {
            k = K;
            personalize();
        }

        private void personalize()
        {
            Size = new Size(300, 50);
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = GeneralMethods.backColor;

            icon!.IconChar = k == 1 ? IconChar.User : 
                (k == 2) ? IconChar.Lock : IconChar.Envelope;
            icon.ForeColor = GeneralMethods.foreColor;
            icon.Top = 5;

            text!.Width = 200;
            text.BackColor = GeneralMethods.backColor;
            text.BorderStyle = BorderStyle.None;
            text.Font = new Font(GeneralMethods.fontFam, 9, FontStyle.Bold);
            text.Text = k == 1 ? t1 : (k == 2) ? t2 : t3;
            text.ForeColor = Color.Gray;
            text.Top = 10;

            text.GotFocus += new EventHandler(textBx_Focus);
            text.LostFocus += new EventHandler(textBx_LostFocus);
        }

        private void textBx_Focus(object? sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender!;

            if ((txt.Text == t1 && k == 1) 
                || (txt.Text == t2  || txt.Text == "Confirm your password" && k == 2) 
                || (txt.Text == t3 && k == 3))
            {
                if (k == 2) txt.PasswordChar = '*';
                txt.Text = string.Empty;
                txt.ForeColor = GeneralMethods.foreColor;
            }
        }
        private void textBx_LostFocus(object? sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender!;

            if (txt.Text == string.Empty)
            {
                txt.PasswordChar = '\0';
                txt.ForeColor = Color.Gray;
                txt.Text = k == 1 ? t1 : (k == 2) ? t2 : t3;
                if (Conf != 0 && k == 2)
                    txt.Text = "Confirm your password";
            }
        }
    }
}
