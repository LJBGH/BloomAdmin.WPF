using System.Windows;
using System.Windows.Controls;

namespace BloomAdmin.Main.Common
{
    public class PasswordHelper
    {
        /// <summary>
        /// 密码属性，用于绑定 PasswordBox 的密码。由于 PasswordBox 的 Password 属性不是依赖属性，所以我们需要使用附加属性来实现绑定。
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",
                typeof(string),
                typeof(PasswordHelper),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnPasswordPropertyChanged)));

        private static bool isUpdating = false;
        public static string GetPassword(DependencyObject d)
        {
            return (string)d.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject d, string value)
        {

            d.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            if (!isUpdating)
                passwordBox.Password = e.NewValue as string;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }



        /// <summary>
        /// 属性附加属性，用于标识是否已经附加了事件处理程序，以避免重复附加事件。
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
                typeof(bool),
                typeof(PasswordHelper),
                new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(Attached)));

        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject d, bool value)
        {
            d.SetValue(AttachProperty, value);
        }


        private static void Attached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            isUpdating = true;
            SetPassword(passwordBox, passwordBox.Password);
            isUpdating = false;
        }
    }
}
