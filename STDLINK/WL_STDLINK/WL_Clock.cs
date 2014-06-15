using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace STDLINK
{
    
        [Designer(typeof(CustomControlDesigner))]
        public partial class WL_Clock : PictureBox
        {
            
            public WL_Clock()
            {
                InitializeComponent();

                G.picturebox = this;
            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                
                this.Invalidate();
            }

            private void WL_Clock_Paint(object sender, PaintEventArgs e)
            {
               // e.Graphics.DrawString(System.DateTime.Now.ToLongTimeString(), this.Font, new SolidBrush(this.ForeColor), e.ClipRectangle);
            }

            protected override void OnPaint(PaintEventArgs pe)
            {
                G.graphics = pe.Graphics;
                // TODO: Add custom paint code here
                pe.Graphics.FillRectangle(new SolidBrush(this.BackColor), pe.ClipRectangle);
                pe.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), pe.ClipRectangle);
                pe.Graphics.DrawString(System.DateTime.Now.ToLongTimeString(), this.Font, new SolidBrush(this.ForeColor), pe.ClipRectangle);
                // Calling the base class OnPaint
                base.OnPaint(pe);
            }

            public new string Text
            {
                get { return base.Text; }
                set { base.Text = value; this.Refresh(); }
            }
        }
        

        public class CustomControlDesigner : ControlDesigner
        {
            public override System.ComponentModel.Design.DesignerActionListCollection ActionLists
            {
                get
                {
                    DesignerActionListCollection collection = new DesignerActionListCollection();

                    collection.Add(new CustomControlActionList(this.Component, this));
                    return collection;
                }
            }

            public override DesignerVerbCollection Verbs
            {
                get
                {
                    DesignerVerbCollection designerVerbs = new DesignerVerbCollection();
                    designerVerbs.Add(new DesignerVerb("编辑", new EventHandler(this.OnEdit)));
                    designerVerbs.Add(new DesignerVerb("添加", new EventHandler(this.OnAdd)));
                    return designerVerbs;
                }
            }
            public void OnEdit(object sender, EventArgs e)
            {
                WL_ClockEdit clockedit = new WL_ClockEdit(G.picturebox,G.graphics);
                clockedit.ShowDialog();
                //MessageBox.Show("OnEdit");
            }
            public void OnAdd(object sender, EventArgs e)
            {
                MessageBox.Show("OnAdd");
            }

        }

        public class CustomControlActionList :System.ComponentModel.Design.DesignerActionList
        {
            private DesignerActionUIService _service;
            private CustomControlDesigner _designer;

            public CustomControlActionList(IComponent c, CustomControlDesigner designer)
                : base(c)
            {
                this._designer = designer;
                this._service = c.Site.GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
                this.AutoShow = true;
            }
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection item = new DesignerActionItemCollection(); 
                item.Add(new DesignerActionHeaderItem("Appearance"));
                item.Add(
                        new DesignerActionPropertyItem("Text",
                         "Text String", "Appearance",
                         "Sets the display text."));
                return item;
            }

            private WL_Clock CustomControl
            {
                get
                {
                    return this.Component as WL_Clock;
                }
            }

            public string Text
            {
                get
                {
                    return CustomControl.Text;
                }
                set
                {
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(CustomControl)["Text"];
                    descriptor.SetValue(CustomControl, value);

                    this._service.Refresh(this.Component);
                }
            }
        }

}
