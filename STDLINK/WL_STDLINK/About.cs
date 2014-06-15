using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

#region About in C#
/********************************************************/
//√Ë ˆ£∫About
//Email: 3206498@sina.com
/********************************************************/
#endregion

namespace STDLINK
{
    /// <summary>
    /// public partial class About : Form
    /// 
    /// </summary>
    public partial class About : Form
    {
        /// <summary>
        /// public string str;
        /// 
        /// </summary>
        public string str;

        /// <summary>
        /// public About(string name)
        /// 
        /// </summary>
        public About(string name)
        {
            InitializeComponent();
            lblProduct.Text = name;
            str = System.Windows.Forms.Application.StartupPath;

            string[] fileEntries = Directory.GetFiles(str,"*.*",SearchOption.AllDirectories);

            foreach (string fileName in fileEntries)
            {
             string str_re= fileName.Remove(0, str.Length+1);
             this.lvComponents.Items.Add(str_re); 
            }

            FileStream fs=new FileStream(str + "\\log.txt", FileMode.OpenOrCreate);
            fs.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
          
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(str + "\\log.txt");   
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}