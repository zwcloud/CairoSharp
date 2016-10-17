using Cairo;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormDemo
{
	public partial class Form1 : Form
	{
		private Cairo.Color bugColor = new Cairo.Color(0.95294117647058818, 0.6, 0.0784313725490196, 1.0);

		public Form1()
		{
			this.InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (System.Drawing.Graphics graphics = e.Graphics)
			{
				using (Win32Surface surface = new Win32Surface(graphics.GetHdc()))
				{
					using (Context context = new Context(surface))
					{
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
			}
		}
	}
}
