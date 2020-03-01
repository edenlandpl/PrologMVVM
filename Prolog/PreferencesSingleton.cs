using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog
{
    public class PreferencesSingleton
    {
        public static string UserLoginName { get; set; }
        public void setUserLoginName(string userLoginName)
        {
            UserLoginName = userLoginName;
            Console.WriteLine("Nazwa ustawiona - " + UserLoginName);
        }
        public string getUserLoginName()
        {
            Console.WriteLine("Nazwa pobrana - " + UserLoginName);
            return UserLoginName;
        }
    }
}
