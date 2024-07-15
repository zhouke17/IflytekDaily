using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTestDemo.ImageMemoryOptimization
{
    public class DisposableVirtualizingStackPanel : VirtualizingStackPanel
    {
        protected override void OnCleanUpVirtualizedItem(CleanUpVirtualizedItemEventArgs e)
        {
            base.OnCleanUpVirtualizedItem(e);
            if (e.UIElement is ListBoxItem listBoxItem)
            {
                ImageDrawingVisual imageDrawingVisual = FindVisualChild<ImageDrawingVisual>(listBoxItem);
                if (imageDrawingVisual != null)
                {
                    imageDrawingVisual.ClearImage();
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T correctlyTyped)
                {
                    return correctlyTyped;
                }
                else
                {
                    var result = FindVisualChild<T>(child);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}
