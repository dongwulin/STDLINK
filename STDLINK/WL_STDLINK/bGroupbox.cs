using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

#region 自定义GroupBox in C#
/********************************************************/
//描述：自定义GroupBox
//Email: 3206498@sina.com
/********************************************************/
#endregion

namespace STDLINK
{
    public partial class bGroupbox : UserControl
    {
       
        private string text;

         #region  Properties
        [Browsable(true), 
        DefaultValue(typeof(string), "GroupBox"),
        Description("Text."),
        Category("WL_Properties"),
        ]

        //StartColor Properties
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                this.pictureBox5.Invalidate();
            }
        }
        #endregion
        
        public bGroupbox()
        {
            InitializeComponent();

            this.button1.Location = new Point(0, 0);
            this.button1.Size = this.Size;

            this.pictureBox1.Location = new Point(0,0);
            this.pictureBox2.Location = new Point(this.Width-18,0);
            this.pictureBox3.Location = new Point(0,this.Height-13);
            this.pictureBox4.Location = new Point(this.Width-11,this.Height-13);

            this.pictureBox6.Location = new Point(8,this.Height-4);
            this.pictureBox6.Size = new Size(this.Width-19,4);
            this.pictureBox5.Size = new Size(this.Width-43,21);

            

        }

       
        private void UserControl1_SizeChanged(object sender, EventArgs e)
        {

            this.button1.Location = new Point(0, 0);
            this.button1.Size = this.Size;

            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox2.Location = new Point(this.Width - 18, 0);
            this.pictureBox3.Location = new Point(0, this.Height - 13);
            this.pictureBox4.Location = new Point(this.Width - 11, this.Height - 13);

            this.pictureBox6.Location = new Point(8, this.Height - 4);
            this.pictureBox6.Size = new Size(this.Width - 19, 4);
            this.pictureBox5.Size = new Size(this.Width - 43, 21);
            
        }

      
        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
             Graphics a = e.Graphics;
            a.DrawString(text, bGroupbox.DefaultFont, Brushes.AliceBlue, new PointF(1, 5));
           
        }
       
    }
}