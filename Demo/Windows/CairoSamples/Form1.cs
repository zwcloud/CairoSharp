using System;
using System.IO;
using System.Windows.Forms;
using Cairo;
using Graphics = System.Drawing.Graphics;

namespace CairoSamples
{
    public partial class Form1 : Form
    {
        public Graphics Graphics1 { get; private set; }
        public Context Context1 { get; set; }
        public Win32Surface Surface1 { get; private set; }

        private readonly byte[] romedalenPngData;

        public Form1()
        {
            InitializeComponent();

            romedalenPngData = File.ReadAllBytes("data/romedalen.png");
        }

        private Action<Context> OnPaintAction;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Graphics1 = e.Graphics)
            using (Surface1 = new Win32Surface(Graphics1.GetHdc()))
            using (Context1 = new Context(Surface1))
            {
                if (OnPaintAction!=null)
                {
                    OnPaintAction(Context1);
                }
            }
        }

        private void arcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                double xc = 128.0;
                double yc = 128.0;
                double radius = 100.0;
                double angle1 = 45.0 * (Math.PI / 180.0);  /* angles are specified */
                double angle2 = 180.0 * (Math.PI / 180.0);  /* in radians           */

                cr.LineWidth = 10.0;
                cr.Arc(xc, yc, radius, angle1, angle2);
                cr.Stroke();

                /* draw helping lines */
                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.LineWidth = 6.0;

                cr.Arc(xc, yc, 10.0, 0, 2 * Math.PI);
                cr.Fill();

                cr.Arc(xc, yc, radius, angle1, angle1);
                cr.LineTo(xc, yc);
                cr.Arc(xc, yc, radius, angle2, angle2);
                cr.LineTo(xc, yc);
                cr.Stroke();
            };

            Invalidate();
        }

        private void arcNegativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                double xc = 128.0;
                double yc = 128.0;
                double radius = 100.0;
                double angle1 = 45.0 * (Math.PI / 180.0);  /* angles are specified */
                double angle2 = 180.0 * (Math.PI / 180.0);  /* in radians           */

                cr.LineWidth = 10.0;
                cr.ArcNegative(xc, yc, radius, angle1, angle2);
                cr.Stroke();

                /* draw helping lines */
                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.LineWidth = 6.0;

                cr.Arc(xc, yc, 10.0, 0, 2 * Math.PI);
                cr.Fill();

                cr.Arc(xc, yc, radius, angle1, angle1);
                cr.LineTo(xc, yc);
                cr.Arc(xc, yc, radius, angle2, angle2);
                cr.LineTo(xc, yc);
                cr.Stroke();
            };

            Invalidate();
        }

        private void clipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.Arc(128.0, 128.0, 76.8, 0, 2 * Math.PI);
                cr.Clip();

                cr.NewPath();  /* current path is not consumed by cairo_clip() */
                cr.Rectangle(0, 0, 256, 256);
                cr.Fill();
                cr.SetSourceRGB(0, 1, 0);
                cr.MoveTo(0, 0);
                cr.LineTo(256, 256);
                cr.MoveTo(256, 0);
                cr.LineTo(0, 256);
                cr.LineWidth = 10.0;
                cr.Stroke();
            };

            Invalidate();
        }

        private void clipImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                int w, h;
                ImageSurface image;

                cr.Arc(128.0, 128.0, 76.8, 0, 2 * Math.PI);
                cr.Clip();
                cr.NewPath(); /* path not consumed by clip()*/

                image = new ImageSurface(romedalenPngData);
                w = image.Width;
                h = image.Height;

                cr.Scale(256.0 / w, 256.0 / h);

                cr.SetSourceSurface(image, 0, 0);
                cr.Paint();

                image.Dispose();
            };

            Invalidate();
        }

        private void curveRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                /* a custom shape that could be wrapped in a function */
                double x0 = 25.6,   /* parameters like cairo_rectangle */
                       y0 = 25.6,
                       rect_width = 204.8,
                       rect_height = 204.8,
                       radius = 102.4;   /* and an approximate curvature radius */

                double x1, y1;

                x1 = x0 + rect_width;
                y1 = y0 + rect_height;

                if (rect_width / 2 < radius)
                {
                    if (rect_height / 2 < radius)
                    {
                        cr.MoveTo(x0, (y0 + y1) / 2);
                        cr.CurveTo( x0, y0, x0, y0, (x0 + x1) / 2, y0);
                        cr.CurveTo( x1, y0, x1, y0, x1, (y0 + y1) / 2);
                        cr.CurveTo( x1, y1, x1, y1, (x1 + x0) / 2, y1);
                        cr.CurveTo( x0, y1, x0, y1, x0, (y0 + y1) / 2);
                    }
                    else
                    {
                        cr.MoveTo(x0, y0 + radius);
                        cr.CurveTo( x0, y0, x0, y0, (x0 + x1) / 2, y0);
                        cr.CurveTo( x1, y0, x1, y0, x1, y0 + radius);
                        cr.LineTo( x1, y1 - radius);
                        cr.CurveTo( x1, y1, x1, y1, (x1 + x0) / 2, y1);
                        cr.CurveTo( x0, y1, x0, y1, x0, y1 - radius);
                    }
                }
                else
                {
                    if (rect_height / 2 < radius)
                    {
                        cr.MoveTo(x0, (y0 + y1) / 2);
                        cr.CurveTo( x0, y0, x0, y0, x0 + radius, y0);
                        cr.LineTo( x1 - radius, y0);
                        cr.CurveTo( x1, y0, x1, y0, x1, (y0 + y1) / 2);
                        cr.CurveTo( x1, y1, x1, y1, x1 - radius, y1);
                        cr.LineTo( x0 + radius, y1);
                        cr.CurveTo( x0, y1, x0, y1, x0, (y0 + y1) / 2);
                    }
                    else
                    {
                        cr.MoveTo(x0, y0 + radius);
                        cr.CurveTo( x0, y0, x0, y0, x0 + radius, y0);
                        cr.LineTo( x1 - radius, y0);
                        cr.CurveTo( x1, y0, x1, y0, x1, y0 + radius);
                        cr.LineTo( x1, y1 - radius);
                        cr.CurveTo( x1, y1, x1, y1, x1 - radius, y1);
                        cr.LineTo( x0 + radius, y1);
                        cr.CurveTo( x0, y1, x0, y1, x0, y1 - radius);
                    }
                }
                cr.ClosePath();

                cr.SetSourceRGB(0.5, 0.5, 1);
                cr.FillPreserve();
                cr.SetSourceRGBA(0.5, 0, 0, 0.5);
                cr.LineWidth = 10.0;
                cr.Stroke();
            };

            Invalidate();
        }

        private void curveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                double x = 25.6, y = 128.0;
                double x1 = 102.4, y1 = 230.4,
                       x2 = 153.6, y2 = 25.6,
                       x3 = 230.4, y3 = 128.0;

                cr.MoveTo( x, y);
                cr.CurveTo( x1, y1, x2, y2, x3, y3);

                cr.LineWidth = 10.0;
                cr.Stroke();

                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.LineWidth = 6.0;
                cr.MoveTo(x, y); cr.LineTo(x1, y1);
                cr.MoveTo(x2, y2); cr.LineTo(x3, y3);
                cr.Stroke();
            };

            Invalidate();
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                double[] dashes =
                {
                    50.0,   /* ink */
                    10.0,   /* skip */
                    10.0,   /* ink */
                    10.0    /* skip*/
                };
                double offset = -50.0;

                cr.SetDash(dashes, offset);
                cr.LineWidth = 10.0;

                cr.MoveTo(128.0, 25.6);
                cr.LineTo(230.4, 230.4);
                cr.RelLineTo(-102.4, 0.0);
                cr.CurveTo(51.2, 230.4, 51.2, 128.0, 128.0, 128.0);

                cr.Stroke();
            };
            
            Invalidate();
        }

        private void fillAndStroke2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.MoveTo(128.0, 25.6);
                cr.LineTo(230.4, 230.4);
                cr.RelLineTo(-102.4, 0.0);
                cr.CurveTo(51.2, 230.4, 51.2, 128.0, 128.0, 128.0);
                cr.ClosePath();

                cr.MoveTo(64.0, 25.6);
                cr.RelLineTo(51.2, 51.2);
                cr.RelLineTo(-51.2, 51.2);
                cr.RelLineTo(-51.2, -51.2);
                cr.ClosePath();

                cr.LineWidth = 10.0;
                cr.SetSourceRGB(0, 0, 1);
                cr.FillPreserve();
                cr.SetSourceRGB(0, 0, 0);
                cr.Stroke();
            };

            Invalidate();
        }

        private void fillStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.LineWidth = 6;

                cr.Rectangle(12, 12, 232, 70);
                cr.NewSubPath(); cr.Arc(64, 64, 40, 0, 2 * Math.PI);
                cr.NewSubPath(); cr.ArcNegative(192, 64, 40, 0, -2 * Math.PI);

                cr.FillRule = FillRule.EvenOdd;
                cr.SetSourceRGB(0, 0.7, 0); cr.FillPreserve();
                cr.SetSourceRGB(0, 0, 0); cr.Stroke();

                cr.Translate(0, 128);
                cr.Rectangle(12, 12, 232, 70);
                cr.NewSubPath(); cr.Arc(64, 64, 40, 0, 2 * Math.PI);
                cr.NewSubPath(); cr.ArcNegative(192, 64, 40, 0, -2 * Math.PI);

                cr.FillRule = FillRule.Winding;
                cr.SetSourceRGB(0, 0, 0.9); cr.FillPreserve();
                cr.SetSourceRGB(0, 0, 0); cr.Stroke();
            };

            Invalidate();
        }

        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                Gradient pat = new LinearGradient(0.0, 0.0, 0.0, 256.0);
                pat.AddColorStopRgba(1, 0, 0, 0, 1);
                pat.AddColorStopRgba(0, 1, 1, 1, 1);
                cr.Rectangle(0, 0, 256, 256);
                cr.SetSource(pat);
                cr.Fill();
                pat.Dispose();

                pat = new RadialGradient(115.2, 102.4, 25.6,
                                         102.4, 102.4, 128.0);
                pat.AddColorStopRgba(0, 1, 1, 1, 1);
                pat.AddColorStopRgba(1, 0, 0, 0, 1);
                cr.SetSource(pat);
                cr.Arc(128.0, 128.0, 76.8, 0, 2 * Math.PI);
                cr.Fill();
                pat.Dispose();
            };

            Invalidate();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                int w, h;
                ImageSurface image;

                image = new ImageSurface(romedalenPngData);
                w = image.Width;
                h = image.Height;

                cr.Translate(128.0, 128.0);
                cr.Rotate(45 * Math.PI / 180);
                cr.Scale(256.0 / w, 256.0 / h);
                cr.Translate(-0.5 * w, -0.5 * h);

                cr.SetSourceSurface(image, 0, 0);
                cr.Paint();
                image.Dispose();
            };

            Invalidate();
        }

        private void imagepatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                int w, h;
                ImageSurface image;

                Pattern pattern;
                Matrix matrix = new Matrix();

                image = new ImageSurface(romedalenPngData);
                w = image.Width;
                h = image.Height;

                pattern = new SurfacePattern(image);
                pattern.Extend = Extend.Repeat;

                cr.Translate(128.0, 128.0);
                cr.Rotate(Math.PI / 4);
                cr.Scale(1 / Math.Sqrt(2), 1 / Math.Sqrt(2));
                cr.Translate(-128.0, -128.0);

                matrix.InitScale(w/256.0*5.0, h/256.0*5.0);
                pattern.Matrix = matrix;

                cr.SetSource(pattern);

                cr.Rectangle(0, 0, 256.0, 256.0);
                cr.Fill();

                pattern.Dispose();
                image.Dispose();
            };

            Invalidate();
        }

        private void multiSegmentCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.MoveTo(50.0, 75.0);
                cr.LineTo(200.0, 75.0);

                cr.MoveTo(50.0, 125.0);
                cr.LineTo(200.0, 125.0);

                cr.MoveTo(50.0, 175.0);
                cr.LineTo(200.0, 175.0);

                cr.LineWidth = 30.0;
                cr.LineCap = LineCap.Round;
                cr.Stroke();
            };

            Invalidate();
        }

        private void roundedRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                /* a custom shape that could be wrapped in a function */
                double x = 25.6,        /* parameters like cairo_rectangle */
                       y = 25.6,
                       width = 204.8,
                       height = 204.8,
                       aspect = 1.0,     /* aspect ratio */
                       corner_radius = height / 10.0;   /* and corner curvature radius */

                double radius = corner_radius / aspect;
                double degrees = Math.PI / 180.0;

                cr.NewSubPath();
                cr.Arc(x + width - radius, y + radius, radius, -90 * degrees, 0 * degrees);
                cr.Arc(x + width - radius, y + height - radius, radius, 0 * degrees, 90 * degrees);
                cr.Arc(x + radius, y + height - radius, radius, 90 * degrees, 180 * degrees);
                cr.Arc(x + radius, y + radius, radius, 180 * degrees, 270 * degrees);
                cr.ClosePath();

                cr.SetSourceRGB(0.5, 0.5, 1);
                cr.FillPreserve();
                cr.SetSourceRGBA(0.5, 0, 0, 0.5);
                cr.LineWidth = 10.0;
                cr.Stroke();
            };

            Invalidate();
        }

        private void setLineCapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.LineWidth = 30.0;
                cr.LineCap = LineCap.Butt; /* default */
                cr.MoveTo(64.0, 50.0); cr.LineTo(64.0, 200.0);
                cr.Stroke();
                cr.LineCap = LineCap.Round;
                cr.MoveTo(128.0, 50.0); cr.LineTo(128.0, 200.0);
                cr.Stroke();
                cr.LineCap = LineCap.Square;
                cr.MoveTo(192.0, 50.0); cr.LineTo(192.0, 200.0);
                cr.Stroke();

                /* draw helping lines */
                cr.SetSourceRGB(1, 0.2, 0.2);
                cr.LineWidth = 2.56;
                cr.MoveTo(64.0, 50.0); cr.LineTo(64.0, 200.0);
                cr.MoveTo(128.0, 50.0); cr.LineTo(128.0, 200.0);
                cr.MoveTo(192.0, 50.0); cr.LineTo(192.0, 200.0);
                cr.Stroke();
            };

            Invalidate();
        }

        private void setLineJoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.LineWidth = 40.96;
                cr.MoveTo(76.8, 84.48);
                cr.RelLineTo(51.2, -51.2);
                cr.RelLineTo(51.2, 51.2);
                cr.LineJoin = LineJoin.Miter; /* default */
                cr.Stroke();

                cr.MoveTo(76.8, 161.28);
                cr.RelLineTo(51.2, -51.2);
                cr.RelLineTo(51.2, 51.2);
                cr.LineJoin = LineJoin.Bevel;
                cr.Stroke();

                cr.MoveTo(76.8, 238.08);
                cr.RelLineTo(51.2, -51.2);
                cr.RelLineTo(51.2, 51.2);
                cr.LineJoin = LineJoin.Round;
                cr.Stroke();
            };

            Invalidate();
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                cr.SelectFontFace("Microsoft Sans Serif", FontSlant.Normal, FontWeight.Bold);
                cr.SetFontSize(90.0);

                cr.MoveTo(10.0, 135.0);
                cr.ShowText("Hello");

                cr.MoveTo(70.0, 165.0);
                cr.TextPath("void");
                cr.SetSourceRGB(0.5, 0.5, 1);
                cr.FillPreserve();
                cr.SetSourceRGB(0, 0, 0);
                cr.LineWidth = 2.56;
                cr.Stroke();

                /* draw helping lines */
                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.Arc(10.0, 135.0, 5.12, 0, 2 * Math.PI);
                cr.ClosePath();
                cr.Arc(70.0, 165.0, 5.12, 0, 2 * Math.PI);
                cr.Fill();
            };

            Invalidate();
        }

        private void textAlignCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                string text = "cairo";
                TextExtents extents;
                double x, y;

                cr.SelectFontFace("Microsoft Sans Serif", FontSlant.Normal, FontWeight.Normal);
                cr.SetFontSize(52.0);
                extents = cr.TextExtents(text);

                x = 128.0 - (extents.Width / 2 + extents.XBearing);
                y = 128.0 - (extents.Height / 2 + extents.YBearing);

                cr.MoveTo(x, y);
                cr.ShowText(text);

                /* draw helping lines */
                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.LineWidth = 6.0;
                cr.Arc(x, y, 10.0, 0, 2 * Math.PI);
                cr.Fill();
                cr.MoveTo(128.0, 0);
                cr.RelLineTo(0, 256);
                cr.MoveTo(0, 128.0);
                cr.RelLineTo(256, 0);
                cr.Stroke();
            };

            Invalidate();
        }

        private void textExtentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPaintAction = cr =>
            {
                string text = "cairo";
                TextExtents extents;
                double x, y;

                cr.SelectFontFace("Microsoft Sans Serif", FontSlant.Normal, FontWeight.Normal);

                cr.SetFontSize(100.0);
                extents = cr.TextExtents(text);

                x = 25.0;
                y = 150.0;

                cr.MoveTo(x, y);
                cr.ShowText(text);

                /* draw helping lines */
                cr.SetSourceRGBA(1, 0.2, 0.2, 0.6);
                cr.LineWidth = 6.0;
                cr.Arc(x, y, 10.0, 0, 2 * Math.PI);
                cr.Fill();
                cr.MoveTo(x, y);
                cr.RelLineTo(0, -extents.Height);
                cr.RelLineTo(extents.Width, 0);
                cr.RelLineTo(extents.XBearing, -extents.YBearing);
                cr.Stroke();
            };

            Invalidate();
        }
    }
}
