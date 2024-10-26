using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.cards
{
    internal class AutoCompletePanel : Panel
    {
        private List<Label> labels = new List<Label>(1001);
        private List<string> strings;
        private TextBox textbx;

        public bool clicked = false;

        public List<string> STRINGS { get => strings; set => strings = value; }

        public AutoCompletePanel(Control parent, List<string> s, TextBox txt)
        {
            Parent = parent.Parent;
            AutoScroll = true;

            strings = s;
            textbx = txt;

            // MessageBox.Show(parent.ToString());
            // MessageBox.Show(parent.Location.ToString());
            // MessageBox.Show(parent.Size.ToString());

            Size = new Size(230, Math.Min(s.Count * 32, 96));
            Location = new Point(parent.Location.X + 10, parent.Location.Y + parent.Height + 2);
            Visible = false;

            // MessageBox.Show(this.Location.ToString());
            // this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void loadLabels()
        {
            int d = strings.Count;

            if (d == 0)
                return;

            BorderStyle = BorderStyle.FixedSingle;
            Visible = true;

            for (int i = 1; i <= d; i++)
            {
                Label l = loadLabel(i - 1);
                if (i > 1)
                    l.Location = new Point(0,
                        labels[i - 2].Height + labels[i - 2].Location.Y + 2);
                labels.Add(l);
            }
        }

        private Label loadLabel(int i)
        {
            Label label = new Label();
            label.Parent = this;
            label.Size = new Size(200, 30);
            label.Text = strings[i];
            // label.BorderStyle = BorderStyle.FixedSingle;
            label.Font = new Font("Bold", 9);
            label.ForeColor = GeneralMethods.foreColor;
            label.Click += new EventHandler(label_OnClick);
            return label;
        }

        private void label_OnClick(object? sender, EventArgs e)
        {
            Label label = (Label)sender!;
            clicked = true;
            textbx.Text = label.Text;
            textbx.Width = Math.Max(textbx.Width, 10 * label.Text.Length);
            textbx.SelectionStart = label.Text.Length;
            Visible = false;

            // MessageBox.Show("1");
        }
    }
}
