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

namespace HackatonMain
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class IntroPage : Page
    {
        //MainWindowVM vm { get => this.DataContext as MainWindowVM; }
        public IntroPage()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }
        private void OpenPolicyWindow(object sender, RoutedEventArgs e)
        {
            PolicyWindow pol = new PolicyWindow();
            ((MainWindowVM)DataContext).PolAgreement = pol.ShowDialog() ?? false;

        }
        private void OpenTosWindow(object sender, RoutedEventArgs e)
        {
            TosWindow tos = new TosWindow();
            tos.Owner = Application.Current.MainWindow;
            ((MainWindowVM)DataContext).UseAgreement =  tos.ShowDialog()??false;

        }

        private void CheckRead(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MainWindowVM;
            if (!vm.UseAgreement || !vm.PolAgreement)
            {
                MessageBox.Show("Please make sure you read both the Privacy Agreement and the Terms of Service before clicking the box.");
                //e.Handled = true;
                var c = (CheckBox)sender;
                c.IsChecked = false;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowVM)DataContext ;/* Application.Current.MainWindow.DataContext as MainWindowVM;*/
            if (vm.UseAgreement && vm.PolAgreement)
            {
                MessageBox.Show("Let's go!");
                MainWindow mv = Application.Current.MainWindow as MainWindow;
                mv._main.NavigationService.Navigate(new FirstNamePage());
            }
            else
                MessageBox.Show("Please make sure you read both our Terms of Service and privacy policy. The links should bring you there.");
        }
    }
}
