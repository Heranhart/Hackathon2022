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
    /// Logique d'interaction pour EmailPage.xaml
    /// </summary>
    public partial class EmailPage : Page
    {
        public EmailPageVm Vm { get => (EmailPageVm)DataContext; }
        public EmailPage()
        {
            InitializeComponent();
            DataContext = new EmailPageVm();
        }
        private void Validate(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Vm.Domain) || string.IsNullOrEmpty(Vm.UserName) || string.IsNullOrEmpty(Vm.Extension))
            {
                MessageBox.Show("You left part of the email adress blank. Please make sure to enter a complete email adress.", "Incorrect mail building");
            }
            else if ((((Vm.UserName.Length < 5 && MessageBox.Show("That's a suspiciously short username you've entered. Are you sure that this is correct ?", "Short", MessageBoxButton.YesNo) == MessageBoxResult.Yes) || Vm.UserName.Length >= 5)
                && ((Vm.Extension.Length > 3 && MessageBox.Show("That's a pretty long enxtension you've entered. Are you sure that this is correct ?", "Long", MessageBoxButton.YesNo) == MessageBoxResult.Yes) || Vm.Extension.Length <= 3))
                )
            {
                string msg;
                if (MessageBox.Show(string.Format("So you have entered {0} for your adress email. Would you like to subscribe to a few dozens annoying newsletters that you'll never be able to unsubscribe from ?", Vm.ActualEmail), "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes
                   &&
                   MessageBox.Show("Really ? That's some really nasty stuff...", "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    msg = "Alright then. Look forward to it !";
                }
                else
                {
                    msg = "Ow... Welp, we tried !";
                }
                if (MessageBox.Show(msg + string.Format("\n\nBut in all seriousness, do you confirm {0} as your email address ?", Vm.ActualEmail), "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    MainWindowVM vm = mainWindow.DataContext as MainWindowVM;
                    vm.Email = Vm.ActualEmail;
                    mainWindow._main.Navigate(new DateOfBirthPage());
                }
            }
        }

        private void AddChar(object sender, RoutedEventArgs e)
        {
            AddCharWindow win = new AddCharWindow();
            win.Owner = App.Current.MainWindow;
            if (win.ShowDialog() ?? false)
            {
                switch (win.WhereChoice)
                {
                    case "Username":
                        Vm.UserName += win.CharChoice;
                        UserNameInput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                        break;
                    case "Domain":
                        Vm.Domain += win.CharChoice;
                        DomainInput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                        break;
                    case "Extension":
                        Vm.Extension += win.CharChoice;
                        ExtensionInput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                        break;
                }
            }
        }


        private void Reset(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("This will clear everything that you've entered so far. Are you sure about that ?", "Reset") == MessageBoxResult.Yes)
            {
                Vm.UserName = "";
                UserNameInput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                Vm.Domain = "";
                DomainInput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                Vm.Extension = "";
                ExtensionInput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            }
        }
    }
    public class EmailPageVm
    {
        public string UserName { get; set; }
        public string UserNameDisplay { get => string.IsNullOrEmpty(UserName) ? "(username)" : UserName; set => UserName = value; }
        public string Domain { get; set; }
        public string DomainDisplay { get => string.IsNullOrEmpty(Domain) ? "(domain)" : Domain; set => Domain = value; }
        public string Extension { get; set; }
        public string ExtensionDisplay { get => string.IsNullOrEmpty(Extension) ? "(extension)" : Extension; set => Extension = value; }
        public string ActualEmail { get => UserName + "@" + Domain + "." + Extension; }
        public EmailPageVm()
        {

        }
    }
}
