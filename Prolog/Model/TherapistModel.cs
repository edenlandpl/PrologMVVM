using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class TherapistModel : INotifyPropertyChanged
    {
        public string IDTherapist { get; set; }
        public string FirstNameTherapist { get; set; }
        public string LastNameTherapist { get; set; }
        public string EmailTherapist { get; set; }
        public string PhoneTherapist { get; set; }
        public string NoteTherapist { get; set; }

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
