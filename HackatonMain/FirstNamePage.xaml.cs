using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour FirstNamePage.xaml
    /// </summary>
    public partial class FirstNamePage : Page
    {
        public int Key { get; set; }
        FirstNamePageVm Vm { get => DataContext as FirstNamePageVm; set => DataContext = value; }

        Char[] chars = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-".ToCharArray();
        public string FirstName { get; set; }
        public FirstNamePage()
        {
            DataContext = new FirstNamePageVm();
            InitializeComponent();
            Key = 0;
            
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
                    FirstName += content;
                    this.Vm.FirstName += content;
                    InputTextBox.Text += content;
                    //FirstName += elt.Text;
                    Keyboard.Inlines.Remove(elt);
                }
            }

        }
        List<Run> runs { get; set; }
        private void GenerateKeyboard(int len)
        {
            Key += len;
            runs=new List<Run>();
            Run run;
            Label label;
            while (len-- > 0)
            {
                //run = new Run(chars[new Random().Next(chars.Length)].ToString());
                label = new Label();

                run = new Run(chars[new Random().Next(chars.Length)].ToString());
                run.Name = "l" + (len+Key).ToString();
                
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
            GenerateKeyboard(100);
        }        
        
        private void Reset(object sender, RoutedEventArgs e)
        {
            this.FirstName = "";
            Vm.FirstName = "";
            InputTextBox.Text = "";
            Keyboard.Inlines.Clear();
            GenerateKeyboard(100);
        }        
        private void Reset()
        {
            this.FirstName = "";
            Vm.FirstName = "";
            InputTextBox.Text = "";
            Keyboard.Inlines.Clear();
            GenerateKeyboard(100);
        }
        private void Validate(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTextBox.Text))
            {
                MessageBox.Show("You must enter a first name, preferably yours. You can find it on your ID card, passport, driver's license, to name a few.");
                MessageBox.Show("If you are struggling with this step, please ask a relative.");
                return;
            }
            var result = MessageBox.Show(string.Format("You have \"written\" {0} as your first name. Do you confirm ?",InputTextBox.Text), "Please confirm", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                if (MessageBox.Show("Really ? You won't be able to change it later.", "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow mw = Application.Current.MainWindow as MainWindow;
                    MainWindowVM vm = mw.DataContext as MainWindowVM;
                    vm.FirstName = InputTextBox.Text;
                    mw._main.NavigationService.Navigate(new LastNamePage());
                    //mw._main.NavigationService.Clo

                }
                else
                    Validate(sender, e);
            }
            else
            {
                if(MessageBox.Show("Ok ! Would you like to reset your entry then ?", "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Reset();
                }
            }
        }

    }
    public class FirstNamePageVm
    {
        public string FirstName { get; set; }   
        public FirstNamePageVm()
        {
            FirstName = "non";
        }

    }
}
