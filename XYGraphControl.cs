using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;


namespace GraphicsControlTest
{

	/// <summary>
	/// Summary description for XYGraphControl.
	/// </summary>
	public class XYGraphControl : System.Windows.Forms.UserControl, INotifyPropertyChanged
	{

		SolidBrush _axisNumbersBrush = new SolidBrush(Color.SlateGray);
		Font _axisFont               = new Font("Arial", 6);


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

			/// <summary>
			///    Required designer variable.
			/// </summary>

			private const int kXAxisIndent = 30;
			private const int kYAxisIndent = 30;

		    private GraphicsPath gpY = new GraphicsPath();
			private Matrix rotateMatrix = new Matrix();
		    private Font GraphFont = new Font("Arial", 8);
            Pen _graphPen = new Pen(Color.Maroon, 1);
        private Label lblTitle;
            Pen _axisPen = new Pen(Color.Blue, 1);

			public XYGraphControl()
			{
				// This call is required by the WinForms Form Designer.
                
				m_points.Add(new PointFloat(0f,0f));
				InitializeComponent();
        	    lblTitle.DataBindings.Add("Text", this, "Title");
				rotateMatrix.RotateAt(270, new PointF(ClientRectangle.Left + 5, (ClientRectangle.Bottom + ClientRectangle.Top)/2));

			    SetStyle(ControlStyles.UserPaint, true);
			    SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
//				InitializePoints();
				// TODO: Add any initialization after the InitForm call

			}

			/// <summary>
			///    Clean up any resources being used.
			/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public PointF[] GetGraphPoints(List<PointFloat> points)
		{
		   PointF[] thePointsArray = new PointF[points.Count];
			int i = 0;
			foreach (PointFloat pt in points)
			{
				thePointsArray[i] = pt.ToPoint();
				i++;
			}

			return thePointsArray;
		}


        public Color DataColor
        {
            get { return _graphPen.Color; }
            set { _graphPen = new Pen(value, 1); }
        }
        
        public Color AxisColor
        {
            get { return _axisPen.Color; }
            set { _axisPen = new Pen(value, 1); }
        }

	    public event PropertyChangedEventHandler PropertyChanged;
        private string _title = "Graph";
        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }



	    protected override void OnPaint( PaintEventArgs pe )
			{
				Graphics g = pe.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
				Rectangle rect = this.ClientRectangle; 
				DrawXAxis(g);
				DrawYAxis(g);

				if (m_nNumberOfPoints > 0)
				{
					if (m_bConnectPoints && m_nNumberOfPoints > 1)
					{
						for (int i = m_nNumberOfPoints; i < 100; i++)
						{
							// for now just set all the rest of the points to the last point
							m_points[i] = m_points[m_nNumberOfPoints - 1];
						}
						g.DrawLines(_graphPen, GetGraphPoints(m_points));
					}
					else
					{
						for (int i = 0; i < m_nNumberOfPoints - 1; i=i+2)
						{
							g.DrawLine(_graphPen, TranslateX(m_points[i].X), TranslateY(m_points[i].Y), TranslateX(m_points[i+1].X), TranslateY(m_points[i+1].Y));
						}
					}
				}

			}

            private float _xmaximum = 1;
            public float XMaximum
            {
                get { return _xmaximum; }
                set { _xmaximum = value; }
            }


            private float _xminimum = 0;
            public float XMinimum
	        {
	            get
	            {
	                return _xminimum;
	            }

                set
                {
                    _xminimum = value;
                }
	        }

            private float _ymaximum = 1;
            public float YMaximum
            {
                get { return _ymaximum; }
                set { _ymaximum = value; }
            }


            private float _yminimum = 0;
            public float YMinimum
	        {
	            get
	            {
	                return _yminimum;
	            }

                set
                {
                    _yminimum = value;
                }
	        }

	    private float XScaleFactor
	    {
	        get
	        {
	            return ((float)(ClientRectangle.Width - kXAxisIndent))/(_xmaximum - _xminimum);
	        }
	    }

	    private float YScaleFactor
	    {
	        get
	        {
	            return ((float)(ClientRectangle.Height - kYAxisIndent))/(_ymaximum - _yminimum);
	        }
	    }


			private int TranslateX(float x)
			{
				int tmp = 0;
				tmp = this.ClientRectangle.Left + kXAxisIndent + (int)(x*XScaleFactor);
				return tmp;
			}
			private int TranslateY(float y)
			{
				int tmp = 0;
				tmp = this.ClientRectangle.Bottom - ((int)(y * YScaleFactor) + kYAxisIndent);
				return tmp;
			}
			private int UntranslateX(int x)
			{
				int tmp = 0;
				tmp = (int)((float)x/XScaleFactor) - (this.ClientRectangle.Left + kXAxisIndent);
				return tmp;
			}
			private int UntranslateY(int y)
			{
				int tmp = 0;
				tmp = this.ClientRectangle.Bottom - ((int)((float)y/YScaleFactor) + kYAxisIndent);
				return tmp;
			}

