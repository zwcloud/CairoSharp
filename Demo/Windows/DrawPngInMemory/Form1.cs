using Cairo;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DrawPngInMemory
{
	public partial class Form1 : Form
	{
		private byte[] pngData = File.ReadAllBytes("1.png");

		public System.Drawing.Graphics Graphics1
		{
			get;
			private set;
		}

		public Context Context1
		{
			get;
			set;
		}

		public Win32Surface Surface1
		{
			get;
			private set;
		}

		public Form1()
		{
			this.InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.Graphics1 = e.Graphics;
			this.Surface1 = new Win32Surface(this.Graphics1.GetHdc());
			this.Context1 = new Context(this.Surface1);
			using (ImageSurface pngImageSurface = new ImageSurface(this.pngData))
			{
				this.Context1.SetSource(pngImageSurface);
				this.Context1.Paint();
			}
			this.Graphics1.Dispose();
			this.Context1.Dispose();
			this.Surface1.Dispose();
		}
	}
}
