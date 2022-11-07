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
    /// Logique d'interaction pour LastNamePage.xaml
    /// </summary>
    public partial class LastNamePage : Page
    {
        public int keyboardLen { get; set; }
        Char[] chars = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-".ToCharArray();
        public string LastName { get; set; }
        public LastNamePage()
        {
            keyboardLen = 30;
            InitializeComponent();
            GenerateKeyboard(keyboardLen);
        }

        List<Run> runs { get; set; }
        private void GenerateKeyboard(int len)
        {

            runs = new List<Run>();
            Run run;
            Label label;
            while (len-- > 0)
            {
                //run = new Run(chars[new Random().Next(chars.Length)].ToString());
                label = new Label();

                run = new Run(chars[new Random().Next(chars.Length)].ToString());
                run.Name = "l" + len.ToString();

                //label.Name = "l" + len.ToString();

                //label.Content = run;
                //label.MouseLeftButtonDown += this.Label_MouseLeftButtonDown;

                //Keyboard.Inlines.Add(label);
                runs.Add(run);
            }

            foreach (Run la in runs)
            {
                Keyboard.Inlines.Add(la);
                //Run r = la.Content as Run;

                la.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => Label_MouseLeftButtonDown(sender, e, la.Name, la.Text));
            }
        }

        private void RegenerateKeyboard(object sender, RoutedEventArgs e)
        {
            Keyboard.Inlines.Clear();
            GenerateKeyboard(keyboardLen);
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            this.LastName = "";
            //Vm.LastName = "";
            //InputTextBox.Text = "";
            Keyboard.Inlines.Clear();
            GenerateKeyboard(keyboardLen);
        }
        private void Reset()
        {
            this.LastName = "";
            //Vm.LastName = "";
            //InputTextBox.Text = "";
            Keyboard.Inlines.Clear();
            GenerateKeyboard(100);
        }
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, string name, string content)
        {
            //Run label = sender as Run;
            Run label = sender as Run;
            //label.Visibility = Visibility.Hidden;
            //for (int i = 0; i < Keyboard.Inlines.Count; i++)
            foreach (Inline elt in Keyboard.Inlines.ToList()/*.Where(e => !string.IsNullOrWhiteSpace(e.Name))*/)
            {
                //var elt = Keyboard.Inlines.ToList()[i] ;
                if (elt != null && name == elt.Name)
                {
                    //var l = FindName(label.Name) as Label;
                    //FirstName += content;
                    //this.Vm.FirstName += content;
                    //InputTextBox.Text += content;
                    //FirstName += elt.Text;
                    Keyboard.Inlines.Remove(elt);
                }
            }
        }
        private void Validate(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Keyboard.Text))
            {
                MessageBox.Show("Error 404 Entry not found","It would seem you have erased all the provided text, resulting in an empty name entry. We can't have that. Please use the links at the bottom of the window to try again.");
                return;
            }
            var result = MessageBox.Show(string.Format("You have \"written\" {0} as your last name. Do you confirm ?", Keyboard.Text), "Please confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (MessageBox.Show("Really ? You won't be able to change it later.", "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow mw = Application.Current.MainWindow as MainWindow;
                    MainWindowVM vm = mw.DataContext as MainWindowVM;
                    vm.LastName = Keyboard.Text;
                    mw._main.NavigationService.Navigate(new SummaryPage());
                }
                else
                    Validate(sender, e);
            }
        }

        private void MoreChars(object sender, RoutedEventArgs e)
        {
            GenerateKeyboard(30);
        }
    }
}
