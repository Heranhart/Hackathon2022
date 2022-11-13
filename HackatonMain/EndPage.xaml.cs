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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HackatonMain
{
    /// <summary>
    /// Logique d'interaction pour EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        public string Time { get; set; }
        public EndPage()
        {
            DateTime t1 = ((MainWindowVM)Application.Current.MainWindow.DataContext).StartTime;
            DateTime t2 = DateTime.Now;
            TimeSpan t3 = t2 - t1;

            Time = "";
            Time += t3.Hours > 0 ? t3.Hours.ToString().PadLeft(2,'0') + " hour(s) " : "";
            Time += t3.Minutes > 0 ? t3.Minutes.ToString().PadLeft(2, '0') + " minute(s) " : "";
            Time += t3.Seconds.ToString().PadLeft(2, '0') + " second(s)";
            InitializeComponent();
            DataContext = this;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("This will completely reset this window, allowing you to start again from scratch.\nAre you ready ?","Again ?",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow = new MainWindow();
            }
        }
    }
}
