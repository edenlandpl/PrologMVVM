using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class DiagnosisModel : INotifyPropertyChanged
    {
        public string IDDiagnosis { get; set; }
        public string DataDiagnosis { get; set; }
        public string NotesDiagnosis { get; set; }
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
