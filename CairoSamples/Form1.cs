using System.Windows.Forms;
using Cairo;
using Color = Cairo.Color;
using Graphics = System.Drawing.Graphics;

namespace CairoSamples
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

        private System.Action<Context> OnPaintAction;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics1 = e.Graphics;
            Surface1 = new Win32Surface(Graphics1.GetHdc());
            Context1 = new Context(Surface1);

            if (OnPaintAction!=null)
            {
                OnPaintAction(Context1);
            }

            Graphics1.Dispose();
            Context1.Dispose();
            Surface1.Dispose();
        }

        private void arcToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OnPaintAction = (cr) =>
            {
                double xc = 128.0;
                double yc = 128.0;
                double radius = 100.0;
                double angle1 = 45.0 * (System.Math.PI / 180.0);  /* angles are specified */
                double angle2 = 180.0 * (System.Math.PI / 180.0);  /* in radians           */

                cr.LineWidth = 10.0;
                cr.Arc(xc, yc, radius, angle1, angle2);
                cr.Stroke();

                /* draw helping lines */
                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.LineWidth = 6.0;

                cr.Arc(xc, yc, 10.0, 0, 2 * System.Math.PI);
                cr.Fill();

                cr.Arc(xc, yc, radius, angle1, angle1);
                cr.LineTo(xc, yc);
                cr.Arc(xc, yc, radius, angle2, angle2);
                cr.LineTo(xc, yc);
                cr.Stroke();
            };

            Invalidate();
        }

    }
}
