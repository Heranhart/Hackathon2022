using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonMain
{
    public class MainWindowVM
    {

        public List<string> LanguageList { get; set; }
        public string SelectedLanguage { get; set; }

        public bool PolAgreement { get; set; }
        public bool UseAgreement { get; set; }
        public MainWindowVM()
        {
            this.LanguageList = new List<string>() { "English" };
            //for(;LanguageList.Count < 100; LanguageList.Add("English")) { }
            
        }
    }
}
