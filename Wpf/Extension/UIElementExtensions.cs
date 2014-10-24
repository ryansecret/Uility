using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf.Extension
{
    public static class UIElementExtensions
    {
        /// <summary>
        ///   Renders the ui element into a bitmap frame.
        /// </summary>
        /// <param name = "element">The UI element.</param>
        /// <returns>The created bitmap frame</returns>
        public static BitmapSource RenderToBitmap(this UIElement element)
        {
           
            return element.RenderToBitmap(1);
          
        }

        /// <summary>
        ///   Renders the ui element into a bitmap frame using the specified scale.
        /// </summary>
        /// <param name = "element">The UI element.</param>
        /// <param name = "scale">The scale (default: 1).</param>
        /// <returns>The created bitmap frame</returns>
        public static BitmapSource RenderToBitmap(this UIElement element, double scale)
        {
            var renderWidth = (int)(element.RenderSize.Width * scale);
            var renderHeight = (int)(element.RenderSize.Height * scale);
             
         
            var renderTarget = new RenderTargetBitmap(renderWidth, renderHeight, 96, 96, PixelFormats.Pbgra32);
            var sourceBrush = new VisualBrush(element);

            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(element.RenderSize.Width, element.RenderSize.Height)));
            }
             
            renderTarget.Render(drawingVisual);

            return renderTarget;
        }
    }
}
