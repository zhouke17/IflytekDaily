using System.Windows;

namespace CustomControls
{
    public class CornerButton : System.Windows.Controls.Button
    {
        public CornerRadius MyCornerRadius
        {
            get { return (CornerRadius)GetValue(MyCornerRadiusProperty); }
            set { SetValue(MyCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCornerRadiusProperty =
            DependencyProperty.Register("MyCornerRadius", typeof(CornerRadius), typeof(CornerButton));


    }
}
