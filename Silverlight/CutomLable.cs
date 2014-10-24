using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Silverlight
{
    public class CutomLable:ContentControl
    {
        public string Text { get; set; }

        private TextBlock block;
        private StringBuilder ShowText;
        public CutomLable()
        {
            block = new TextBlock();
            ShowText = new StringBuilder(Text);
            block.Text = ShowText.ToString();
        }

       
        protected override Size MeasureOverride(Size availableSize)
        {
            //for (int i = Text.Length-1; i >=0 &&block.Width>availableSize.Width; i--)
            //{
            //    ShowText.Clear();
            //    ShowText.Append("...");
            //    ShowText.Insert(0, Text.Substring(0, i));
            //    block.Text = ShowText.ToString();
            //}
            
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }
    }
}
