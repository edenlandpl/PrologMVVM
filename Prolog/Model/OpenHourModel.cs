using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    class OpenHourModel : INotifyPropertyChanged
    {
        public string OpenOfficeHour { get; set; }
        public string OpenOfficeMinutes { get; set; }
        public string LastNamePatientReservation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
