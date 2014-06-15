using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace STDLINK
{
    public partial class WL_Skin : Form
    {
        string[] fileList;
        Button[] buttonskin;
        string skinstr = "";
        string _path;
        public WL_Skin(string path)
        {
            InitializeComponent();
            _path = path;
            if (!System.IO.Directory.Exists(path + "\\skin"))
            {
                MessageBox.Show("皮肤目录skin不存在，请检查！");
            }
            else
            {
                fileList = System.IO.Directory.GetFiles(path + "\\skin", "*.ssk");
                string temstr=path + "\\skin\\";
                int temlen = temstr.Length;
                if (fileList.Length==0)
                {
                    MessageBox.Show("无皮肤文件，请检查！");
                }
                else
                {
                    buttonskin = new Button[fileList.Length];
                    for (int i = 0; i < fileList.Length; i++)
                    {
                        buttonskin[i] = new Button();
                        buttonskin[i].Location = new System.Drawing.Point(12 + 160 * (i % 6), 40 * (i/ 6)+12);
                        buttonskin[i].Name = buttonskin+i.ToString();
                        buttonskin[i].Size = new System.Drawing.Size(140, 24);
                        buttonskin[i].Text = fileList[i].Substring(temlen);
                        this.Controls.Add(buttonskin[i]);
                        buttonskin[i].Click += new EventHandler(WL_Skin_Click);
                    }
                }
            }

            try
            {
                string skinload = File.ReadAllText(path + "\\skin.dat");
                this.skinEngine1.SerialNumber = "";
                this.skinEngine1.SkinFile = path + "\\skin\\" + skinload;
                skinstr = skinload;
            }
            catch
            {

            }
        }

        private void WL_Skin_Click(object sender, EventArgs e)
        {
            Button bt=(Button)sender;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = _path + "\\skin\\" + bt.Text;
            skinstr = bt.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WL_Skin));
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            skinstr = "";
            this.skinEngine1.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinEngine1.SkinStreamMain")));
        }//无皮肤

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path + "\\skin.dat", skinstr);
            this.Close();
        }
    }
}
