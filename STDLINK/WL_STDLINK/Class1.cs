using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STDLINK
{
    static class Class1
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new WL_Skin(Application.StartupPath));
            Application.Run(new Form2());
        }
    }
}
namespace TestCollections
{
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    // A delegate type for hooking up change notifications.
    public delegate void ChangedEventHandler(object sender, System.EventArgs e);
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    // A class that works just like ArrayList, but sends event
    // notifications whenever the list changes.
    public class ListWithChangedEvent : System.Collections.ArrayList
    {
        // An event that clients can use to be notified whenever the
        // elements of the list change.
       
        public event ChangedEventHandler Changed;
       
        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(System.EventArgs e)
        {
            if (Changed != null)
            {
                string a = "bc";
                Changed(a, e);
            }
        }

        // Override some of the methods that can change the list;
        // invoke event after each
       
        public override int Add(object value)
        {
            int i = base.Add(value);
            OnChanged(System.EventArgs.Empty);
            return i;
        }
       
        public override void Clear()
        {
            base.Clear();
            OnChanged(System.EventArgs.Empty);
        }
      
        public override object this[int index]
        {
            set
            {
                base[index] = value;
                OnChanged(System.EventArgs.Empty);
            }
        }
    }
}

namespace TestEvents
{
    using TestCollections;

    class EventListener
    {
        private ListWithChangedEvent m_list;

        public EventListener(ListWithChangedEvent list)
        {
            m_list = list;

            // Add "ListChanged" to the Changed event on m_list:
            m_list.Changed += new ChangedEventHandler(ListChanged);
            m_list.Changed += delegate 
            {
                System.Console.WriteLine("This is");
            };
        }

        // This will be called whenever the list changes.
        private void ListChanged(object sender, System.EventArgs e)
        {
            System.Console.WriteLine(sender.ToString());
        }

        public void Detach()
        {
            // Detach the event and delete the list
            m_list.Changed -= new ChangedEventHandler(ListChanged);
            m_list = null;
        }
    }

    class Test
    {
        // Test the ListWithChangedEvent class.
        static void Main2()
        {
            // Create a new list.
            ListWithChangedEvent list = new ListWithChangedEvent();

            // Create a class that listens to the list's change event.
            EventListener listener = new EventListener(list);

            // Add and remove items from the list.
            list.Add("item 1");
            list.Clear();
            listener.Detach();
            while(true)
            {
            }
        }
    }
}

