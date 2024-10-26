using Atestat1.Model;
using Atestat1.View.cards;
using FontAwesome.Sharp;
using K4os.Compression.LZ4.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.inputs
{
    internal class Input : Panel
    {
        protected IconPictureBox? icon;
        protected List<string>? strings;
        protected AutoCompletePanel? panel;
        protected TextBox? text;



        public List<string> STRINGS { get => strings!; set => strings = value; }
        public TextBox TEXT { get => text!; set => text = value; }

        public Input(Control parent)
        {
            Parent = parent;
            //this.Size = new Size(500, 500);
            BorderStyle = BorderStyle.FixedSingle;

            loadIcon();
            loadTextBox();
        }

        protected void loadIcon()
        {
            icon = new IconPictureBox();
            icon.Parent = this;
            icon.Size = new Size(40, 40);
            icon.IconChar = IconChar.A;
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected void loadTextBox()
        {
            text = new TextBox();
            text.Parent = this;
            text.Location = new Point(icon!.Width + 13, 3);
            text.Size = new Size(120, 69);
            text.TabStop = false;
            text.Font = new Font("Bold", 11);
            text.TextChanged += new EventHandler(text_TextChanged);
        }
        protected void text_TextChanged(object? sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender!;
            List<string> s = new List<string>(1001);

            if (panel != null && panel!.clicked)
            {
                Parent.Controls.Remove(panel);
                panel.clicked = false;
            }
            else if (panel != null && panel!.clicked == false)
            {
                Parent.Controls.Remove(panel);
                if (txt.Text != string.Empty)
                {
                    foreach (var x in strings!)
                    {
                        string needle = txt.Text.ToLower();
                        char first = char.ToUpper(needle[0]);
                        needle = first + needle[1..];
                        KMP kmp = new KMP(x, needle);
                        if (kmp.search() != -1)
                            s.Add(x);
                    }

                    panel = new AutoCompletePanel(this, s, text!);
                    panel.loadLabels();
                }
            }

        }
    }
}
