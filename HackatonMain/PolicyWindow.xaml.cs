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
    /// Logique d'interaction pour SubWin.xaml
    /// </summary>
    public partial class PolicyWindow : Window
    {
        public bool HasBeenRead { get; set; }
        public PolicyWindow()
        {
            InitializeComponent();
            LoremText.Height = SubPage.Height - LabelTitle.Height - 50;
            MainWindow mw = (MainWindow)Application.Current.MainWindow;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.HasBeenRead)
            {

                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                MainWindowVM vm = (MainWindowVM)mw.DataContext;
                vm.PolAgreement = true;
            }
        }

        private void LoremText_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                this.HasBeenRead = true;
            }

        }
    }
}


