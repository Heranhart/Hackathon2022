using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HackatonMain
{
    public class MainWindowVM
    {

        public List<string> LanguageList { get; set; }
        public string SelectedLanguage { get; set; }

        public bool PolAgreement { get; set; }
        public bool UseAgreement { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime StartTime { get; set; }
        public MainWindowVM()
        {
            this.LanguageList = new List<string>() { "English" };
            StartTime = DateTime.Now;
            //for(;LanguageList.Count < 100; LanguageList.Add("English")) { }
            
        }
    }
}
