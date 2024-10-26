using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class CustomForm : Form
    {
        public int Level;
        public MainForm Main;
        public CustomForm(MainForm m, int lvl)
        {
            this.Main = m;
            this.Level = lvl;
        }
    }
}
