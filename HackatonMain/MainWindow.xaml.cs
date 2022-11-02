using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
            LanguageComboBox.SelectedValue = "English";
        }

        private void OpenPolicyWindow(object sender, RoutedEventArgs e)
        {
            PolicyWindow pol = new PolicyWindow();
            pol.Show();

        }
        private void OpenTosWindow(object sender, RoutedEventArgs e)
        {
            TosWindow tos = new TosWindow();
            //tos.Owner = this;
            tos.ShowDialog();

        }

        private void CheckRead(object sender, RoutedEventArgs e)
        {
           var vm = this.DataContext as MainWindowVM;
            if(!vm.UseAgreement || !vm.PolAgreement)
            {
                MessageBox.Show("Please make your you read both the Privacy Agreement and the Terms of Service");
                e.Handled = true;
                var c = (CheckBox)sender; 
                c.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MainWindowVM;
            if (vm.UseAgreement && vm.PolAgreement)
            {
                MessageBox.Show("Let's go!");
                // TODO suite
            }
            else
                MessageBox.Show("Please make sure you read both our terms of use and privacy policy. The links should bring you there.");
        }
    }
}
