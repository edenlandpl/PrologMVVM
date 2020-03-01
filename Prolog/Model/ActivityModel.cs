using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class ActivityModel : INotifyPropertyChanged
    {
        public string IDActivity { get; set; }
        public string DataActivity { get; set; }
        public string NotesActivity { get; set; }
        public string LastNameTherapist { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
                ph(this, new PropertyChangedEventArgs(p));
        }
    }
}
