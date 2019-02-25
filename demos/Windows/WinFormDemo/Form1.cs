using Cairo;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormDemo
{
    public partial class Form1 : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem staticToolStripMenuItem;
        private Timer timer1;
        private IContainer components;
        private ToolStripMenuItem tosvgstreamToolStripMenuItem;
        private ToolStripMenuItem animatedToolStripMenuItem;

        public Form1()
        {
            InitializeComponent();
        }

        private enum example_t
        {
            static_example = 0,
            animated_example = 1,
            to_svg_stream_example = 2
        }

        private example_t example_type = example_t.static_example;


        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (example_type == example_t.static_example)
            {
                DrawStatic(e);
            }
            else if (example_type == example_t.animated_example)
            {
                DrawAnimated(e);
            }
            else if (example_type == example_t.to_svg_stream_example)
            {
                DrawStaticAndToOutputStream();
            }
        }

        private void DrawStaticAndToOutputStream()
        {
            MemoryStream outpStream = new MemoryStream();
            // create a SvgSurface, which output is bound to a stream.
            var svgStreamSurface = new SvgSurface(outpStream, 1024, 1024);
            using (Context context = new Context(svgStreamSurface))
            {
                //clear the background to white
                context.SetSourceRGB(1, 1, 1);
                context.Paint();

                //stroke the bug
                context.LineWidth = 2.0;
                context.SetSourceColor(this.bugColor);
                context.MoveTo(7.0, 64.0);
                context.CurveTo(1.0, 47.0, 2.0, 46.0, 9.0, 51.0);
                context.MoveTo(25.0, 80.0);
                context.CurveTo(10.0, 73.0, 11.0, 70.0, 14.0, 63.0);
                context.MoveTo(10.0, 41.0);
                context.CurveTo(2.0, 36.0, 1.0, 33.0, 1.0, 26.0);
                context.LineWidth = 1.0;
                context.MoveTo(1.0, 26.0);
                context.CurveTo(5.0, 23.0, 7.0, 18.0, 12.0, 17.0);
                context.LineTo(12.0, 14.0);
                context.Stroke();
                context.MoveTo(30.0, 74.0);
                context.CurveTo(14.0, 64.0, 10.0, 48.0, 11.0, 46.0);
                context.LineTo(10.0, 45.0);
                context.LineTo(10.0, 40.0);
                context.CurveTo(13.0, 37.0, 15.0, 35.0, 19.0, 34.0);
                context.Stroke();
            }
            // .Finish() is mandatory. Without this, cairo does not write anything to the stream.
            svgStreamSurface.Finish();

            outpStream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(outpStream);

            Console.WriteLine(reader.ReadToEnd());

            timer1.Enabled = false;
        }

        private Cairo.Color bugColor = new Cairo.Color(0.95294117647058818, 0.6, 0.0784313725490196, 1.0);
        private void DrawStatic(PaintEventArgs e)
        {
            using (System.Drawing.Graphics graphics = e.Graphics)
            using (Win32Surface surface = new Win32Surface(graphics.GetHdc()))

            using (Context context = new Context(surface))
            {
                //clear the background to white
                context.SetSourceRGB(1, 1, 1);
                context.Paint();

                //stroke the bug
                context.LineWidth = 2.0;
                context.SetSourceColor(this.bugColor);
                context.MoveTo(7.0, 64.0);
                context.CurveTo(1.0, 47.0, 2.0, 46.0, 9.0, 51.0);
                context.MoveTo(25.0, 80.0);
                context.CurveTo(10.0, 73.0, 11.0, 70.0, 14.0, 63.0);
                context.MoveTo(10.0, 41.0);
                context.CurveTo(2.0, 36.0, 1.0, 33.0, 1.0, 26.0);
                context.LineWidth = 1.0;
                context.MoveTo(1.0, 26.0);
                context.CurveTo(5.0, 23.0, 7.0, 18.0, 12.0, 17.0);
                context.LineTo(12.0, 14.0);
                context.Stroke();
                context.MoveTo(30.0, 74.0);
                context.CurveTo(14.0, 64.0, 10.0, 48.0, 11.0, 46.0);
                context.LineTo(10.0, 45.0);
                context.LineTo(10.0, 40.0);
                context.CurveTo(13.0, 37.0, 15.0, 35.0, 19.0, 34.0);
                context.Stroke();
            }
        }

        private double angle = -Math.PI / 2;
        private ImageSurface backSurface;
        private void DrawAnimated(PaintEventArgs e)
        {
            if (backSurface == null)
            {
                backSurface = new ImageSurface(Format.Rgb24,
                    this.ClientSize.Width, this.ClientSize.Height);
            }

            using (Graphics graphics = e.Graphics)
            {
                var hdc = graphics.GetHdc();
                using (Win32Surface surface = new Win32Surface(hdc))
                {
                    //draw to image surface
                    Context context = new Context(backSurface);
                    context.LineWidth = 2.0;
                    context.SetSourceRGB(1, 1, 1);
                    context.Paint();//clear the background to white
                    context.SetSourceRGB(0, 0, 0);
                    context.MoveTo(100, 100);
                    context.LineTo(100 + 80 * Math.Cos(angle), 100 + 80 * Math.Sin(angle));
                    angle += Math.PI / 180;
                    context.Stroke();

                    //paint image surface to win32 surface
                    Context context1 = new Context(surface);
                    context1.SetSource(backSurface);
                    context1.Paint();

                    context.Dispose();
                    context1.Dispose();
                }
                graphics.ReleaseHdc();
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.staticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tosvgstreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staticToolStripMenuItem,
            this.animatedToolStripMenuItem,
            this.tosvgstreamToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // staticToolStripMenuItem
            // 
            this.staticToolStripMenuItem.Name = "staticToolStripMenuItem";
            this.staticToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.staticToolStripMenuItem.Text = "static";
            this.staticToolStripMenuItem.Click += new System.EventHandler(this.staticToolStripMenuItem_Click);
            // 
            // animatedToolStripMenuItem
            // 
            this.animatedToolStripMenuItem.Name = "animatedToolStripMenuItem";
            this.animatedToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.animatedToolStripMenuItem.Text = "animated";
            this.animatedToolStripMenuItem.Click += new System.EventHandler(this.animatedToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tosvgstreamToolStripMenuItem
            // 
            this.tosvgstreamToolStripMenuItem.Name = "tosvgstreamToolStripMenuItem";
            this.tosvgstreamToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.tosvgstreamToolStripMenuItem.Text = "tosvgstream";
            this.tosvgstreamToolStripMenuItem.Click += new System.EventHandler(this.tosvgstreamToolStripMenuItem_Click_1);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void staticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            example_type = example_t.static_example;
            timer1.Enabled = false;
            this.Invalidate();
        }

        private void animatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            example_type = example_t.animated_example;
            timer1.Enabled = true;
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void tosvgstreamToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            example_type = example_t.to_svg_stream_example;
            timer1.Enabled = true;
            this.Invalidate();
        }
    }
}