		private int m_nCurrentIndex = 0;
		private List<PointFloat> m_points = new List<PointFloat>();

        //public float XValue
        //{
        //    get
        //    {
        //        return (float)(UntranslateX((m_points[m_nCurrentIndex]).X) * m_fXTickValue + XOrigin);
        //    }

        //    set
        //    {
        //        PointI p = (m_points[m_nCurrentIndex]);
        //        p.X = TranslateX((int)((value- XOrigin)/m_fXTickValue));
        //    }
        //}

        //public float YValue
        //{
        //    get
        //    {
        //        return (float)(UntranslateY((m_points[m_nCurrentIndex]).Y) * m_fYTickValue + YOrigin);
        //    }

        //    set
        //    {
        //        PointI p = (m_points[m_nCurrentIndex]);
        //        p.Y = TranslateY((int)((value- YOrigin)/m_fYTickValue) );
        //    }
        //}

        //public Point AbsolutePoint
        //{
        //    get
        //    {
        //        return (m_points[m_nCurrentIndex]).ToPoint();
        //    }

        //    set
        //    {
        //        m_points[m_nCurrentIndex] = new PointI(value);	
        //    }
        //}

		public int CurrentIndex
		{
			get
			{
				return m_nCurrentIndex;
			}

			set
			{
				m_nCurrentIndex = value;	
			}

		}

		private float m_ptXOrigin = 0.0f;
		private float m_ptYOrigin = 0.0f;

		public float XOrigin
		{
			get
			{
				return m_ptXOrigin;
			}

			set
			{
				m_ptXOrigin = value;	
			}

		}

		public float YOrigin
		{
			get
			{
				return m_ptYOrigin;
			}

			set
			{
				m_ptYOrigin = value;	
			}

		}

		private int m_nNumberOfPoints = 0;
		public int NumberOfPoints
		{
			get
			{
				return m_nNumberOfPoints;
			}

			set
			{
				m_nNumberOfPoints = value;
			}
		}



        private float _ticksPerAxis = 10;

        public float TicksPerAxis
        {
            get { return _ticksPerAxis; }
            set { _ticksPerAxis = value; }
        }


		private string m_LabelX = "";
		private string m_LabelY = "";

		public string LabelX
		{
			get
			{
			   return m_LabelX;
			}

			set
			{
			  m_LabelX = value;
			}

		}

		public string LabelY
		{
			get
			{
				return m_LabelY;
			}

			set
			{
				m_LabelY = value;
				gpY.Reset();
				gpY.AddString(m_LabelY, new FontFamily("Arial"), (int)FontStyle.Regular, 8, new PointF(ClientRectangle.Left + 5, (ClientRectangle.Bottom + ClientRectangle.Top)/2), new StringFormat());
				gpY.Transform(rotateMatrix);
			}

		}



		private bool m_bConnectPoints = false;
		public bool ConnectPointsFlag
		{
			get
			{
				return m_bConnectPoints;
			}
			set
			{
				m_bConnectPoints = value;
			}
		}

		private void InitializePoints()
		{
			for (int i = 0; i < 100; i++)
			{
				// for now just set all the rest of the points to the last point
				PointFloat p = m_points[i];
				p.X = TranslateX(0);
				p.Y = TranslateY(0);
//				m_points[i].X = TranslateX(0);
//				m_points[i].Y = TranslateY(0);
			}

		}

		public void AddPoint(float x, float y)
		{
			m_points.Add(new PointFloat(x, y));
			CurrentIndex ++;
			NumberOfPoints ++;
		}

		public void Reset() 
		{
			CurrentIndex = 0;
			NumberOfPoints = 0;
			m_points.Clear();
		}


        private void DrawXAxis(Graphics g)
		{
			g.DrawLine( _axisPen, ClientRectangle.Left + kXAxisIndent, ClientRectangle.Bottom - kYAxisIndent, ClientRectangle.Right, ClientRectangle.Bottom - kYAxisIndent );
			g.DrawString(m_LabelX, GraphFont, Brushes.Blue, new PointF(50, ClientRectangle.Bottom - kYAxisIndent/2), new StringFormat());
			Font theAxisFont = new Font("Arial", 6);
			try
			{
                float increment = CalculateIncrement(_xminimum, _xmaximum);
				for (float i = _xminimum; i < _xmaximum + (_xmaximum - _xminimum)/_ticksPerAxis; i += increment)
				{
						float xcalcValue = i;
						string strCalcValue = xcalcValue.ToString();
						int index = strCalcValue.IndexOf(".");
						if (index != - 1 && index + 2 < strCalcValue.Length)
						{
							strCalcValue = strCalcValue.Substring(0, index + 2);
						}
						g.DrawString(strCalcValue, theAxisFont, _axisNumbersBrush, TranslateX(i), this.ClientRectangle.Bottom - (kXAxisIndent - theAxisFont.Height/3));

					g.DrawLine(_axisPen, TranslateX(i), this.ClientRectangle.Bottom - kYAxisIndent - 3, TranslateX(i), this.ClientRectangle.Bottom - kYAxisIndent + 3);
				}		
			
			}
			catch (Exception e)
			{
				g.DrawString(e.Message, theAxisFont, _axisNumbersBrush, 10, 10);
			}
			theAxisFont.Dispose();
		}
		

