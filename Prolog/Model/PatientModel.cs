using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    public class PatientModel : INotifyPropertyChanged
    {
        public string IDPatient { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
        public string Street { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string IDTherapist { get; set; }
        public string NameTherapist { get; set; }
        public string Phone { get; set; }
        public string  Email { get; set; }

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
