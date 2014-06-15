using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

#region Color Button in C#
/********************************************************/
//描述：渐变色按钮
//Email: 3206498@sina.com
/********************************************************/
#endregion

namespace STDLINK
{
    //[ToolboxBitmap(@"d:\BIN Empty.ico")] //控件图标
    public class bButton : System.Windows.Forms.Control
    {
        int stat = 99;

        Pen ZPen;
        Pen OZPen;
        Pen DZPen;

        GraphicsPath gp;

        protected Color EndColor = Color.White;
        protected Color StartColor = Color.Gray;
        protected Color PenColor = Color.Gray;
        protected LinearGradientMode lgm = LinearGradientMode.Vertical;

        protected Color OEndColor = Color.Honeydew;
        protected Color OStartColor = Color.Green;
        protected Color OPenColor = Color.Green;
        protected LinearGradientMode Olgm = LinearGradientMode.Vertical;

        protected Color DEndColor = Color.Honeydew;
        protected Color DStartColor = Color.Lime;
        protected Color DPenColor = Color.Lime;
        protected LinearGradientMode Dlgm = LinearGradientMode.Vertical;
        protected SmoothingMode Smoothing_Mod = SmoothingMode.AntiAlias;
        public enum ButtonStyle
        {
            Rectangle = 0,
            Ellipse = 1
        }

        ButtonStyle inputfilter = ButtonStyle.Ellipse;
        float radius = 20;
        float penwith_n = 1;
        float penwith_d = 2;
        float penwith_h = 1;
        int textmove_x = 1;
        int textmove_y = 1;

        public bButton()
        {
            SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.DoubleBuffer |
                    ControlStyles.Selectable |
                    ControlStyles.UserMouse,
                    true
                    );
        }

        #region 属性

        #region StartColor Properties
        [
        DefaultValue(typeof(Color), "Honeydew"),
        Description("StartColor."),
        Category("WL_Properties"),
        ]

        //StartColor Properties
        public Color Normal_Start_Color
        {
            get
            {
                return StartColor;
            }
            set
            {
                StartColor = value;
                Invalidate();
            }
        }
        #endregion

        #region EndColor Properties
        [
        DefaultValue(typeof(Color), "Green"),
        Description("EndColor."),
        Category("WL_Properties"),
        ]

        //GradientColorTwo Properties
        public Color Normal_End_Color
        {
            get
            {
                return EndColor;
            }
            set
            {
                EndColor = value;
                Invalidate();
            }
        }

        #endregion

        #region PenColor Properties
        [
        DefaultValue(typeof(Color), "Green"),
        Description("Normal_BorderColor."),
        Category("WL_Properties"),
        ]

        //GradientColorTwo Properties
        public Color Normal_BorderColor
        {
            get
            {
                return PenColor;
            }
            set
            {
                PenColor = value;
                Invalidate();
            }
        }

        #endregion

        #region LinearGradientMode Properties
        //LinearGradientMode Properties
        [
        DefaultValue(typeof(LinearGradientMode), "Vertical"),
        Description("Gradient Mode"),
        Category("WL_Properties"),
        ]

        public LinearGradientMode Normal_GradientMode
        {
            get
            {
                return lgm;
            }

            set
            {
                lgm = value;
                Invalidate();
            }
        }
        #endregion


        #region OStartColor Properties
        [
        DefaultValue(typeof(Color), "White"),
        Description("OStartColor."),
        Category("WL_Properties"),
        ]

        //StartColor Properties
        public Color Hover_Start_Color
        {
            get
            {
                return OStartColor;
            }
            set
            {
                OStartColor = value;
                Invalidate();
            }
        }
        #endregion

        #region OEndColor Properties
        [
        DefaultValue(typeof(Color), "Lime"),
        Description("OEndColor."),
        Category("WL_Properties"),
        ]

        //GradientColorTwo Properties
        public Color Hover_End_Color
        {
            get
            {
                return OEndColor;
            }
            set
            {
                OEndColor = value;
                Invalidate();
            }
        }

        #endregion

        #region OPenColor Properties
        [
        DefaultValue(typeof(Color), "Lime"),
        Description("Hover_BorderColor."),
        Category("WL_Properties"),
        ]

        //GradientColorTwo Properties
        public Color Hover_BorderColor
        {
            get
            {
                return OPenColor;
            }
            set
            {
                OPenColor = value;
                Invalidate();
            }
        }

        #endregion

