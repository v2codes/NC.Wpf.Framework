using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NC.Wpf.Framework.Mvvm
{
    /// <summary>
    /// 使用 Attached Property
    /// 创建一个自定义的 Attached Property，并将其绑定到 PasswordBox 的 Password 属性。
    /// 在 Attached Property 的回调函数中，你可以处理密码的变化，并将其存储在 ViewModel 中的 SecureString 属性中。
    /// </summary>
    public static class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("BindingPassword", typeof(string), typeof(PasswordHelper), new PropertyMetadata(string.Empty, OnBindingPasswordChanged));

        public static string GetBindingPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetBindingPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void OnBindingPasswordChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }

            // Remove any handler, we want our new handler to be in charge
            passwordBox.PasswordChanged -= PasswordChanged;

            if ((string)e.NewValue != passwordBox.Password)
            {
                passwordBox.Password = (string)e.NewValue;
            }

            // Now we can hook the new handler
            if ((string)e.NewValue != (string)e.OldValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            SetBindingPassword(passwordBox, passwordBox.Password);
        }
    }
}
