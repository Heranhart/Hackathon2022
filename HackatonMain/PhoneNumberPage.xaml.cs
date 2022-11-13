using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour PhoneNumberPage.xaml
    /// </summary>
    public partial class PhoneNumberPage : Page
    {
        PhoneNumberPageVm Vm { get => (PhoneNumberPageVm)DataContext; }
        public PhoneNumberPage()
        {
            InitializeComponent();
            DataContext = new PhoneNumberPageVm();

        }

        private void RefreshX(object sender, TextChangedEventArgs e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            var s = (TextBox)sender;
            string str = s.Text;
            if (!string.IsNullOrEmpty(str))
            {

                vm.X = Convert.ToInt32(((TextBox)sender).Text);
            }
            else
            {
                vm.X = 0;
            }
            RefreshPhoneNumber();
        }
        private void RefreshY(object sender, TextChangedEventArgs e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            //vm.Y = Convert.ToInt32(((TextBox)sender).Text);
            var s = (TextBox)sender;
            string str = s.Text;
            if (!string.IsNullOrEmpty(str))
            {

                vm.Y = Convert.ToInt32(((TextBox)sender).Text);
            }
            else
            {
                vm.Y = 0;
            }
            RefreshPhoneNumber();

        }
        private void RefreshZ(object sender, TextChangedEventArgs e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            //vm.Z = Convert.ToInt32(((TextBox)sender).Text);
            var s = (TextBox)sender;
            string str = s.Text;
            if (!string.IsNullOrEmpty(str))
            {

                vm.Z = Convert.ToInt32(((TextBox)sender).Text);
            }
            else
            {
                vm.Z = 0;
            }
            RefreshPhoneNumber();

        }
        private void RefreshMod(object sender, TextChangedEventArgs e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            //vm.Z = Convert.ToInt32(((TextBox)sender).Text);
            var s = (TextBox)sender;
            string str = s.Text;
            if (!string.IsNullOrEmpty(str))
            {

                vm.Mod = Convert.ToInt32(((TextBox)sender).Text);
            }
            else
            {
                vm.Mod = 0;
            }
            RefreshPhoneNumber();

        }

        private void RefreshPhoneNumber()
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            long result = (long)(vm.A * (vm.X ?? 0)) + (long)((vm.Y ?? 0) * vm.B) - (long)((vm.Z ?? 0) * vm.C) + (long)(vm.Mod ?? 0);
            if (result < 1e10)
            {
                vm.PhoneNumber = Math.Max(result, 0).ToString().PadLeft(10, '0');
                PhoneNumberOutput.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            }
            else
            {
                MessageBox.Show("Wow there pard'ner ! Try to stick to the 10 provided digits, will ya ?", "That's a big number");
            }
        }

        private void CheckNumberInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void SliderA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            Slider slide = (Slider)sender;
            RefreshPhoneNumber();

        }
        private void SliderB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            RefreshPhoneNumber();
        }
        private void SliderC_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PhoneNumberPageVm vm = (PhoneNumberPageVm)DataContext;
            RefreshPhoneNumber();
        }
        private void Validate(object sender, RoutedEventArgs e)
        {
            long longNumber = Convert.ToInt64(Vm.PhoneNumber);
            if (longNumber == 0)
            {
                MessageBox.Show("Please enter a non-zero phone number using the provided inputs. You can tone it with the sliders !", "That's a lot of zeros...");
                return;
            }
            else if (longNumber < 1e6)
            {
                MessageBox.Show("Ok, that is a non-zero, but there's a suspicious amount of leading zeros, don't you think ?\nPlease enter an actual phone number !", "That's a pretty low number");
                return;
            }
            else if (Vm.PhoneNumber[0] != '0')
            {
                if (MessageBox.Show("Uhh you may have forgotten to leave a leading 0 in your number...\nAre you sure about that ?", "That's a pretty high number", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            if (((Vm.PhoneNumber.Contains("69") && MessageBox.Show("So your number contains 69, eh ?\nEheh, nice...", "Nice") == MessageBoxResult.OK) || !Vm.PhoneNumber.Contains("69"))
                && MessageBox.Show(string.Format("You have entered the phone number \"{0}\" in reversed.\nDo you confirm your entry ?", string.Join(string.Empty, Vm.PhoneNumber.Reverse().ToList())), "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                MainWindowVM vm = mainWindow.DataContext as MainWindowVM;
                vm.PhoneNumber = Vm.PhoneNumber;
                mainWindow._main.Navigate(new SummaryPage());
            }
        }

        private void FunFact(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[]
            {
                "Thanks for trusting this link !\n\nHere an actual fun fact regarding this madness :",
                "It is possible to write any integer as a linear combination of two prime numbers !\nIn plain english, that means that for any integer N (positive or negative) and a set of two prime integer a and b (for instance,3 and 7), there exists a set of integers x an y such that :\n\n N = a*x + b*y\n\nOf course, x and y can be either positive or negative as well.",
                "See how this relates to this page ? \nHere you can freely choose your coefficents (prime or not !), and you have more than two !",
                "Now then, can you find a combination that adds up to your phone number ?"
            };
            foreach (string line in lines)
            {
                MessageBox.Show(Application.Current.MainWindow, line, "Fun fact!");
            }
        }
    }

    public class PhoneNumberPageVm
    {
        public long A { get; set; }
        public long B { get; set; }
        public long C { get; set; }
        public long? X { get; set; }
        public long? Y { get; set; }
        public long? Z { get; set; }
        public long? Mod { get; set; }


        public string PhoneNumber { get; set; }

        public PhoneNumberPageVm()
        {
            A = 1057;
            B = 69420;
            C = 666;
            //X = 0;
            //Y = 0;
            //Z = 0;

            PhoneNumber = "0000000000";
        }
    }
}