        #region OLinearGradientMode Properties
        //LinearGradientMode Properties
        [
        DefaultValue(typeof(LinearGradientMode), "Vertical"),
        Description("Gradient Mode"),
        Category("WL_Properties"),
        ]

        public LinearGradientMode Hover_GradientMode
        {
            get
            {
                return Olgm;
            }

            set
            {
                Olgm = value;
                Invalidate();
            }
        }
        #endregion


        #region DStartColor Properties
        [
        DefaultValue(typeof(Color), "Honeydew"),
        Description("StartColor."),
        Category("WL_Properties"),
        ]

        //StartColor Properties
        public Color Down_Start_Color
        {
            get
            {
                return DStartColor;
            }
            set
            {
                DStartColor = value;
                Invalidate();
            }
        }
        #endregion

        #region DEndColor Properties
        [
        DefaultValue(typeof(Color), "Lime"),
        Description("DEndColor."),
        Category("WL_Properties"),
        ]

        //GradientColorTwo Properties
        public Color Down_End_Color
        {
            get
            {
                return DEndColor;
            }
            set
            {
                DEndColor = value;
                Invalidate();
            }
        }

        #endregion

        #region DPenColor Properties
        [
        DefaultValue(typeof(Color), "Lime"),
        Description("Down_BorderColor."),
        Category("WL_Properties"),
        ]

        //GradientColorTwo Properties
        public Color Down_BorderColor
        {
            get
            {
                return DPenColor;
            }
            set
            {
                DPenColor = value;
                Invalidate();
            }
        }

        #endregion

        #region DLinearGradientMode Properties
        //LinearGradientMode Properties
        [
        DefaultValue(typeof(LinearGradientMode), "Vertical"),
        Description("DGradient Mode"),
        Category("WL_Properties"),
        ]

        public LinearGradientMode Down_GradientMode
        {
            get
            {
                return Dlgm;
            }

            set
            {
                Dlgm = value;
                Invalidate();
            }
        }
        #endregion


        #region radius Properties
        [
        DefaultValue(typeof(float), "20"),
        Description("radius."),
        Category("WL_Properties"),
        ]

        //radius Properties
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                try
                {
                    radius = value;
                }
                catch
                {
                    //MessageBox.Show("请输入整数");
                }

                if (radius == 0)
                    radius = 1;

