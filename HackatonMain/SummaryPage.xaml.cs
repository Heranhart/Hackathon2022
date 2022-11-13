using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;

namespace HackatonMain
{
    /// <summary>
    /// Logique d'interaction pour SummaryPage.xaml
    /// </summary>
    public partial class SummaryPage : Page
    {
        public int Check { get; set; }
        public bool CheckMissingPhone { get; set; }
        MainWindowVM ActualVm { get; set; }
        MainWindowVM FakeVm { get; set; }
        DispatcherTimer Timer { get; set; }
        public bool TriggeredMode { get; set; }

        public SummaryPage()
        {
            //ActualVm = Application.Current.MainWindow.DataContext as MainWindowVM;
            ActualVm = (MainWindowVM)Application.Current.MainWindow.DataContext;
            FakeVm = new MainWindowVM()
            {
                FirstName = ActualVm.LastName, //?? "LASTNAME",
                LastName = ActualVm.FirstName ,//?? "FIRSTNAME",
                Email = ActualVm.Email, //?? "Username@Domain.extension",
                DateOfBirth = ActualVm.DateOfBirth,
                PhoneNumber = "000000000"//actualVm.PhoneNumber,
            };
            var splits = Regex.Split(FakeVm.Email, "\\.|@");
            InitializeComponent();
            FakeVm.Email = splits[1] + "@" + splits[2] + "." + splits[0];
            Check = 0;
            DataContext = FakeVm; //Application.Current.MainWindow.DataContext;
            this.Timer = new DispatcherTimer();
            this.Timer.Interval = new TimeSpan(0, 0, 5);
            this.Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            MessageBox.Show(owner: Application.Current.MainWindow, "Oops ! Your phone number didn't load in for some reason. There it is !\nSorry about that.", "Oopsies");

            ((MainWindowVM)DataContext).PhoneNumber = ActualVm.PhoneNumber ?? "Dummy Number";
            PhoneNumber.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            CheckMissingPhone = true;
            Timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you confirm that you have double-checked all items of the forms, and that all entries are correct ?" + (Check < 2 ? "\nOtherwise please take some time to carefuly check it. You never know what kind of nonsense can happen..." : ""), "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Check >= 2)
                {
                    MainWindow mainWindow = App.Current.MainWindow as MainWindow;
                    mainWindow._main.Navigate(new PasswordPage());
                }
                else
                {
                    TriggeredMode = true;
                    string[] lines = new string[]
                    {
                        "Well too bad !! You failed !!\nYou either didn't read any of what these windows told you, or you actually couldn't find anything wrong while checking it ! Or both, for all I know !!",
                        "Or maybe you found that this form is \"Too Hard\" to use, and you gave up trying to enter anything that makes sense, and in the end you couldn't notice anything because you forgot what you had initialy entered ??",
                        "I mean come on ! You saw what this form is about at this point, right ??\nWhat made you think that you could just get here, click the big \"Confirm\" button without a care in the world, and that everything would work like a charm ?!",
                        "Were there not enough red flags ? Wasn't the missing phone number a big enough red flag ?\nIsn't this WHOLE PAGE a big enough RED flag ?!?!",
                        "I bet you didn't even read the Privacy Policies window that says \"Entering incorrect informations may lead to prosecution.\" ! Do you want to get a trial ? Because that's how you get a trial !!!",
                        "*Deep breaths...*",
                        "...I got a bit side-tracked, but I hope I got my point across.",
                        "Now please be a good little user, get a pair of working eyeballs, maybe clean up your screen a little, and Check That Form !!"
                    };
                    foreach (string line in lines)
                    {
                        MessageBox.Show(Application.Current.MainWindow, line, "Wrong !!");
                    }
                }
            }
        }

        private void Wrong(object sender, RoutedEventArgs e)
        {
            switch (Check)
            {
                case 0:
                    if(TriggeredMode)
                        MessageBox.Show("Oh... Look who decided to finally show up. This isn't what you entered ? You don't say...What could have possibly happened here ? Hmmmm....\n\nNow what did I mess up again...", "A bit late to the party, eh ?");
                    else
                        MessageBox.Show("Really ? Let me check...", "Were mistakes made ..?");
                    Thread.Sleep(3000);
                    ((MainWindowVM)DataContext).LastName = ActualVm.LastName;
                    LastName.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                    ((MainWindowVM)DataContext).FirstName = ActualVm.FirstName;
                    FirstName.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                    MessageBox.Show("Oh ! Found it ! It looks like your first and last names got switched somehow. Please check again !", "Oops !");
                    Check++;
                    break;
                case 1:
                    if(TriggeredMode)
                        MessageBox.Show("Another one, you say ? \nOh no... How could that possibly happen.. \nOh the humanity...", "A bit late to check, don't you think ?");
                    else
                        MessageBox.Show("Another one ? Hold on...", "Were mistakes made ..?");
                    Thread.Sleep(5000);

                    ((MainWindowVM)DataContext).Email = ActualVm.Email;
                    Email.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                    MessageBox.Show("Well I'll be ! Your e-mail adress got juggled up for some reason. Please check it is now correct !", "Oops !");
                    Check++;
                    break;
                case 2:
                    if(TriggeredMode)
                        MessageBox.Show("Hahaha, look at you, frantically clicking this link. Are you even checking the form at all every now and then ?", "Over-clicked");
                    else
                        MessageBox.Show("Seriously ?! Come on !!\nSorry. So, what is it this time...", "Were mistakes made ..?");
                    Thread.Sleep(7000);
                    MessageBox.Show("Uhh... Well this is awkward. I couldn't find any other error in these 5 lines of data...Perhaps you might want to check again..?", "Error 404 : Error Not Found");
                    Check++;
                    TriggeredMode = false;
                    break;
                default:
                    MessageBox.Show("No, really,I am not finding any other error in there.\nIf you think something is still wrong, you may screwed something up so bad even I can't do anything about it.\nIn this case, you just might have to start over from the beginning...", "Error 404 : Error Not Found. Wait...");
                    break;
            }
        }
    }
}
