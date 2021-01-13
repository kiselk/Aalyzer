using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Text;

namespace GraphicsControlTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{

	    public bool capture_ctrl = false;
        public bool file_loaded = false;
        public bool first_time = true;
        MARGINS margins = new MARGINS();
	    public bool move_bool = false;
        private GraphicsControlTest.XYGraphControl xyGraphControl1;
        private TextBox textBox1;
        private TextBox eTimeThresh;
        private NumericUpDown nFileCount;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private TextBox eTime;
        private TextBox eStartI;
        private TextBox eEndI;
        private TextBox eDelayTotal;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox eStartTime;
        private TextBox eEndTime;
        private TextBox ePercent;
        private Label label7;
        private Label label8;
        private CheckBox cOnTop;
        private Panel pLine;

	    public Boolean time_meter = false;
	    public bool transparent = false;
        public int x = 0;
	    public int y = 0;
        private Label lLines;
        private NumericUpDown nLines;
        private Label label9;
        private Label label10;
	    public string filename_global = "";
        private Label lLinesThresh;
        private CheckBox cAutoLines;
	    public int start_lines;
        private NumericUpDown nYMax;
        private Label label11;
        private Label lClickedTranslation;
	    public int lines_size = 0;

        public static string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;

            while (guidResult.Length < length)
            {
                // Get the GUID.
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }

            // Make sure length is valid.
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

