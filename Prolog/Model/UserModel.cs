using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class UserModel : INotifyPropertyChanged
    {
        public string IDUser { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public string TypeUser { get; set; }
        public string FirstNameUser { get; set; }
        public string LastNameUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
                ph(this, new PropertyChangedEventArgs(p));
        }
    }
}
