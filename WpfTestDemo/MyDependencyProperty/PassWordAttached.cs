using System.Windows;
using System.Windows.Controls;

namespace WpfTestDemo.MyDependencyProperty
{
    public class PassWordAttached
    {
        public static string GetPwd(DependencyObject obj)
        {
            return (string)obj.GetValue(PwdProperty);
        }

        public static void SetPwd(DependencyObject obj, string value)
        {
            obj.SetValue(PwdProperty, value);
        }

        // Using a DependencyProperty as the backing store for Pwd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PwdProperty =
            DependencyProperty.RegisterAttached("Pwd", typeof(string), typeof(PassWordAttached), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>//使用绑定源MyPassWord的默认值触发Pwd的属性值变化的回调函数，创建PasswordBox内容变化的事件，从而实现将值传递出去给到绑定源。间接监控了不能进行内容绑定的密码值。
            {
                var passwordbox = s as PasswordBox;
                if (passwordbox != null)
                {
                    passwordbox.PasswordChanged -= Passwordbox_PasswordChanged;
                    passwordbox.PasswordChanged += Passwordbox_PasswordChanged;
                }
            })));

        private static void Passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            SetPwd(passwordBox, passwordBox.Password);//将密码内容Password传递出去给到自定义的依赖属性MyPassWord，从而间接实现数据绑定。
        }
    }
}
