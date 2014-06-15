using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace STDLINK
{
    public partial class WL_ClockEdit : Form
    {
        PictureBox picbox_edit;
        Graphics graphics_edit;
        public WL_ClockEdit(PictureBox picbox,Graphics graphics)
        {
            InitializeComponent();
            picbox_edit = picbox;
            graphics_edit = graphics;
        }

        private void WL_ClockEdit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            picbox_edit.BackColor = Color.White;
            graphics_edit.DrawEllipse(Pens.Black, 5, 5, 30, 30);
            
            picbox_edit.Invalidate();
        }
    }
}
