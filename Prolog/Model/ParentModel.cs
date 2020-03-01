using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class ParentModel : INotifyPropertyChanged
    {
        public string IDParent { get; set; }
        public string FirstNameParent { get; set; }
        public string LastNameParent { get; set; }
        public string TelephoneParent { get; set; }
        public string EmailParent { get; set; }
        public string StreetParent { get; set; }
        public string ZIPParent { get; set; }
        public string CityParent { get; set; }
        public string IDPatient { get; set; }
        public string FirstNamePatient { get; set; }
        public string LastNamePatient { get; set; }

        public event PropertyChangedEventHandler UpdateValue;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
                ph(this, new PropertyChangedEventArgs(p));
        }
    }
}
