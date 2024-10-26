using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View.inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.forms
{
    internal class TestForm : Form
    {
        private RecoverPass f;

        public TestForm()
        {
            Size = new Size(1000, 1000);
            CenterToScreen();

            f = new RecoverPass(new MainForm(), this);
            f.Show();
            
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Pen rotring = new Pen(Color.Black, 3);
        //    for (int i = 1; i <= roads; i++)
        //    {
        //        Road r = repository.getRoad(i);
        //        e.Graphics.DrawCurve(rotring, r.POINTS);
        //    }
        //}

    }
}