                Invalidate();
            }
        }
        #endregion

        #region ButtonStyle
        [
               DefaultValue(ButtonStyle.Ellipse),
               Category("WL_Properties"),
            ]
        public ButtonStyle InputFilter
        {
            get
            {
                return inputfilter;
            }
            set
            {
                inputfilter = value;
            }
        }
        #endregion

        #region SmoothingMode
        [
            DefaultValue(SmoothingMode.AntiAlias),
            Category("WL_Properties"),
        ]
        public SmoothingMode Smoothing_Mode
        {
            get
            {
                return Smoothing_Mod;
            }
            set
            {
                Smoothing_Mod = value;
            }
        }
        #endregion


        #region penwith_n Properties
        [
        DefaultValue(typeof(float), "1"),
        Description("penwith_n"),
        Category("WL_Properties"),
        ]

        public float penwithn
        {
            get
            {
                return penwith_n;
            }
            set
            {
                try
                {
                    penwith_n = value;
                }
                catch 
                {
                    //MessageBox.Show("请输入整数");
                }

                Invalidate();
            }
        }
        #endregion

        #region penwith_d Properties
        [
        DefaultValue(typeof(float), "2"),
        Description("penwith_d"),
        Category("WL_Properties"),
        ]

        public float penwithd
        {
            get
            {
                return penwith_d;
            }
            set
            {
                try
                {
                    penwith_d = value;
                }
                catch 
                {
                    //MessageBox.Show("请输入整数");
                }

                Invalidate();
            }
        }
        #endregion

        #region penwith_h Properties
        [
        DefaultValue(typeof(float), "2"),
        Description("penwith_h"),
        Category("WL_Properties"),
        ]

        public float penwithh
        {
            get
            {
                return penwith_h;
            }
            set
            {
                try
                {
                    penwith_h = value;
                }
                catch
                {
                    //MessageBox.Show("请输入整数");
                }

                Invalidate();
            }
        }
        #endregion


        #region textmove_x Properties
        [
        DefaultValue(typeof(int), "1"),
        Description("textmove_x"),
        Category("WL_Properties"),
        ]

        public int textmovex
        {
            get
            {
                return textmove_x;
            }
            set
            {
                try
                {
                    textmove_x = value;
                }
                catch 
                {
                    //MessageBox.Show("请输入整数");
                }

                Invalidate();
            }
        }
        #endregion

        #region textmove_y Properties
        [
        DefaultValue(typeof(int), "1"),
        Description("textmove_y"),
        Category("WL_Properties"),
        ]

        public int textmovey
        {
            get
            {
                return textmove_y;
            }
            set
            {
                try
                {
                    textmove_y = value;
                }
                catch
                {
                    //MessageBox.Show("请输入整数");
                }

                Invalidate();
            }
        }
        #endregion

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = Smoothing_Mod;

            gp = new GraphicsPath();

            ZPen = new Pen(PenColor, penwith_n);
            OZPen = new Pen(OPenColor, penwith_h);
            DZPen = new Pen(DPenColor, penwith_d);

            StringFormat SF = new StringFormat();
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            int X = 0;
            int Y = 0;
            int width = this.Width - 2;
            int height = this.Height - 2;
            Rectangle rect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 3, ClientRectangle.Height - 3);
            Rectangle Drect = new Rectangle(0, 0, width - 1 + textmove_x, height - 2 + textmove_y);//文字动

            LinearGradientBrush ZBrush;
            LinearGradientBrush OZBrush;
            LinearGradientBrush DZBrush;

            if (InputFilter == ButtonStyle.Ellipse)
            {
                gp.AddEllipse(rect);


            }
            else
            {

                gp.AddLine(X + radius, Y + 1, X + width - (radius * 2), Y + 1);

                gp.AddArc(X + width - (radius * 2), Y + 1, radius * 2 - 1, radius * 2 - 1, 270, 90);

                gp.AddLine(X + width - 1, Y + radius, X + width - 1, Y + height - (radius * 2));

                gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2 - 1, radius * 2 - 1, 0, 90);

                gp.AddLine(X + width - (radius * 2), Y + height - 1, X + radius, Y + height - 1);

                gp.AddArc(X + 1, Y + height - (radius * 2), radius * 2 - 1, radius * 2 - 1, 90, 90);

                gp.AddLine(X + 1, Y + height - (radius * 2), X + 1, Y + radius);

                gp.AddArc(X + 1, Y + 1, radius * 2 - 1, radius * 2 - 1, 180, 90);

                gp.CloseFigure();
            }

            switch (stat)
            {

                case 19://UP                        
                    OZBrush = new LinearGradientBrush(rect, Hover_Start_Color, Hover_End_Color, Olgm);
                    e.Graphics.FillPath(OZBrush, gp);
                    e.Graphics.DrawPath(OZPen, gp);
                    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, SF);
                    break;

                case 9://DOWN
                    DZBrush = new LinearGradientBrush(rect, Down_Start_Color, Down_End_Color, Dlgm);
                    e.Graphics.FillPath(DZBrush, gp);
                    e.Graphics.DrawPath(DZPen, gp);
                    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), Drect, SF);
                    break;

                case 2://ENTER
                    OZBrush = new LinearGradientBrush(rect, Hover_Start_Color, Hover_End_Color, Olgm);
                    e.Graphics.FillPath(OZBrush, gp);
                    e.Graphics.DrawPath(OZPen, gp);
                    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, SF);
                    break;

                case 3://MOVE
                    ZBrush = new LinearGradientBrush(rect, Normal_Start_Color, Normal_End_Color, lgm);
                    e.Graphics.FillPath(ZBrush, gp);
                    e.Graphics.DrawPath(ZPen, gp);
                    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, SF);
                    break;

                default:
                    ZBrush = new LinearGradientBrush(rect, Normal_Start_Color, Normal_End_Color, lgm);
                    e.Graphics.FillPath(ZBrush, gp);
                    e.Graphics.DrawPath(ZPen, gp);
                    e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, SF);
                    break;
            }


        }

        #region Method  override
        protected override void OnMouseDown(MouseEventArgs mevent)
        {

            stat = 9;
            Invalidate();

            base.OnMouseDown(mevent);

        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            stat = 19;
            Invalidate();

            base.OnMouseUp(mevent);
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            stat = 2;
            Invalidate();

            base.OnMouseEnter(eventargs);
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            stat = 3;
            Invalidate();

            base.OnMouseLeave(eventargs);
        }

        //protected override void OnMouseMove(MouseEventArgs mevent);
        #endregion

    }
}
