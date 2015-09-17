using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cairo;
using Color = Cairo.Color;
using Graphics = System.Drawing.Graphics;

namespace CairoDrawOnWinFormWindowDemo
{
    public partial class Form1 : Form
    {
        public Graphics Graphics1 { get; private set; }
        public Context Context1 { get; set; }
        public Win32Surface Surface1 { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics1 = e.Graphics;
            Surface1 = new Win32Surface(Graphics1.GetHdc());
            Context1 = new Context(Surface1);

            var p1 = new PointD(10, 10);
            var p2 = new PointD(100, 10);
            var p3 = new PointD(100, 100);
            var p4 = new PointD(10, 100);

            Context1.MoveTo(p1);
            Context1.LineTo(p2);
            Context1.LineTo(p3);
            Context1.LineTo(p4);
            Context1.LineTo(p1);
            Context1.ClosePath();
            Context1.Fill();
            Context1.MoveTo(140, 110);
            Context1.SetFontSize(32);
            Context1.SetSourceColor(new Color(0,0,0.8,1));
            Context1.ShowText("Hello Cairo!");

            Graphics1.Dispose();
            Context1.Dispose();
            Surface1.Dispose();
        }
    }
}
