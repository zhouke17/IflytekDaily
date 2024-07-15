using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfTestDemo.ImageMemoryOptimization
{
    public class ImageDrawingVisual : FrameworkElement
    {
        private DrawingVisual _drawingVisual;

        public ImageDrawingVisual()
        {
            _drawingVisual = new DrawingVisual();
            AddVisualChild(this._drawingVisual);
            AddLogicalChild(this._drawingVisual);
        }

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(ImageDrawingVisual), new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageDrawingVisual imageDrawingVisual)
            {
                imageDrawingVisual.LoadImage(e.NewValue as string);
            }
        }
        public void LoadImage(string imagePath)
        {
            using (var drawingContext = _drawingVisual.RenderOpen())
            {
                var bitmap = new BitmapImage();
                using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = fileStream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                drawingContext.DrawImage(bitmap, new Rect(0, 0, Width, Height));
            }
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _drawingVisual;
        }

        public void ClearImage()
        {
            using (var drawingContext = _drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, Width, Height));
            }
        }
    }
}
