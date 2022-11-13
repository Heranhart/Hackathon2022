using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour AddCharWindow.xaml
    /// </summary>
    public partial class AddCharWindow : Window
    {
        //public override AddCharVm DialogResults {get;set;}
        //public EmailPageVm EmailPageVm { get => (EmailPageVm)(((MainWindow)Application.Current.MainWindow)._main.DataContext); }
        //public AddCharVm Vm { get => DataContext as AddCharVm; }
        public AddCharWindow()
        {
            InitializeComponent();

            Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._0123456789".ToCharArray().Select(_ => _.ToString()).ToArray();
            int n = Chars.Length;
            Random rng = new Random();
            while (n > 1)
            {
                int k = rng.Next(n--);
                string temp = Chars[n];
                Chars[n] = Chars[k];
                Chars[k] = temp;
            }

            Where = new string[] { "Username", "Domain", "Extension" };
            DataContext = this;// new AddCharVm();
        }

        private void AddIt(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CharChoice))
            {
                MessageBox.Show("Please select a character to add !", "Data not found");
                return;
            }
            if (string.IsNullOrEmpty(WhereChoice))
            {
                MessageBox.Show("Please choose where to insert the selected character !", "Data not found");
                return;
            }
            switch (WhereChoice)
            {
                case "Username":
                    break;
                case "Domain":
                    if (CharChoice == ".")
                    {
                        MessageBox.Show("You cannot add a \".\" to the domain.", "Incorrect mail building");
                        return;
                    }
                    break;
                case "Extension":
                    if (!Regex.IsMatch(CharChoice, "[a-zA-Z]") /*new[] { ".", "-", "_" }.ToList().Contains(CharChoice)*/)
                    {
                        MessageBox.Show("You cannot add special characters or numbers to the extension.", "Incorrect mail building");
                        return;
                    }
                    break;
            }
            this.DialogResult = true;
        }

        public string[] Chars { get; set; }
        public string CharChoice { get; set; }

        public string[] Where { get; set; }
        public string WhereChoice { get; set; }
    }
    //public class AddCharVm
    //{
    //    public char[] Chars { get; set; }
    //    public char CharChoice { get; set; }

    //    public string[] Where { get; set; }
    //    public string WhereChoice { get; set; }


    //    public AddCharVm()
    //    {
    //        Chars = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ-".ToCharArray();
    //        Where = new string[] { "Username","Domain","Extension"};
    //    }
    //}

}
