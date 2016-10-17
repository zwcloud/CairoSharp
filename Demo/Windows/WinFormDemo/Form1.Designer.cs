using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormDemo
{
	public partial class Form1 : Form
	{
		private IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.ClientSize = new Size(384, 262);
			base.Name = "Form1";
			this.Text = "CairoDemo cairo version: 1.14.2";
			base.ResumeLayout(false);
		}

		#endregion
	}
}