            // Return the first length bytes.
            return guidResult.Substring(0, length);
        }

	    public string unique_sess_key;
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
     
        private enum RenderMode { None, EntireWindow, TopWindow, Region };
        private RenderMode m_RenderMode;
        private Region m_blurRegion;

        public void trigger_transparent()
        {
            if(!transparent)
            {
                xyGraphControl1.BackColor = Color.Black;
                Form1.ActiveForm.BackColor = Color.Black;
                textBox1.Visible = false;
                eStartI.Visible = false;
                eStartTime.Visible = false;
                eTime.Visible = false;
                eTimeThresh.Visible = false;
                eEndI.Visible = false;
                eEndTime.Visible = false;
               // bGo.Visible = false;
                
                eDelayTotal.Visible = false;
                ePercent.Visible = false;
                nFileCount.Visible = false;
                cOnTop.Visible = false;
                //pLine.Visible = false;
                //bNext.Visible = false;
               // bPrev.Visible = false;
                nLines.Visible = false;
                lLines.Visible = true;
                lLinesThresh.Visible = true;
                cAutoLines.Visible = false;
                nYMax.Visible = false;
                margins.cyTopHeight = this.Height+20;
                IntPtr hWnd = this.Handle;
                int result = DwmExtendFrameIntoClientArea(hWnd, ref margins);
                //FillRectangle(Brushes.Black, this.ClientRectangle);
                transparent = true;

            }
            else
            {
                xyGraphControl1.BackColor = Color.WhiteSmoke;
                Form1.ActiveForm.BackColor = Color.WhiteSmoke;
                textBox1.Visible = true;
                eStartI.Visible = true;
                eStartTime.Visible = true;
                eTime.Visible = true;
                eTimeThresh.Visible = true;
                eEndI.Visible = true;
                eEndTime.Visible = true;
              //  bGo.Visible=true;
              
                eDelayTotal.Visible = true;
                ePercent.Visible = true;
                nFileCount.Visible = true;
                cOnTop.Visible = true;
                //pLine.Visible = true;
                lLines.Visible = false;
                lLinesThresh.Visible = false; 
                cAutoLines.Visible = true;
                nYMax.Visible = true;
              //  bNext.Visible = true;
              //  bPrev.Visible = true;
                nLines.Visible = true;
                
                margins.cyTopHeight = 0;
                lLines.Text = "";
                IntPtr hWnd = this.Handle;
                int result = DwmExtendFrameIntoClientArea(hWnd, ref margins);
                //FillRectangle(Brushes.WhiteSmoke, this.ClientRectangle);
                transparent = false;

            }
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg); // let the normal winproc process it

            const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x01;

            switch (msg.Msg)
            {
                case WM_NCHITTEST:
                    if (HTCLIENT == msg.Result.ToInt32())
                    {
                        // it's inside the client area

                        // Parse the WM_NCHITTEST message parameters
                        // get the mouse pointer coordinates (in screen coordinates)
                        Point p = new Point();
                        p.X = (msg.LParam.ToInt32() & 0xFFFF);// low order word
                        p.Y = (msg.LParam.ToInt32() >> 16); // hi order word

                        // convert screen coordinates to client area coordinates
                        p = PointToClient(p);

                        // if it's on glass, then convert it from an HTCLIENT
                        // message to an HTCAPTION message and let Windows handle it from then on
                        if (PointIsOnGlass(p))
                            msg.Result = new IntPtr(2);
                    }
                    break;

               
            }
        }

        private bool PointIsOnGlass(Point p)
        {
            // test for region or entire client area
            // or if upper window glass and inside it.
            // not perfect, but you get the idea
            return true;
        }


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.eTimeThresh = new System.Windows.Forms.TextBox();
            this.nFileCount = new System.Windows.Forms.NumericUpDown();
            this.eTime = new System.Windows.Forms.TextBox();
            this.eStartI = new System.Windows.Forms.TextBox();
            this.eEndI = new System.Windows.Forms.TextBox();
            this.eDelayTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.eStartTime = new System.Windows.Forms.TextBox();
            this.eEndTime = new System.Windows.Forms.TextBox();
            this.ePercent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cOnTop = new System.Windows.Forms.CheckBox();
            this.pLine = new System.Windows.Forms.Panel();
            this.lLines = new System.Windows.Forms.Label();
            this.nLines = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lLinesThresh = new System.Windows.Forms.Label();
            this.cAutoLines = new System.Windows.Forms.CheckBox();
            this.nYMax = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.xyGraphControl1 = new GraphicsControlTest.XYGraphControl();
            this.lClickedTranslation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(42, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1076, 84);
            this.textBox1.TabIndex = 4;
            // 
            // eTimeThresh
            // 
            this.eTimeThresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eTimeThresh.BackColor = System.Drawing.Color.White;
            this.eTimeThresh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eTimeThresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eTimeThresh.Location = new System.Drawing.Point(1050, 136);
            this.eTimeThresh.Name = "eTimeThresh";
            this.eTimeThresh.Size = new System.Drawing.Size(69, 20);
            this.eTimeThresh.TabIndex = 5;
            this.eTimeThresh.Text = "0.3";
            this.eTimeThresh.TextChanged += new System.EventHandler(this.eTimeThresh_TextChanged);
            // 
            // nFileCount
            // 
            this.nFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nFileCount.Enabled = false;
            this.nFileCount.Location = new System.Drawing.Point(1051, 110);
            this.nFileCount.Name = "nFileCount";
            this.nFileCount.Size = new System.Drawing.Size(34, 20);
            this.nFileCount.TabIndex = 7;
            this.nFileCount.ValueChanged += new System.EventHandler(this.nFileCount_ValueChanged);
            // 
            // eTime
            // 
            this.eTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eTime.Location = new System.Drawing.Point(1051, 283);
            this.eTime.Name = "eTime";
            this.eTime.Size = new System.Drawing.Size(68, 20);
            this.eTime.TabIndex = 9;
            // 
            // eStartI
            // 
            this.eStartI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eStartI.BackColor = System.Drawing.Color.White;
            this.eStartI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eStartI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eStartI.Location = new System.Drawing.Point(1051, 162);
            this.eStartI.Name = "eStartI";
            this.eStartI.Size = new System.Drawing.Size(68, 20);
            this.eStartI.TabIndex = 10;
            this.eStartI.Text = "0";
            // 
            // eEndI
            // 
            this.eEndI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eEndI.BackColor = System.Drawing.Color.White;
            this.eEndI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eEndI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eEndI.Location = new System.Drawing.Point(1050, 189);
            this.eEndI.Name = "eEndI";
            this.eEndI.Size = new System.Drawing.Size(69, 20);
            this.eEndI.TabIndex = 11;
            this.eEndI.Text = "0";
            // 
            // eDelayTotal
            // 
            this.eDelayTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eDelayTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eDelayTotal.Location = new System.Drawing.Point(1050, 309);
            this.eDelayTotal.Name = "eDelayTotal";
            this.eDelayTotal.Size = new System.Drawing.Size(68, 20);
            this.eDelayTotal.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(1014, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Time";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(995, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "StartLine";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(998, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "EndLine";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(950, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Delay over Thresh";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(992, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "StartTime";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(995, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "EndTime";
            // 
            // eStartTime
            // 
            this.eStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eStartTime.BackColor = System.Drawing.Color.White;
            this.eStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eStartTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eStartTime.Location = new System.Drawing.Point(1051, 215);
            this.eStartTime.Name = "eStartTime";
            this.eStartTime.Size = new System.Drawing.Size(68, 20);
            this.eStartTime.TabIndex = 19;
            this.eStartTime.Text = "0";
            // 
            // eEndTime
            // 
            this.eEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eEndTime.BackColor = System.Drawing.Color.White;
            this.eEndTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eEndTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eEndTime.Location = new System.Drawing.Point(1050, 241);
            this.eEndTime.Name = "eEndTime";
            this.eEndTime.Size = new System.Drawing.Size(68, 20);
            this.eEndTime.TabIndex = 20;
            this.eEndTime.Text = "0";
            // 
            // ePercent
            // 
            this.ePercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ePercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ePercent.Location = new System.Drawing.Point(1050, 335);
            this.ePercent.Name = "ePercent";
            this.ePercent.Size = new System.Drawing.Size(69, 20);
            this.ePercent.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(940, 338);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Percent over Thresh";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(1004, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Thresh";
            // 
            // cOnTop
            // 
            this.cOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cOnTop.AutoSize = true;
            this.cOnTop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cOnTop.Checked = true;
            this.cOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cOnTop.Location = new System.Drawing.Point(1001, 370);
            this.cOnTop.Name = "cOnTop";
            this.cOnTop.Size = new System.Drawing.Size(62, 17);
            this.cOnTop.TabIndex = 25;
            this.cOnTop.Text = "On Top";
            this.cOnTop.UseVisualStyleBackColor = true;
            this.cOnTop.CheckedChanged += new System.EventHandler(this.cOnTop_CheckedChanged);
            // 
            // pLine
            // 
            this.pLine.BackColor = System.Drawing.Color.Red;
            this.pLine.Location = new System.Drawing.Point(42, 550);
            this.pLine.Margin = new System.Windows.Forms.Padding(0);
            this.pLine.Name = "pLine";
            this.pLine.Size = new System.Drawing.Size(0, 5);
            this.pLine.TabIndex = 31;
            // 
            // lLines
            // 
            this.lLines.AutoSize = true;
            this.lLines.BackColor = System.Drawing.Color.Black;
            this.lLines.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLines.Location = new System.Drawing.Point(86, 36);
            this.lLines.Name = "lLines";
            this.lLines.Size = new System.Drawing.Size(0, 19);
            this.lLines.TabIndex = 34;
            this.lLines.UseCompatibleTextRendering = true;
            this.lLines.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lLines_MouseDown);
            // 
            // nLines
            // 
            this.nLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nLines.Location = new System.Drawing.Point(1050, 393);
            this.nLines.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nLines.Name = "nLines";
            this.nLines.Size = new System.Drawing.Size(34, 20);
            this.nLines.TabIndex = 35;
            this.nLines.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(1012, 395);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Lines";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(1012, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Page";
            // 
            // lLinesThresh
            // 
            this.lLinesThresh.AutoSize = true;
            this.lLinesThresh.BackColor = System.Drawing.Color.Transparent;
            this.lLinesThresh.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLinesThresh.Location = new System.Drawing.Point(83, 143);
            this.lLinesThresh.Name = "lLinesThresh";
            this.lLinesThresh.Size = new System.Drawing.Size(0, 19);
            this.lLinesThresh.TabIndex = 38;
            this.lLinesThresh.UseCompatibleTextRendering = true;
            this.lLinesThresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lLinesThresh_MouseDown);
            // 
            // cAutoLines
            // 
            this.cAutoLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAutoLines.AutoSize = true;
            this.cAutoLines.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cAutoLines.Checked = true;
            this.cAutoLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cAutoLines.Location = new System.Drawing.Point(987, 419);
            this.cAutoLines.Name = "cAutoLines";
            this.cAutoLines.Size = new System.Drawing.Size(76, 17);
            this.cAutoLines.TabIndex = 39;
            this.cAutoLines.Text = "Auto Lines";
            this.cAutoLines.UseVisualStyleBackColor = true;
            // 
            // nYMax
            // 
            this.nYMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nYMax.Location = new System.Drawing.Point(1050, 443);
            this.nYMax.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nYMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nYMax.Name = "nYMax";
            this.nYMax.Size = new System.Drawing.Size(35, 20);
            this.nYMax.TabIndex = 40;
            this.nYMax.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(1010, 445);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "YMax";
            // 
            // xyGraphControl1
            // 
            this.xyGraphControl1.AllowDrop = true;
            this.xyGraphControl1.AxisColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.xyGraphControl1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xyGraphControl1.ConnectPointsFlag = false;
            this.xyGraphControl1.CurrentIndex = 0;
            this.xyGraphControl1.DataColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xyGraphControl1.ForeColor = System.Drawing.Color.Black;
            this.xyGraphControl1.LabelX = "";
            this.xyGraphControl1.LabelY = "";
            this.xyGraphControl1.Location = new System.Drawing.Point(0, 0);
            this.xyGraphControl1.Name = "xyGraphControl1";
            this.xyGraphControl1.NumberOfPoints = 0;
            this.xyGraphControl1.Size = new System.Drawing.Size(1132, 594);
            this.xyGraphControl1.TabIndex = 0;
            this.xyGraphControl1.TicksPerAxis = 10F;
            this.xyGraphControl1.Title = "";
            this.xyGraphControl1.XMaximum = 1F;
            this.xyGraphControl1.XMinimum = 0F;
            this.xyGraphControl1.XOrigin = 0F;
            this.xyGraphControl1.YMaximum = 1F;
            this.xyGraphControl1.YMinimum = 0F;
            this.xyGraphControl1.YOrigin = 0F;
            this.xyGraphControl1.Load += new System.EventHandler(this.xyGraphControl1_Load);
            this.xyGraphControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.xyGraphControl1_DragDrop);
            this.xyGraphControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.xyGraphControl1_DragEnter);
            this.xyGraphControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xyGraphControl1_KeyPress);
            this.xyGraphControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xyGraphControl1_MouseDown);
            this.xyGraphControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xyGraphControl1_MouseMove);
            this.xyGraphControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.xyGraphControl1_MouseUp);
            // 
            // lClickedTranslation
            // 
            this.lClickedTranslation.AutoSize = true;
            this.lClickedTranslation.BackColor = System.Drawing.Color.Transparent;
            this.lClickedTranslation.ForeColor = System.Drawing.Color.Red;
            this.lClickedTranslation.Location = new System.Drawing.Point(290, 241);
            this.lClickedTranslation.Name = "lClickedTranslation";
            this.lClickedTranslation.Size = new System.Drawing.Size(0, 16);
            this.lClickedTranslation.TabIndex = 42;
            this.lClickedTranslation.UseCompatibleTextRendering = true;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1131, 594);
            this.Controls.Add(this.lClickedTranslation);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nYMax);
            this.Controls.Add(this.cAutoLines);
            this.Controls.Add(this.lLinesThresh);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nLines);
            this.Controls.Add(this.lLines);
            this.Controls.Add(this.pLine);
            this.Controls.Add(this.cOnTop);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ePercent);
            this.Controls.Add(this.eEndTime);
            this.Controls.Add(this.eStartTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eDelayTotal);
            this.Controls.Add(this.eEndI);
            this.Controls.Add(this.eStartI);
            this.Controls.Add(this.eTime);
            this.Controls.Add(this.nFileCount);
            this.Controls.Add(this.eTimeThresh);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.xyGraphControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            
            Application.Run(new Form1());
		}
        public ArrayList data = new ArrayList();
        public ArrayList time = new ArrayList();
        public ArrayList lines = new ArrayList();
        public ArrayList input = new ArrayList();
        public ArrayList props = new ArrayList();
        public ArrayList lines2 = new ArrayList();

        public float time_thresh = 0;

        private void ShowPart(int file_count)
        {
            xyGraphControl1.Reset();
            xyGraphControl1.XOrigin = 0;
            xyGraphControl1.YOrigin = -1;
            xyGraphControl1.LabelX = "";
            xyGraphControl1.LabelY = "";
            xyGraphControl1.XMinimum = 0f;

            xyGraphControl1.YMinimum = 0f;
            xyGraphControl1.YMaximum = (float)nYMax.Value+0.1f;
            xyGraphControl1.Title = "";

            //for (float i = 0; i < 5; i ++)
            //	{
            /*                   xyGraphControl1.AddPoint(1, 1);
                               xyGraphControl1.AddPoint(1, 2);
                               xyGraphControl1.AddPoint(2, 1);
                               xyGraphControl1.AddPoint(2, 3);
                               xyGraphControl1.AddPoint(3, 1);
                               xyGraphControl1.AddPoint(3, 4);

               */
            //}

            time_thresh = (float) Convert.ToDouble(eTimeThresh.Text);
            //int file_count = Convert.ToInt32(nFileCount.Value);
            this.Text = filename_global + ": Page " + file_count + " of " + nFileCount.Maximum;
            using (StreamReader sr = new StreamReader(System.IO.Path.GetTempPath() + "temp_file_"+unique_sess_key+"_"+file_count+".txt"))
            {
                string line;
                double c = 0;
                data.Clear();
                time.Clear();
                lines.Clear();
                input.Clear();
                lines2.Clear();

                int start_time_pos=0;
                int end_time_pos=0;
                float time_now=0;
                float time_prev=0;
                float time_diff = 0;
                while (((line = sr.ReadLine()) != null) && (c<300002))
                {

                    data.Add(line.Length);
                    line = Translator.TranslatePackets(line);
                    start_time_pos = line.IndexOf(":");
                    if (start_time_pos > 0)
                    {
                        end_time_pos = line.IndexOf("]", start_time_pos);
                        if (end_time_pos > 0)
                        {
                            while (line[start_time_pos + 1] == '0')
                            {
                                start_time_pos++;
                            }
                            time_now = (float) Convert.ToDouble(line.Substring(start_time_pos + 1, end_time_pos - start_time_pos - 1));
                            time.Add(time_now);
                            time_diff = time_now - time_prev;
                            if ((time_diff > time_thresh) && (c > 0))
                            {
                                props.Clear();
                                props.Add(c);
                                props.Add(time_diff);
                                lines2.Add(props.Clone());
                                lines.Add("L:" + c + " [" + time_diff.ToString("0.00") + "] " + line);
                            }
                            

                            time_prev = time_now;
                        }
                        else
                        { time.Add("0");
                        }
                }
                    else {
                        time.Add("0");
                    }
                          
                    c++;
                    input.Add(line);
                }
            }

            xyGraphControl1.XMaximum = 0.1f + (float)time.Count;
            /*for (int i = 0; i < data.Count; i++)
            {
                xyGraphControl1.AddPoint((float) i, 0f);
                xyGraphControl1.AddPoint((float) i, (float) Convert.ToInt32(data[i].ToString()));
            }
            */
            for (int i = 1; i < time.Count; i++)
            {
                xyGraphControl1.AddPoint((float) i, 0f);
                float time_diff = (float) Convert.ToDouble(time[i].ToString()) - (float) Convert.ToDouble(time[i - 1].ToString());
                if ((time_diff > 0) && (time_diff < 100))
                {
                   
                    xyGraphControl1.AddPoint((float)i, time_diff);

                } else xyGraphControl1.AddPoint((float)i, 0f);


                
            }
            textBox1.Text = "";
            foreach (var line1 in lines)

            {
                textBox1.Text += line1 + "\r\n";

            }
            xyGraphControl1.Invalidate();

        }

	    private void CalcTime()
	    {
	        if (time.Count>0)
	        {
	            int graph_left = xyGraphControl1.Left+25;
	            int graph_right = xyGraphControl1.Right;
	            int line_left = pLine.Left;
	            int line_right = pLine.Right;
	            double c_start_i = (line_left - graph_left)*xyGraphControl1.XMaximum;
                double c_end_i = (line_right - graph_left) * xyGraphControl1.XMaximum;
                double d_start_i = c_start_i / (xyGraphControl1.Width-25);
	            double d_end_i = c_end_i/(xyGraphControl1.Width-25);
	            int start_i = (int)d_start_i;
	            int end_i = (int) d_end_i-1;
	            float time_total = (float) Convert.ToDouble(time[end_i].ToString()) - (float) Convert.ToDouble(time[start_i].ToString());

                eTime.Text = Convert.ToString(time_total);
	            eEndI.Text = end_i.ToString();
	            eStartI.Text = start_i.ToString();
	            eStartTime.Text = time[start_i].ToString();
	            eEndTime.Text = time[end_i].ToString();
	            double delay_total = 0;
	            double time_current = 0;
	            double time_start_i=0;
	            double time_end_i=0;

                lines.Clear();
                lines2.Clear();
                
                for (int i=start_i+1;i<end_i;i++)
                {
                    
                    time_start_i = Convert.ToDouble(time[i - 1].ToString());
                    time_end_i = Convert.ToDouble(time[i].ToString());
                    if ((time_start_i > 0) && (time_end_i > 0))
                    {
                        time_current = time_end_i - time_start_i;

                        if (time_current > (float) time_thresh)
                        {
                        props.Clear();
                            props.Add(i);
                            props.Add(time_current);
                            //props.Add(input[i]);

                            delay_total += time_current;
                            lines.Add("L:" + i + " [" + time_current.ToString("0.00") + "] " + input[i].ToString());
                            lines2.Add(props.Clone());
                        }
                    }
                }
                textBox1.Text = "";
                foreach (var line1 in lines)
                {
                    textBox1.Text += line1 + "\r\n";

                }
	            eDelayTotal.Text = delay_total.ToString();
	            ePercent.Text = ((int) ((float) delay_total/(float) time_total*100f)).ToString() + "%";
	        }
	    }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (transparent)
            {
                if (cAutoLines.Checked == true)
                {
                    int size_in_pixels = (this.Height - 60)*80/100;
                    nLines.Value = size_in_pixels/20;
                } 
                margins.cyTopHeight = this.Height + 20;

                IntPtr hWnd = this.Handle;
                int result = DwmExtendFrameIntoClientArea(hWnd, ref margins);
                if(Screen.AllScreens.Length>1)
                {
                    Rectangle rect = Screen.GetBounds(this);
                   // int pos_left=this.Left;
                   // if (pos_left < 0) pos_left = -pos_left;
                        if (this.Width > rect.Width + 30)
                        {
                            lLines.Visible = true;
                            lLinesThresh.Left = rect.Width + 30;
                            lLinesThresh.Top = lLines.Top;
                            draw_lines_thresh();

                        }
                }
                    
            }
            
            xyGraphControl1.SetBounds(0, 0, ClientRectangle.Width - 0, ClientRectangle.Height - 0);
            xyGraphControl1.Invalidate();
           
        }

        public void draw_lines_thresh()
        {
            int counter = 0;
            int lines_count = (int)nLines.Value;
            lLinesThresh.Text = "";
            string[] text = textBox1.Lines;
            foreach (var s in text)
            {
                if (counter <= lines_count)
                {
                    lLinesThresh.Text += s + "\r\n";
                    counter++;
                }
            }
        }

        private void Split(string inputfile)
        {
            int count = 0;
            filename_global = inputfile;
            using (StreamReader sr = new StreamReader(inputfile))
            {
                
                string line;
                
                int line_count = 1;
                bool next_file = false;
                bool finished = false;
                while (!finished)
                {
                    next_file = false;
                    using (StreamWriter sw = new StreamWriter(System.IO.Path.GetTempPath()+"temp_file_"+unique_sess_key+"_" + count+".txt",false))
                    {

                        while ((!next_file)&&(!finished))
                        {
                            line = sr.ReadLine();
                            if (line == null) finished = true;
                            else
                            {
                                sw.WriteLine(line);
                                line_count++;
                                if (line_count > 300000)
                                {
                                    count++;
                                    line_count = 0;
                                    next_file = true;
                                }
                            }
                        }
                    }
                }

            }
            nFileCount.Enabled = true;
            nFileCount.Maximum = count;

        }

        private void xyGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void xyGraphControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((!transparent)||((transparent)&&(capture_ctrl)))
            {
                time_meter = true;
                pLine.Left = MousePosition.X - this.Left-8;
                pLine.Width = 0;
                pLine.Top = MousePosition.Y - this.Top-28;
                /*pLine.Top =
                    (int)
                    ((float) xyGraphControl1.Bottom - 31f -
                     (float) (xyGraphControl1.Height - 31)*(float) Convert.ToDouble(eTimeThresh.Text)/(xyGraphControl1.YMaximum - xyGraphControl1.YMinimum));
                */
                int yaxis_size_pixtel = xyGraphControl1.Bottom - 31;
                int line_pixels_above_yaxis = yaxis_size_pixtel - pLine.Top;
                float percent_of_axis = (float) line_pixels_above_yaxis/(float) (xyGraphControl1.Height - 31);

                eTimeThresh.Text = (percent_of_axis*(float)(xyGraphControl1.YMaximum - xyGraphControl1.YMinimum)).ToString();
                       


                if (pLine.Left < xyGraphControl1.Left + 31) pLine.Left = xyGraphControl1.Left + 31;
            } else
            { move_bool = true;
                x = MousePosition.X;
                y = MousePosition.Y;
                Point p = new Point(x,y);
                p = PointToClient(p);

            }

        }

        private void xyGraphControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((!transparent)||((transparent)&&(capture_ctrl)))
            {
                time_meter = false;
                capture_ctrl = false;
                
                CalcTime();
                draw_lines_thresh();
            }
            else
            {
                
                    move_bool = false;
            }
        }

	   
        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible

        static extern bool GetCursorPos(ref Point lpPoint);

        private void xyGraphControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((!transparent)||((transparent)&&capture_ctrl))
            {
                if (time_meter) pLine.Width = MousePosition.X - this.Left -8 - pLine.Left;
                if (pLine.Right > xyGraphControl1.Right) pLine.Width = xyGraphControl1.Right - pLine.Left;
            }
            else
            {
                if(move_bool)
                {
                    Point p = new Point();
                    
                    p.X = MousePosition.X;
                    p.Y = MousePosition.Y;
                    int xdiff = 0;
                    int ydiff=0;

                    xdiff = p.X - x;
                    ydiff = p.Y - y;


                    this.Left += xdiff;
                    this.Top += ydiff;
                    x = p.X;
                    y = p.Y;

                }
                else
                {
                    Point p = new Point();

                    p.X = MousePosition.X;
                    p = PointToClient(p);
                    int xdiff = 0;
                    

                    xdiff = p.X - x;


                    if (xdiff != 0)
                    {
                        if (input.Count > 0)
                        {
                            //int graph_left = xyGraphControl1.Left + 25;
                            //int graph_right = xyGraphControl1.Right;
                            double c_start_i = (p.X - 30)*xyGraphControl1.XMaximum/(xyGraphControl1.Width - 30);
                            start_lines = (int) c_start_i;
                            draw_lines(start_lines);
                        }


                        x = p.X;
                    }
                }
            }
        }

        public void draw_lines(int start)
        {
            lLines.Text = "";
            int size = (int)nLines.Value;
            if (start > input.Count - size+5) start = input.Count - size+5;
            if (start < 5) start = 5;
            for (int i = start-5; i < start + size-5; i++)
            {
                lLines.Text += input[i].ToString() + "\r\n";
            }
        }
        protected override void OnMouseWheel(MouseEventArgs mea)
        {
            if((transparent)&&(!move_bool)&&(file_loaded))
            {
               
                start_lines -= mea.Delta/120;
                draw_lines(start_lines);
            }


        }

	    private void trackBar1_Scroll(object sender, EventArgs e)
        {
      //      Form1.Opacity = (trackBar1.Value)/100;
        }



        private void cOnTop_CheckedChanged(object sender, EventArgs e)
        {

            if (cOnTop.Checked)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

	    //TODO windows temp 
        //dragdrop




        /// <summary>
        /// TRANSPARENT
        /// 
        ///
        /// 


        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
           IntPtr hWnd,
           ref MARGINS pMarInset
        );
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

