using Prolog.Command;
using Prolog.Converters;
using Prolog.Model;
using Prolog.SQLConnection;
using Prolog.ViewModel.SubView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Prolog.ViewModel
{
    class ReservationsViewModel : INotifyPropertyChanged
    {                
        public AddReservationCommand addReservationCommand { get; set; }
        public UpdateReservationCommand updateReservationCommand { get; set; }
        public DeleteReservationCommand deleteReservationCommand { get; set; }

        public ChangeDayTherapistReservation changeDayTherapistReservation { get; set; }

    Dictionary<string, string> patientsListToReservation = new Dictionary<string, string>();

        public ObservableCollection<string> therapistsListToReservation;
        public ObservableCollection<string> TherapistsListToReservation
        {
            get { return therapistsListToReservation; }
            set
            {
                therapistsListToReservation = value;
                OnPropertyChanged("TherapistsListToReservation");
            }
        }

        private ObservableCollection<ReservationModel> reservationsList;
        public ObservableCollection<ReservationModel> ReservationsList
        {
            get { return reservationsList; }
            set
            {
                reservationsList = value;
                OnPropertyChanged("ReservationsList");
            }
        }

        private ObservableCollection<OpenHourModel> openOfficeHoursList;

        public ObservableCollection<OpenHourModel> OpenOfficeHoursList
        {
            get { return openOfficeHoursList; }
            set
            {
                openOfficeHoursList = value;
                OnPropertyChanged("OpenOfficeHoursList");
            }
        }
        public virtual ObservableCollection<DateTime> DatesReservations { get; set; }

        private ReservationToColorConverter setCounterColorsReservation = new ReservationToColorConverter();

        string[] data = new string[399]; 
        string[] dataRes = new string[399];
        private string colorBackgroundTable = "Green";
        public string choosenIDTherapist = "4";
        public string ColorBackgroundTable
        {
            get { return colorBackgroundTable; }
            set
            {
                colorBackgroundTable = value;
                OnPropertyChanged("ColorBackgroundTable");
            }
        }

        private bool isSelected = true;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                //selectedFirstName = SelectedRow.FirstName;
                OnPropertyChanged("IsSelected");
            }
        }
        
         private ReservationModel selectedRow ;
        public ReservationModel SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                //Console.WriteLine("SelectedRow - " + SelectedRow.LastName + SelectedRow.IDReservation);
                //lastNameReservation = SelectedRow.LastName;
                //OnPropertyChanged("LastNameReservation");
                OnPropertyChanged("SelectedRow");
            }
        }
        private string lastNameReservation;
        public string LastNameReservation
        {
            get
            {
                //Console.WriteLine("Jestem w SelectedFirstname " + SelectedRow.IDPatient);
                return lastNameReservation;
            }
            set
            {
                lastNameReservation = value;
                OnPropertyChanged("LastNameReservation");
            }
        }
        // data ustawiana w pickerze, wskazuje na wyświetlany dzień w rezerwacjach
        private string dateForReservationList = "2020-01-22";
        public string DateForReservationList
        {
            get
            {
                return dateForReservationList;
            }
            set
            {
                dateForReservationList = value;
                OnPropertyChanged("DateForReservationList");
            }
        }

        private string selectedTherapistsListToReservation = "01";
        public string SelectedTherapistsListToReservation
        {
            get
            {
                return selectedTherapistsListToReservation;
            }
            set
            {
                selectedTherapistsListToReservation = value;
                Console.WriteLine("Wybraniec lodu - " + selectedTherapistsListToReservation);
                OnPropertyChanged("SelectedTherapistsListToReservation");
            }
        }

        public ReservationsViewModel()
        {
            addReservationCommand = new AddReservationCommand(this);
            updateReservationCommand = new UpdateReservationCommand(this);
            deleteReservationCommand = new DeleteReservationCommand(this);
            changeDayTherapistReservation = new ChangeDayTherapistReservation(this);
            
            DateTime currentTime = DateTime.Now;
            DateTime dataVisitStart = new DateTime(2019, 12, 23, 08, 30, 00);
            DateTime dataVisitEnd = new DateTime(2019, 12, 23, 09, 30, 00);
            DatesReservations = new ObservableCollection<DateTime> { DateTime.Today };
            therapistsListToReservation = new ObservableCollection<string>
            {
            };
            readPatients();
            readTherapists();
            reservationsList = new ObservableCollection<ReservationModel>
            {               
            };
            //DataGrid_Loaded();
            DataGridReservation_Loaded();
        }

        public void DataGrid_Loaded()
        {
            int j = 0;
            int temp = 0;
            DateTime currentTime = DateTime.Now;

            int startOpenOfficeHour = 8;
            int endOpenOfficeHour = 16;
            int startOpenOfficeMinutes = 30;


            for (j = startOpenOfficeHour; j <= endOpenOfficeHour; j++) // Looping throw colums
            {
                temp = 100 + j;
                data[j] = temp.ToString();
                if (j==14)
                {
                    setCounterColorsReservation.setCounterColors();
                }
            }

            DBClass.openConnection();

            DBClass.sql = "select distinct IDReservation, DataVisitStart, DataVisitEnd, RoomVisit, AcceptedVisit, FirstName, LastName, LastNameTherapist, TimeStartVisit " +
                "from reservations, patients, therapists " +
                "where therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '1') " +
                "and patients.FirstName = (select FirstName from patients where IDPatient = reservations.IDPatient) " +
                "and patients.LastName = (select LastName from patients where IDPatient = reservations.IDPatient) " +
                "and  convert(Date, DataVisitStart) = '2020-01-22' and TimeStartVisit = '08:00:00' " +
                "ORDER BY DataVisitStart Asc";
            // pełna wersja
            //"select distinct IDReservation, DataVisitStart, DataVisitEnd, RoomVisit, AcceptedVisit, FirstName, LastName, LastNameTherapist " +
            //    "from reservations, patients, therapists " +
            //    "where(reservations.IDPatient = '1') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '1') " +
            //    "and patients.FirstName = (select FirstName from patients where IDPatient = '1') " +
            //    "and patients.LastName = (select LastName from patients where IDPatient = '1')";
            // wybierze tylko z daty zadanej 
            //select* from reservations where convert(Date, DataVisitStart) = '2020-01-22'and convert(time, DataVisitStart) = '10:00:00' ORDER BY DataVisitStart Asc
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        dataRes[j] = reader.GetValue(j).ToString();
                        Console.WriteLine("REzerW - " + dataRes[j]);
                    }
                    ReservationsList.Add(new ReservationModel
                    {
                        IDReservation = dataRes[0],
                        DataVisitStart = dataRes[1].Substring(0, 8),
                        DataVisitEnd = dataRes[2],
                        RoomVisit = dataRes[3],
                        AcceptedVisit = dataRes[4],
                        FirstName = dataRes[5],
                        LastName = dataRes[6],
                        LastNameTherapist = dataRes[7],
                        TimeStartVisit = dataRes[8]
                        //Console.WriteLine(" Lista id : " + dataRes[0] + dataRes[6]);
                    });;
                }
            }
            DBClass.closeConnection();
            //foreach (var item in ReservationsList.Skip(1).Take(3))
            //{
            //    Console.WriteLine(" Lista rezerwacji : " + item.LastName); 
            //}
        }
        public void DataGridReservation_Loaded()
        {
            ReservationsList.Clear();
            //Console.WriteLine("Długość listy - " + ReservationsList.Count()); 
            openOfficeHoursList = new ObservableCollection<OpenHourModel> { };
            TimeSpan tempInterval = new TimeSpan(0, 0, 0);
            string tempString = "";
            int startOpenOfficeHour = 8;
            int endOpenOfficeHour = 16;
            TimeSpan startOpenOfficeTimeSpan = new TimeSpan(startOpenOfficeHour, 0, 0);
            TimeSpan intervalMinutes = new TimeSpan(0, 30, 0);
            int intervalMinutesInt = (int)intervalMinutes.TotalMinutes;
            // poprzez zmianę wartości i można modulować godziny otwarcia gabinetu, poprzez zmienne programowe
            endOpenOfficeHour *= (60 / intervalMinutesInt) / 2;
            for (int i = startOpenOfficeHour; i < (endOpenOfficeHour); i++)
            {
                // ustawione na interwał 1 godzinny
                openOfficeHoursList.Add(new OpenHourModel { OpenOfficeHour = startOpenOfficeTimeSpan.ToString(@"hh\:mm") });
                GetReservationWhenTime(startOpenOfficeTimeSpan.ToString(@"hh\:mm"));
                //startOpenOfficeTimeSpan += intervalMinutes;
                openOfficeHoursList.Add(new OpenHourModel { OpenOfficeHour = ((startOpenOfficeTimeSpan + intervalMinutes).ToString(@"hh\:mm")) });
                GetReservationWhenTime(startOpenOfficeTimeSpan.ToString(@"hh\:mm"));
                startOpenOfficeTimeSpan += intervalMinutes + intervalMinutes;
                //Console.WriteLine("czasówka - " + startOpenOfficeTimeSpan);
                //Console.WriteLine("REzerW - " + dataRes[5]);
            }
            //DataGrid_Loaded();
        }
        public void GetReservationWhenTime(string timeReservation)
        {
            int j = 0;
            int temp = 0; 
            int startOpenOfficeHour = 8;
            int endOpenOfficeHour = 16;
            int startOpenOfficeMinutes = 30;
            string tempNamePatient = "";
            choosenIDTherapist = SelectedTherapistsListToReservation.Substring(0, 2);
            DateTime currentTime = DateTime.Now;            
            for (j = startOpenOfficeHour; j <= endOpenOfficeHour; j++) // Looping throw colums
            {
                temp = 100 + j;
                data[j] = temp.ToString();
                if (j == 14)
                {
                    setCounterColorsReservation.setCounterColors();
                }
            }
            DBClass.openConnection();

            DBClass.sql = "select distinct IDReservation, DataVisitStart, DataVisitEnd, RoomVisit, AcceptedVisit, IDPatientReservation, firstNameUser, lastNameUser, FirstNameTherapist, LastNameTherapist, reservations.IDTherapist, CONVERT(varchar(5), TimeStartVisit) " +
                "from reservations, patients, therapists, users " +
                "where therapists.LastNameTherapist = (select distinct LastNameTherapist from therapists where IDTherapist = '" + choosenIDTherapist + "') " +
                "and reservations.IDTherapist = '" + choosenIDTherapist + "' " +
                "and users.firstNameUser = (select firstNameUser from users where IDUsers = reservations.IDUser) " +
                "and users.lastNameUser = (select lastNameUser from users where IDUsers = reservations.IDUser) " +
                "and  convert(Date, DataVisitStart) = '" + dateForReservationList + "' and TimeStartVisit = '" + timeReservation + "' " +
                "ORDER BY DataVisitStart Asc";
            // wybierze tylko z daty zadanej 
            //select* from reservations where convert(Date, DataVisitStart) = '2020-01-22'and convert(time, DataVisitStart) = '10:00:00' ORDER BY DataVisitStart Asc
            //"where therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '" + choosenIDTherapist + "') " +
            //"where  reservation.IDTherapist = '" + choosenIDTherapist + "' " +
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        dataRes[j] = reader.GetValue(j).ToString();
                    }
                    Console.WriteLine("User05 - " + dataRes[5]);
                    // ustawka na puste pola
                    if (dataRes[5].Length > 0)
                    {
                        tempNamePatient = patientsListToReservation[dataRes[5]];
                    }
                    
                    ReservationsList.Add(new ReservationModel
                    {
                        IDReservation = dataRes[0],
                        DataVisitStart = dataRes[1].Substring(0, 10),
                        DataVisitEnd = dataRes[2],
                        RoomVisit = dataRes[3],
                        AcceptedVisit = dataRes[4],
                        IDPatient = dataRes[5],
                        FirstNameUser = dataRes[6],
                        LastNameUser = dataRes[7],
                        FirstNameTherapist = dataRes[8],
                        LastNameTherapist = dataRes[9],
                        TimeStartVisit = dataRes[10],
                        textToReservationView = dataRes[6] + " " + dataRes[7] + "  - [ " + tempNamePatient + " ]"
                    }); 
                    DBClass.closeConnection();
                    return;
                }
                ReservationsList.Add(new ReservationModel
                {
                    IDReservation = "",
                    DataVisitStart = dateForReservationList,
                    DataVisitEnd = "",
                    RoomVisit = "",
                    AcceptedVisit = "",
                    IDPatient = "",
                    FirstNameUser = "",
                    LastNameUser = "",
                    FirstNameTherapist = "",
                    LastNameTherapist = "",
                    TimeStartVisit = timeReservation,
                    textToReservationView = "Wolny termin"
                }); ;
                DBClass.closeConnection();
                //return;
            }       
        }
        public void SetDateForListReservation(string chooseDate)
        {
            dateForReservationList = chooseDate;
            Console.WriteLine("Data w VM - " + dateForReservationList);
            ReservationsList.Clear();
            DataGridReservation_Loaded();
            AddReservation();
        }

        public void readPatients()
        {
            string firstLastNamePatient = "";
            DBClass.openConnection();
            //) CAST(DateOfBirth AS date), convert(Date, DateOfBirth, 23) 
            DBClass.sql = "select IDPatient, FirstName, LastName from patients";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            //Console.WriteLine("Przy bazie" + data[3]);
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    if (data[1] == null )
                    {
                        data[1] = "";
                    }
                    if (data[2] == null)
                    {
                        data[2] = "";
                    }
                    firstLastNamePatient = data[1] + " " + data[2];
                    patientsListToReservation.Add(data[0], firstLastNamePatient);
                }
            }
            DBClass.closeConnection();
        }

        public void readTherapists()
            {
                string firstLastNameTherapist = "";
                DBClass.openConnection();
                //) CAST(DateOfBirth AS date), convert(Date, DateOfBirth, 23) 
                DBClass.sql = "select IDTherapist, FirstNameTherapist, LastNameTherapist from therapists";
                DBClass.cmd.CommandType = CommandType.Text;
                DBClass.cmd.CommandText = DBClass.sql;

                DBClass.da = new SqlDataAdapter(DBClass.cmd);
                DBClass.dt = new DataTable();
                DBClass.da.Fill(DBClass.dt);

                // wyciągamy dane
                int i = 0;
                int j = 0;
                //Console.WriteLine("Przy bazie" + data[3]);
                using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                        {
                            data[j] = reader.GetValue(j).ToString();
                        }
                        if (data[1] == null)
                        {
                            data[1] = "";
                        }
                        if (data[2] == null)
                        {
                            data[2] = "";
                        }
                        
                        firstLastNameTherapist = data[0] + " - " + data[1] + " " + data[2];
                    if (data[0] == "0")
                    {
                        firstLastNameTherapist = "";
                    }
                    if (firstLastNameTherapist != null)
                        {
                            therapistsListToReservation.Add(firstLastNameTherapist);
                        }
                        
                    }
                }
                DBClass.closeConnection();
        }
        public void AddReservation()
        {
            //Console.WriteLine("W dowaniau - " + SelectedRow.LastName);
            //Console.WriteLine("Dodajemy ... reservację");
            DataGridReservation_Loaded();
        }

        public void UpdateReservation()
        {
            Console.WriteLine("Updatujemy rezerwację");
        }

        public void DeleteReservation()
        {
            Console.WriteLine(" Usuwamy rezerwację");
        }

        public void ChangeDayTherapistReservation()
        {
            DataGridReservation_Loaded();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
