using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;


namespace Uility.WPF.绑定代理
{
    public class MyTriger : TriggerBase<Button>
    {
       
        protected override void OnDetaching()
        {
             
            base.OnDetaching();
        }
        protected override void OnAttached()
        {
            AssociatedObject.Click += new System.Windows.RoutedEventHandler(button_Click);
           
            base.OnAttached();
        }

        void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           InvokeActions(null);
        }
    }

    
}
