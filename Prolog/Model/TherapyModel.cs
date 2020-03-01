using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class TherapyModel : INotifyPropertyChanged
    {
        public string IDTherapy { get; set; }
        public string DataTherapy { get; set; }
        public string NotesTherapy { get; set; }
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
