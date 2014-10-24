namespace Uility.WPF.控件
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public abstract class SLDialogBox
    {
        protected Popup _popup = new Popup();
        Control _parent = null;
        protected string _caption = string.Empty;
        public abstract UIElement GetControlTree();
        public abstract void WireHandlers();
        public abstract void WireUI();

        public SLDialogBox(Control parent, string caption)
        {
            _parent = parent;
            _caption = caption;
            _popup.Child = GetControlTree();
            WireUI();
            WireHandlers();
            AdjustPostion();

        }
        public void ShowDialog(bool isModal)
        {
            if (_popup.IsOpen)
                return; 
            _popup.IsOpen = true;
            ((UserControl)_parent).IsEnabled = false;
        }
        public void CloseDialog()
        {
            if (!_popup.IsOpen)
                return; 
            _popup.IsOpen = false;
            ((UserControl)_parent).IsEnabled = true;
        }
        private void AdjustPostion()
        {
            UserControl parentUC = _parent as UserControl;
            if (parentUC == null) return; 

            FrameworkElement popupElement = _popup.Child as FrameworkElement;
            if (popupElement == null) return;

            Double left = (parentUC.Width - popupElement.Width) / 2;
            Double top = (parentUC.Height - popupElement.Height) / 2;
            _popup.Margin = new Thickness(left, top, left, top);
        }
    }
}