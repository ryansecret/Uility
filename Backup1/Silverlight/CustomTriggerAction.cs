using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Silverlight
{
    public class CustomTriggerAction : TriggerAction<Button>
    {

        protected override void Invoke(object parameter)
        {
         
        }
    }
    public class CutomBeheavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }
    }
}
