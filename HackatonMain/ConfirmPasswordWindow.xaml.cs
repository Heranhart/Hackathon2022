using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HackatonMain
{
    /// <summary>
    /// Logique d'interaction pour ConfirmPasswordWindow.xaml
    /// </summary>
    public partial class ConfirmPasswordWindow : Window
    {
        public string Password { get; set; }
        public string PasswordReversed { get; set; }
        public ConfirmPasswordWindow(string password)
        {
            PasswordReversed = string.Join("", password.Reverse().ToList());
            InitializeComponent();
        }

        private void ShowPassword(object sender, MouseEventArgs e)
        {
            TextBlockPw.Visibility = Visibility.Visible;
            TextBlockPw.Text = Password;
            PwBox.Visibility = Visibility.Hidden;
        }

        private void HidePassword(object sender, MouseEventArgs e)
        {
            TextBlockPw.Visibility = Visibility.Hidden;
            PwBox.Visibility = Visibility.Visible;
        }

        private void AlsoHidePassword(object sender, MouseButtonEventArgs e)
        {
            TextBlockPw.Visibility = Visibility.Hidden;
            PwBox.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Password = PwBox.Password;
            if (Password == PasswordReversed)
                this.DialogResult = true;
            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PwBox.Password;
        }

        private void PwBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                 e.Command == ApplicationCommands.Cut ||
                 e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
