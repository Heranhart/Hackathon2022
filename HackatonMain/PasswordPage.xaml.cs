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
    /// Logique d'interaction pour PasswordPage.xaml
    /// </summary>
    public partial class PasswordPage : Page
    {
        public string Password { get; set; }
        public bool PasswordOk { get; set; }
        public int CharsMin { get; set; }
        public int CharsMax { get; set; }
        public int NumberCount { get; set; }
        public string RequiredChar { get; set; }

        public string RegexToMatch { get; set; }
        public BitmapImage CheckedBitmap { get; set; }
        public BitmapImage UncheckedBitmap { get; set; }

        public PasswordPage()
        {
            CheckedBitmap = new BitmapImage();
            CheckedBitmap.BeginInit();
            CheckedBitmap.UriSource = new Uri("Assets/cm.png", UriKind.Relative); // new Uri("\\Assets\\rickroll.gif");
            CheckedBitmap.EndInit();
            UncheckedBitmap = new BitmapImage();
            UncheckedBitmap.BeginInit();
            UncheckedBitmap.UriSource = new Uri("Assets/ucm.png", UriKind.Relative); // new Uri("\\Assets\\rickroll.gif");
            UncheckedBitmap.EndInit();

            Random rng = new Random();
            CharsMin = rng.Next(10, 20);
            CharsMax = rng.Next(25, 155);
            string[] Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._".ToCharArray().Select(_ => _.ToString()).ToArray();
            RequiredChar = Chars[rng.Next(0, Chars.Length - 1)];
            NumberCount = rng.Next(3, 6);
            RegexToMatch = GenerateRegex();
            var Numbers = "0123456789".ToCharArray().Select(_ => _.ToString()).ToArray();

            TextBlock Text1 = new TextBlock() { Text = string.Format("Contain at least {0} characters", CharsMin), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(Text1, 0);
            TextBlock Text2 = new TextBlock() { Text = string.Format("Contain at most {0} characters", CharsMax), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(Text2, 1);
            TextBlock Text3 = new TextBlock() { Text = "Contain at least 1 special character", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(Text3, 2);
            TextBlock Text4 = new TextBlock() { Text = string.Format("Contain exactly {0} numbers", NumberCount), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(Text4, 3);
            TextBlock Text5 = new TextBlock() { Text = string.Format("Contain exactly 1 \"{0}\"", RequiredChar), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(Text5, 4);
            TextBlock Text6 = new TextBlock() { Name = "RegexText", Text = string.Format("Match the following Regex at least once : \"{0}\"", RegexToMatch), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(Text6, 5);

            InitializeComponent();
            img1.Source = UncheckedBitmap;
            img2.Source = UncheckedBitmap;
            img3.Source = UncheckedBitmap;
            img4.Source = UncheckedBitmap;
            img5.Source = UncheckedBitmap;
            img6.Source = UncheckedBitmap;
            RequirementsGrid.Children.Add(Text1);
            RequirementsGrid.Children.Add(Text2);
            RequirementsGrid.Children.Add(Text3);
            RequirementsGrid.Children.Add(Text4);
            RequirementsGrid.Children.Add(Text5);
            RequirementsGrid.Children.Add(Text6);
            DataContext = this;
            Password = string.Empty;
            CheckPassword();
        }

        private string GenerateRegex()
        {
            bool hasQuantifier = false;
            string result = "";

            string[] lowerChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray().Select(_ => _.ToString()).ToArray();
            string[] upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Select(_ => _.ToString()).ToArray();
            string[] allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray().Select(_ => _.ToString()).ToArray();

            Random rng = new Random();
            string str1 = "["; // [a-zA-Z]

            int min = rng.Next(0, 10);
            str1 += lowerChars[min] + "-"; //[a-
            int max = rng.Next(min + 2, 25);
            str1 += lowerChars[max]; // [a-z

            min = rng.Next(5, 15);
            str1 += upperChars[min] + "-"; //[a-zA-
            max = rng.Next(min + 6, 25);
            str1 += upperChars[max] + "]"; // [a-zA-Z]

            if (rng.Next(0, 10) > 2)
            {
                str1 += "{" + rng.Next(1, 5) + "}"; // [a-zA-Z]{5}
                hasQuantifier = true;
            }

            string str2 = "";
            string str4 = "";
            int count2 = rng.Next(2, 4);
            for (int i = 0; i < count2; i++)
            {
                str2 += allChars[rng.Next(0, allChars.Length)]; // abcd
            }

            string str3 = "";

            if (rng.Next(0, 10) > 5)
            {
                str3 = "(" + str1 + "|" + str2 + ")"; // ([a-zA-Z]|abcd)
            }
            else
            {
                str3 = "(" + str2 + "|" + str1 + ")"; // (abcd|[a-zA-Z])
            }

            if (/*!hasQuantifier &&*/ rng.Next(0, 10) > 4)
            {
                str3 += "{" + rng.Next(1, 5) + "}";// string.Format("{{0}}", rng.Next(1, 5));
                hasQuantifier = true;
            }

            int count4 = rng.Next(1, 4);
            for (int i = 0; i < count4; i++)
            {
                str4 += allChars[rng.Next(0, allChars.Length)]; // abcd
            }

            if (rng.Next(0, 10) > 5)
            {
                result = str3 + str4; // ([a-zA-Z]|abcd)abcd
            }
            else
            {
                result = str4 + str3; // abcd([a-zA-Z]|abcd)
            }

            return result;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
            CheckPassword();
        }
        private void CheckPassword()
        {
            // Test longeur 1
            img1.Source = Password.Length >= CharsMin ? CheckedBitmap : UncheckedBitmap;
            PasswordOk = Password.Length >= CharsMin;
            // Test longeur 2
            img2.Source = Password.Length <= CharsMax ? CheckedBitmap : UncheckedBitmap;
            PasswordOk &= Password.Length <= CharsMax;
            // Test au moins 1 car. spe.
            Regex regexSpeChar = new Regex("[a-zA-Z0-9]");
            int countNonSpeChar = regexSpeChar.Matches(Password).Count;

            img3.Source = Password.Length != countNonSpeChar
                ? CheckedBitmap : UncheckedBitmap;
            PasswordOk &= Password.Length != countNonSpeChar;
            // Test exactement x nombres
            Regex regexNumbers = new Regex("[0-9]");
            int countNumbers = regexNumbers.Matches(Password).Count;

            img4.Source = countNumbers == NumberCount ? CheckedBitmap : UncheckedBitmap;
            PasswordOk &= countNumbers == NumberCount;
            // Test exactement 1 specifique car.
            Regex regexReqChar = new Regex(RequiredChar);
            int countReqChar = regexReqChar.Matches(Password).Count;

            img5.Source = countReqChar == 1 ? CheckedBitmap : UncheckedBitmap;
            PasswordOk &= countReqChar == 1;
            // Test Regex match
            Regex regexMatch = new Regex(RegexToMatch);
            int countRegexMatch = regexMatch.Matches(Password).Count;


            img6.Source = countRegexMatch > 0 ? CheckedBitmap : UncheckedBitmap;
            PasswordOk &= countRegexMatch > 0;
        }

        private void ClickNewRegex(object sender, RoutedEventArgs e)
        {
            RegexToMatch = GenerateRegex();

            var text = RequirementsGrid.Children.Cast<UIElement>().First(i => Grid.GetRow(i) == 5 && Grid.GetColumn(i) == 0);
            ((TextBlock)text).Text = string.Format("Match the following Regex at least once : \"{0}\"", RegexToMatch);
            CheckPassword();
        }

        private void Validation(object sender, RoutedEventArgs e)
        {
            // TODO Checks....
            if (PasswordOk)
            {

                ConfirmPasswordWindow confirmation = new ConfirmPasswordWindow(Password);
                bool validated = confirmation.ShowDialog() ?? false;
                if (validated)
                {
                    ((MainWindow)Application.Current.MainWindow)._main.Navigate(new EndPage());
                }
                else
                {
                    MessageBox.Show("The password confirmation process failed. The confirmation password did not match the previously entered password.\nKeep in mind the confirmation password must be reversed !");
                }
            }
            else
            {
                MessageBox.Show("Your password doesn't match all the requirements. Make sure all the criteria are checked, then press the \"Confirm\" button.");
            }

        }

        private void ShowPassword(object sender, MouseEventArgs e)
        {
            TextBlockPw.Visibility = Visibility.Visible;
            TextBlockPw.Text = Password;
            PwBox.Visibility = Visibility.Hidden;
        }

        private void HidePassword(object sender, MouseEventArgs e)
        {
            TextBlockPw.Visibility = Visibility.Hidden;
            PwBox.Visibility = Visibility.Visible;
        }

        private void AlsoHidePassword(object sender, MouseButtonEventArgs e)
        {
            TextBlockPw.Visibility = Visibility.Hidden;
            PwBox.Visibility = Visibility.Visible;
        }

        private void PwBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
        e.Command == ApplicationCommands.Cut ||
        e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
