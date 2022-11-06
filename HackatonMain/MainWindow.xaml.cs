using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        MainWindowVM vm { get => this.DataContext as MainWindowVM; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
            
#if DEBUG
            _main.NavigationService.Navigate(new FirstNamePage());
#else
            _main.NavigationService.Navigate(new IntroPage());
#endif
            //LanguageComboBox.SelectedValue = "English";
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            new Help().Show();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HelpText.Background = PickBrush();
        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
        //private void OpenPolicyWindow(object sender, RoutedEventArgs e)
        //{
        //    PolicyWindow pol = new PolicyWindow();
        //    pol.Show();

        //}
        //private void OpenTosWindow(object sender, RoutedEventArgs e)
        //{
        //    TosWindow tos = new TosWindow();
        //    //tos.Owner = this;
        //    tos.ShowDialog();

        //}

        //private void CheckRead(object sender, RoutedEventArgs e)
        //{
        //   var vm = this.DataContext as MainWindowVM;
        //    if(!vm.UseAgreement || !vm.PolAgreement)
        //    {
        //        MessageBox.Show("Please make your you read both the Privacy Agreement and the Terms of Service");
        //        e.Handled = true;
        //        var c = (CheckBox)sender; 
        //        c.IsChecked = false;
        //    }
        //}

        //private void StartButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var vm = this.DataContext as MainWindowVM;
        //    if (vm.UseAgreement && vm.PolAgreement)
        //    {
        //        MessageBox.Show("Let's go!");
        //        // TODO suite
        //    }
        //    else
        //        MessageBox.Show("Please make sure you read both our terms of use and privacy policy. The links should bring you there.");
        //}
    }
}
