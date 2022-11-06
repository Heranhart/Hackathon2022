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
    public partial class TosWindow : Window
    {
        public bool IsActualWindow { get; set; }
        public TosWindow()
        {
            var vm = new TosVm();
            Random rnd = new Random();
            var r = rnd.Next(100);
            if (r > 50)
                vm.IsActualWindow = true;
            DataContext = vm;
            IsActualWindow = vm.IsActualWindow;
            InitializeComponent();
            if (IsActualWindow)
            {
                TextBoxFail.Visibility = Visibility.Hidden;
                var wins = Application.Current.Windows.Cast<Window>().Count(wi => wi.Title == "Terms of service");
                if(wins > 1)
                {
                    var s = TextBoxSuccess.Inlines.Last() as Run;
                    s.Text = s.Text.Remove(s.Text.Length - 1, 1) + " ";
                    TextBoxSuccess.Inlines.Add(new Run("(and the others !)."));
                }
            }
            else
            {
                TextBoxSuccess.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //foreach(Window w in Application.Current.Windows)
            //{
            //    if(w.Title =="SubWin")
            //        w.Close();
            //}
            var vmc = DataContext as TosVm;
            if (vmc.IsActualWindow)
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                MainWindowVM vm = (MainWindowVM)mw.DataContext;
                vm.UseAgreement = true;
                //if (Owner != null && Owner.Title == "SubWin")
                //{

                //    var vmo = this.Owner.DataContext as TosVm;
                //    if (vmo != null)
                //    {

                //        vmo.IsActualWindow = true;
                //    }
                //    Owner.Close();
                //}
            }
        }

        private void OpenNewTos(object sender, RoutedEventArgs e)
        {
            var oi = new TosWindow();
            oi.Owner = this;
            oi.ShowDialog();


        }

    }

    public class TosVm
    {
        public bool IsActualWindow { get; set; }
        public TosVm()
        {
            IsActualWindow = false;
        }
    }

}