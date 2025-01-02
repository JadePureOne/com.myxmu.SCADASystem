using System.Windows;
using System.Windows.Controls;

namespace Common.Helpers
{
    /// <summary>
    /// 提供对 <see cref="PasswordBox"/> 控件的扩展功能的帮助类。
    /// </summary>
    public class PasswordBoxHelper
    {
        /// <summary>
        /// 获取 <see cref="PasswordProperty"/> 的值。
        /// </summary>
        /// <param name="obj">目标 <see cref="DependencyObject"/>。</param>
        /// <returns>密码字符串。</returns>
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        /// <summary>
        /// 设置 <see cref="PasswordProperty"/> 的值。
        /// </summary>
        /// <param name="obj">目标 <see cref="DependencyObject"/>。</param>
        /// <param name="value">要设置的密码字符串。</param>
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// 注册 Password 附加属性。
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata("",
                new PropertyChangedCallback(OnPasswordChanged)));
        

        /// <summary>
        /// 当 <see cref="PasswordProperty"/> 改变时调用。
        /// </summary>
        /// <param name="d">目标 <see cref="DependencyObject"/>。</param>
        /// <param name="e">属性更改事件参数。</param>
        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                // 移除旧的事件处理程序，防止重复添加
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                // 添加新的事件处理程序
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// 当 <see cref="PasswordBox"/> 的密码改变时调用。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">路由事件参数。</param>
        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // 更新 Password 附加属性的值
                SetPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}