/*
           
            margins.cxLeftWidth = 0;
            margins.cxRightWidth = 0;
            margins.cyTopHeight = 0;
            margins.cyBottomHeight = 0;

            IntPtr hWnd = this.Handle;
            int result = DwmExtendFrameIntoClientArea(hWnd, ref margins);
  */
            unique_sess_key = GetUniqueKey(6); 
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
          /*  using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(255, 245, 245, 245)))
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                //e.Graphics.DrawString("This is writing on glass", this.Font, textBrush, 10, 10);
            }*/
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        //    if (e.KeyChar == ' ') margins.cyTopHeight = this.Height;
            
        }

        private void xyGraphControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') trigger_transparent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            //Returns true if the character was processed by the control;

            //otherwise, false.

            bool bHandled = false;

            if ((transparent)&&(file_loaded))
            {
                int page = (int)nFileCount.Value;
                switch (keyData)
                {

                    case Keys.LButton|Keys.ShiftKey|Keys.Control
                    :

                        capture_ctrl = true;

                        bHandled = true;

                        break;
                    case Keys.PageUp:

                        start_lines -= (int) nLines.Value;

                        bHandled = true;

                        break;

                    case Keys.PageDown:

                        start_lines += (int) nLines.Value;

                        bHandled = true;

                        break;

                    case Keys.Up:

                        start_lines--;

                        bHandled = true;

                        break;

                    case Keys.Down:

                        start_lines++;

                        bHandled = true;

                        break;
                    case Keys.Left:

                        page--;
                        if(page>=nFileCount.Minimum)
                        {
                            
                            nFileCount.Value = page;
                            ShowPart(page);
                        }
                        

                        bHandled = true;

                        break;

                    case Keys.Right:

                        page++;
                        if(page<=nFileCount.Maximum)
                        {
                            
                            nFileCount.Value = page;
                            ShowPart(page);
                        }
                        bHandled = true;

                        break;

                }
                if (bHandled) draw_lines(start_lines);
            }
            return bHandled;

        }




	    private void button1_Click(object sender, EventArgs e)
        {

        }

        private void xyGraphControl1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // loop through the string array, adding each filename to the ListBox
            file_loaded = true; 
            Split(files[0]);
            ShowPart(0);
            


        }

        private void xyGraphControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // loop through the string array, adding each filename to the ListBox
            file_loaded = true;
            Split(files[0]);
            ShowPart(0);
            
        }

        private void bGo_Click(object sender, EventArgs e)
        {
            int file_count = Convert.ToInt32(nFileCount.Value);
            ShowPart(file_count);
        }

        private void bPrev_Click(object sender, EventArgs e)
        {
            int file_count = Convert.ToInt32(nFileCount.Value);
            if (file_count > nFileCount.Minimum)
            {
                file_count--;
                nFileCount.Value = file_count;
                ShowPart(file_count);
            }
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            int file_count = Convert.ToInt32(nFileCount.Value);
            if (file_count < nFileCount.Maximum)
            {
                file_count++;
                nFileCount.Value = file_count;
                ShowPart(file_count);
            }
        }
        public void deleteTempFiles()
        {
            for(int i=0;i<=Convert.ToInt32(nFileCount.Maximum) ;i++)
            {
                File.Delete(System.IO.Path.GetTempPath() + "temp_file_" + unique_sess_key + "_" + i + ".txt");
            }
       }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            deleteTempFiles();
        }

        private void eTimeThresh_TextChanged(object sender, EventArgs e)
        {
            time_thresh = (float) Convert.ToDecimal(eTimeThresh.Text);
        }

        private void nFileCount_ValueChanged(object sender, EventArgs e)
        {
            int file_count = Convert.ToInt32(nFileCount.Value);
           ShowPart(file_count);
            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if(first_time)
            {
                if (cAutoLines.Checked == true)
                {
                    int size_in_pixels = (this.Height - 60) * 80 / 100;
                    nLines.Value = size_in_pixels / 20;
                } 
                trigger_transparent();
                first_time = false;
            }
        }

        private void lLinesThresh_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(MousePosition.X,MousePosition.Y);
            p = PointToClient(p);
            int number_line_clicked = (p.Y-lLinesThresh.Top)/15;
            start_lines = Convert.ToInt32((lines2[number_line_clicked] as ArrayList)[0].ToString()) + 5;
            draw_lines(start_lines);


        }

        private void lLines_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(MousePosition.X, MousePosition.Y);
            p = PointToClient(p);
            int number_line_clicked = (p.Y - lLines.Top) / 15;
            int number_char_clicked = (p.X - lLines.Left) / 10;
            string line_clicked = input[number_line_clicked + start_lines].ToString();
            lClickedTranslation.Top = lLines.Top - 15;
            lClickedTranslation.Text = "Char"+line_clicked[number_char_clicked];

        }

       


	}
}
