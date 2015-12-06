using System.Windows.Forms;
using Cairo;
using Color = Cairo.Color;
using Graphics = System.Drawing.Graphics;

namespace CairoDrawPngFromMemory
{
    public partial class Form1 : Form
    {
        public Graphics Graphics1 { get; private set; }
        public Context Context1 { get; set; }
        public Win32Surface Surface1 { get; private set; }
        byte[] pngData = System.IO.File.ReadAllBytes("1.png");

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

            using (ImageSurface pngImageSurface = new ImageSurface(pngData))
            {
                Context1.SetSource(pngImageSurface);
                Context1.Paint();
            }

            Graphics1.Dispose();
            Context1.Dispose();
            Surface1.Dispose();
        }
    }
}
