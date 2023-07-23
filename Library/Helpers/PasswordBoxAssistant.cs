using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Library.Helpers
{
    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(SecureString),
                typeof(PasswordBoxAssistant), new PropertyMetadata(null, OnBoundPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool),
                typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordChanged;

                if (!(bool)passwordBox.GetValue(UpdatingPassword))
                {
                    passwordBox.Password = (e.NewValue as SecureString)?.ToUnsecureString();
                }

                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.SetValue(UpdatingPassword, true);
                SetBoundPassword(passwordBox, passwordBox.SecurePassword.Copy());
                passwordBox.SetValue(UpdatingPassword, false);
            }
        }

        public static void SetBoundPassword(DependencyObject dp, SecureString value)
        {
            dp.SetValue(BoundPassword, value);
        }

        public static SecureString GetBoundPassword(DependencyObject dp)
        {
            return (SecureString)dp.GetValue(BoundPassword);
        }

        private static string ToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