        private void DrawYAxis(Graphics g)
		{
			g.DrawLine( _axisPen, ClientRectangle.Left + kXAxisIndent, ClientRectangle.Top, ClientRectangle.Left + kXAxisIndent, ClientRectangle.Bottom  - kYAxisIndent );
//			g.DrawPath(_graphPen, gpY);
			StringFormat theFormat = new StringFormat(StringFormatFlags.DirectionVertical);
            // calculate the size of the y-axis label
            SizeF labelSize = g.MeasureString(m_LabelY, GraphFont);
            // create a bitmap with the dimensions of the y-axis label
            Bitmap stringmap = new Bitmap((int)labelSize.Height + 1, (int)labelSize.Width + 1);
            // get a graphics object that will draw to the y-axis bitmap
            Graphics gbitmap = Graphics.FromImage(stringmap);
            gbitmap.SmoothingMode = SmoothingMode.HighQuality;
//			g.DrawString(m_LabelY, GraphFont, Brushes.Blue, new PointF(ClientRectangle.Left , ClientRectangle.Top + 100), theFormat);
            // translate the graphics surface width of the the label text
            gbitmap.TranslateTransform(0, labelSize.Width);
            // rotate the graphics surface back 90 degrees
            gbitmap.RotateTransform(-90);
            // draw the string into the transformed graphics surface
			gbitmap.DrawString(m_LabelY, GraphFont, Brushes.Blue, new PointF(0 , 0), new StringFormat(StringFormatFlags.NoClip));
            // draw the bitmap containing the rotated string to the graph
            g.DrawImage(stringmap, (float)ClientRectangle.Left, (float)ClientRectangle.Top + 100);
            float increment = CalculateIncrement(_yminimum, _ymaximum);
            for (float i = _yminimum; i < _ymaximum + (_ymaximum - _yminimum) / _ticksPerAxis; i += increment)
			{
					float ycalcValue = i;
					string strCalcValue = ycalcValue.ToString();
					int index = strCalcValue.IndexOf(".");
					if (index != - 1 && index + 2 < strCalcValue.Length)
					{
						strCalcValue = strCalcValue.Substring(0, index + 2);
					}
					g.DrawString(strCalcValue, _axisFont, _axisNumbersBrush, this.ClientRectangle.Left + kXAxisIndent - (_axisFont.Height + 6), TranslateY(i));
				
				
               g.DrawLine(_axisPen, this.ClientRectangle.Left + kXAxisIndent - 3, TranslateY(i), this.ClientRectangle.Left + kXAxisIndent + 3, TranslateY(i));
			}

            gbitmap.Dispose();
            stringmap.Dispose();
		}

        /// <summary>
        /// Calculate an increment value that makes sense
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float CalculateIncrement(float min, float max)
        {
            // figure out a raw increment value to come close to
            float increment =  (max - min) /TicksPerAxis ;
            
            // get the precision value on the right side of the decimal
            float precision = increment - ((int) increment);

            // round this value to a whole number, and track
            // it's multiple
            float multiple = 1;
            while (precision < 1f)
            {
                precision *= 10;
                multiple *= 10;
            }

            // these are the precision values we will allow on
            // the right side of the decimal for our graph
            float[] allowablePrecisions = new float[]{1, 2, 5};

            // find the closest precision (1,2, or 5) to the precision
            // value of our raw increment.
            float minimumPrecision = allowablePrecisions[0];
            foreach (float nextPrecision in allowablePrecisions)
            {
                if (Math.Abs(precision - minimumPrecision) > 
                    Math.Abs(precision - nextPrecision))
                {
                    // use .5 as increment
                    minimumPrecision = nextPrecision;
                }
            }

            // determine a new precision value
            precision = minimumPrecision/multiple;

            // calculate the new increment by adding the precision
            // to the integer value
            increment = ((int) increment) + precision;

            return increment;
        }


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(124, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TextChanged += new System.EventHandler(this.lblTitle_TextChanged);
            // 
            // XYGraphControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblTitle);
            this.Name = "XYGraphControl";
            this.Size = new System.Drawing.Size(336, 248);
            this.Resize += new System.EventHandler(this.XYGraphControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void XYGraphControl_Resize(object sender, EventArgs e)
        {
            lblTitle.Location = new Point(Width/2, 0);
        }

        private void lblTitle_TextChanged(object sender, EventArgs e)
        {
           
        }


	    ///<summary>
	    ///Occurs when a property value changes.
	    ///</summary>
	    ///
	}
}
