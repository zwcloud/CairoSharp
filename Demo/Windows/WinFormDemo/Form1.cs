using System.Windows.Forms;
using Cairo;
using Color = Cairo.Color;
using Graphics = System.Drawing.Graphics;

namespace WinFormDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var graphics = e.Graphics)
            using (var surface = new Win32Surface(graphics.GetHdc()))
            using (var context = new Context(surface))
            {
                var p1 = new PointD(10, 10);
                var p2 = new PointD(100, 10);
                var p3 = new PointD(100, 100);
                var p4 = new PointD(10, 100);

                context.MoveTo(p1);
                context.LineTo(p2);
                context.LineTo(p3);
                context.LineTo(p4);
                context.LineTo(p1);
                context.ClosePath();
                context.Fill();
                context.MoveTo(140, 110);
                context.SetFontSize(32);
                context.SetSourceColor(new Color(0, 0, 0.8, 1));
                context.ShowText("Hello Cairo!");
            }
        }

    }
}
