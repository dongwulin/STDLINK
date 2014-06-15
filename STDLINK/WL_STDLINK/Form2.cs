using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace STDLINK
{
    public partial class Form2 : Form
    {
        STDLINK.k842G k842;
        int ch = 0;
        public Form2()
        {
            InitializeComponent();
           
            foreach (Control con in this.Controls)
            {
                this.comboBox2.Items.Add(con.Name);
            }

            k842 = new STDLINK.k842G((short)768);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (Control con in this.Controls)
            //{
            //    if (con.Name = this.comboBox2.Text)
            //    {
            //        this.propertyGrid1.SelectedObject = this.gett;
            //    }
            //} 
            Type type = sender.GetType();
            
            MessageBox.Show(type.Name);
            
        }

        /// <summary>
        /// 根据控件名和属性名取值
        /// </summary>
        /// <param name="ClassInstance">控件所在实例</param>
        /// <param name="ControlName">控件名</param>
        /// <param name="PropertyName">属性名</param>
        /// <returns>属性值</returns>
        public  Object GetValueControlProperty(Object ClassInstance, string ControlName, string PropertyName)
        {
            Object Result = null;

            Type myType = ClassInstance.GetType();

            FieldInfo myFieldInfo = myType.GetField(ControlName, BindingFlags.NonPublic | //"|"为或运算，除非两个位均为0，运算结果为0，其他运算结果均为1
             BindingFlags.Instance);

            if (myFieldInfo != null)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(myFieldInfo.FieldType);

                PropertyDescriptor myProperty = properties.Find(PropertyName, false);

                if (myProperty != null)
                {
                    Object ctr;

                    ctr = myFieldInfo.GetValue(ClassInstance);

                    try
                    {
                        Result = myProperty.GetValue(ctr);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// 根据控件名和属性名赋值
        /// </summary>
        /// <param name="ClassInstance">控件所在实例</param>
        /// <param name="ControlName">控件名</param>
        /// <param name="PropertyName">属性名</param>
        /// <param name="Value">属性值</param>
        /// <returns>属性值</returns>
        public  Object SetValueControlProperty(Object ClassInstance, string ControlName, string PropertyName, Object Value)
        {
            Object Result = null;

            Type myType = ClassInstance.GetType();

            FieldInfo myFieldInfo = myType.GetField(ControlName, BindingFlags.NonPublic
             | BindingFlags.Instance);
            if (myFieldInfo != null)
            {
                //myFieldInfo.
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(myFieldInfo.FieldType);

                PropertyDescriptor myProperty = properties.Find(PropertyName, false);  //这里设为True就不用区分大小写了

                if (myProperty != null)
                {
                    Object ctr;

                    ctr = myFieldInfo.GetValue(ClassInstance); //取得控件实例

                    try
                    {
                        myProperty.SetValue(ctr, Value);

                        Result = ctr;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return Result;
        }

        //以下实现Label1.Text=TextBox1.Text,Label2.Text=TextBox2
        private void button1_Click(object sender, EventArgs e)
        {
           int i;
        
           for( i = 1;i<= 2;i++)
           {
            this.SetValueControlProperty(this, "Label" + i.ToString(), "Text", GetValueControlProperty(this, "TextBox" + i.ToString(), "Text"));
           }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            this.label4.Text = k842.In((short)ch).ToString();
            this.label5.Text = ch.ToString();
            ch++;
            if (ch>31)
            {
                ch = 0;
            }
        }

    }
}
