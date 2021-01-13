using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GraphicsControlTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private GraphicsControlTest.XYGraphControl xyGraphControl1;
		private System.Windows.Forms.Button btnSine;
		private System.Windows.Forms.Button btnSpiral;
        private Button btnInverseX;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnSine = new System.Windows.Forms.Button();
            this.btnSpiral = new System.Windows.Forms.Button();
            this.btnInverseX = new System.Windows.Forms.Button();
            this.xyGraphControl1 = new GraphicsControlTest.XYGraphControl();
            this.SuspendLayout();
            // 
            // btnSine
            // 
            this.btnSine.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSine.Location = new System.Drawing.Point(55, 288);
            this.btnSine.Name = "btnSine";
            this.btnSine.Size = new System.Drawing.Size(80, 24);
            this.btnSine.TabIndex = 1;
            this.btnSine.Text = "Sine";
            this.btnSine.Click += new System.EventHandler(this.btnSine_Click);
            // 
            // btnSpiral
            // 
            this.btnSpiral.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSpiral.Location = new System.Drawing.Point(158, 288);
            this.btnSpiral.Name = "btnSpiral";
            this.btnSpiral.Size = new System.Drawing.Size(80, 24);
            this.btnSpiral.TabIndex = 2;
            this.btnSpiral.Text = "Spiral";
            this.btnSpiral.Click += new System.EventHandler(this.btnSpiral_Click);
            // 
            // btnInverseX
            // 
            this.btnInverseX.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnInverseX.Location = new System.Drawing.Point(262, 288);
            this.btnInverseX.Name = "btnInverseX";
            this.btnInverseX.Size = new System.Drawing.Size(80, 24);
            this.btnInverseX.TabIndex = 3;
            this.btnInverseX.Text = "1/x";
            this.btnInverseX.Click += new System.EventHandler(this.btnInverseX_Click);
            // 
            // xyGraphControl1
            // 
            this.xyGraphControl1.AxisColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xyGraphControl1.ConnectPointsFlag = false;
            this.xyGraphControl1.CurrentIndex = 0;
            this.xyGraphControl1.DataColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xyGraphControl1.LabelX = "";
            this.xyGraphControl1.LabelY = "";
            this.xyGraphControl1.Location = new System.Drawing.Point(16, 16);
            this.xyGraphControl1.Name = "xyGraphControl1";
            this.xyGraphControl1.NumberOfPoints = 0;
            this.xyGraphControl1.Size = new System.Drawing.Size(336, 248);
            this.xyGraphControl1.TabIndex = 0;
            this.xyGraphControl1.TicksPerAxis = 10F;
            this.xyGraphControl1.Title = "Test";
            this.xyGraphControl1.XMaximum = 1F;
            this.xyGraphControl1.XMinimum = 0F;
            this.xyGraphControl1.XOrigin = 0F;
            this.xyGraphControl1.YMaximum = 1F;
            this.xyGraphControl1.YMinimum = 0F;
            this.xyGraphControl1.YOrigin = 0F;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(376, 317);
            this.Controls.Add(this.btnInverseX);
            this.Controls.Add(this.btnSpiral);
            this.Controls.Add(this.btnSine);
            this.Controls.Add(this.xyGraphControl1);
            this.Name = "Form1";
            this.Text = "XY Plot";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnSine_Click(object sender, System.EventArgs e)
		{
				xyGraphControl1.Reset();
				xyGraphControl1.XOrigin = 0;
				xyGraphControl1.YOrigin = -1;
				xyGraphControl1.LabelX = "Angle";
				xyGraphControl1.LabelY = "Amplitude";
		        xyGraphControl1.XMinimum = 0f;
		        xyGraphControl1.XMaximum = 6.28f;
		        xyGraphControl1.YMinimum = 0f;
		        xyGraphControl1.YMaximum = 2f;
		    xyGraphControl1.Title = "Sine Curve";

				for (float i = 0; i < 6.28; i += 6.28f/500f)
				{
					xyGraphControl1.AddPoint(i, (float)Math.Sin((double)i) + 1);
				}

				xyGraphControl1.Invalidate();
		}


        private void btnInverseX_Click(object sender, EventArgs e)
        {
			xyGraphControl1.Reset();
			xyGraphControl1.XOrigin = 0;
			xyGraphControl1.YOrigin = -1;
			xyGraphControl1.LabelX = "x value";
			xyGraphControl1.LabelY = "reciprocal value";
		    xyGraphControl1.XMinimum = .01f;
		    xyGraphControl1.XMaximum = 100f;
		    xyGraphControl1.YMinimum = .005f;
		    xyGraphControl1.YMaximum = 1f;
            xyGraphControl1.Title = "Reciprocal";

				for (float i = 1; i < 100; i += 1)
				{
					xyGraphControl1.AddPoint(i, 1f/i);
				}

            xyGraphControl1.Invalidate();
        }


		private void btnSpiral_Click(object sender, System.EventArgs e)
		{
            // clear the graph
			xyGraphControl1.Reset();

            // set up the graph parameters and labels
			xyGraphControl1.XOrigin = 0;
			xyGraphControl1.YOrigin = -1;
			xyGraphControl1.LabelX = "Sine of Angle";
			xyGraphControl1.LabelY = "Cosine of Angle";
		    xyGraphControl1.XMinimum = 0f;
		    xyGraphControl1.XMaximum = 3f;
		    xyGraphControl1.YMinimum = 0f;
		    xyGraphControl1.YMaximum = 3f;
		    xyGraphControl1.Title = "Spiral";
           
            // add the data into the graph
			for (float i = 0; i < 6.28 * 7; i += 6.28f/500f)
			{

				xyGraphControl1.AddPoint((float)Math.Sin((double)i) *(1- i/50.0f) + 1.5f, (float)Math.Cos((double)i)* (1 - i/50.0f) + 1.5f);
			}

            // force the graph to redraw
			xyGraphControl1.Invalidate();
		}

        private void Form1_Resize(object sender, EventArgs e)
        {
               xyGraphControl1.SetBounds(20, 20, ClientRectangle.Width - 20, ClientRectangle.Height - 50);
            xyGraphControl1.Invalidate();
        }


	}
}
