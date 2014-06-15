using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

#region AddControls Library in C#
/********************************************************/
//描述：动态加入winform控件
//Email: 3206498@sina.com
/********************************************************/
#endregion

namespace STDLINK
{
    
    public class WL_AddControls<my_type> 
        where my_type : System.Windows.Forms.Control,new()
    {
        ArrayList list;
        Point chazhi;
        Timer t;
       
        System.Windows.Forms.Control HostForm;
        Color nor_color;
        Color sel_color;
        Type type;
        
        public WL_AddControls(System.Windows.Forms.Control host)
        {
            HostForm = host;
            list = new ArrayList();
            t = new Timer();
            t.Enabled = false;
            t.Interval = 100;
            t.Tick += new EventHandler(t_Tick);
        }

        public void Remove_bt()
        {
            HostForm.Controls.Remove(G.gbt);
            list.Remove(G.gbt);
            G.gbt.Dispose();
        }

        public my_type Add_bt(Color Nor_color, Color Sel_color)
        {
            nor_color = Nor_color;
            sel_color = Sel_color;
            my_type bt = new my_type();//也可以用下面的语句创建实例

            //bt = System.Activator.CreateInstance<my_type>();
            
            bt.BackColor = Nor_color;
            list.Add(bt);

            HostForm.Controls.Add(bt);

            bt.MouseDown += new MouseEventHandler(bt_MouseDown);
            bt.MouseUp += new MouseEventHandler(bt_MouseUp);
            return bt;
        }

        void bt_MouseUp(object sender, MouseEventArgs e)
        {

            t.Enabled = false;

        }

        void bt_MouseDown(object sender, MouseEventArgs e)
        {
            if (G.gbt != null)
                G.gbt.BackColor = Color.Blue;
            G.gbt = (my_type)sender;
            G.gbt.BackColor = Color.Red;
            t.Enabled = true;
            chazhi = new Point(Control.MousePosition.X - G.gbt.Location.X, Control.MousePosition.Y - G.gbt.Location.Y);

            type = sender.GetType();
        }

        #region 未解决动态改变属性问题 总是提示找不到构造函数 晕～
        //public void change_text(string text, string[] text2)
        //{
        //    //MessageBox.Show(type.ToString());
        //    Object[] args = new Object[] { 8 };

        //    Object obj = type.InvokeMember(null,
        //        BindingFlags.DeclaredOnly |
        //        BindingFlags.Public | BindingFlags.NonPublic |
        //        BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);

        //    type.InvokeMember("Name", BindingFlags.SetProperty, null, obj, new string[] { "aaa" });

        //}

        //public void change_bg(int r, int g, int b)
        //{
        //   // type.InvokeMember("BackColor", BindingFlags.SetProperty, null, obj, new string[] { "abc" });

        //}
        #endregion

        void t_Tick(object sender, EventArgs e)
        {
            G.gbt.Location = new Point(Control.MousePosition.X - chazhi.X, Control.MousePosition.Y - chazhi.Y);
        }

    }

}
