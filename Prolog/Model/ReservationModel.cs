using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Model
{
    public class ReservationModel : INotifyPropertyChanged
    {
        public string IDReservation { get; set; }
        public string DataVisitStart { get; set; }
        public string DataVisitEnd { get; set; } 
        public string RoomVisit { get; set; }
        public string AcceptedVisit { get; set; }
        public string IDPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameTherapist { get; set; }
        public string LastNameTherapist { get; set; }
        public string TimeStartVisit { get; set; }
        public string FirstNameUser { get; set; }
        public string LastNameUser { get; set; }
        public string textToReservationView { get; set; }
        public string TherapistName { get; set; }

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
