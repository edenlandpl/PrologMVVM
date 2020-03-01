using Prolog.Command;
using Prolog.SQLConnection;
using Prolog.View;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;

namespace Prolog.ViewModel.SubViewModel
{
    class UpdateTherapistSubViewModel : INotifyPropertyChanged
    {
        private TherapistsViewModel _therapistsViewModel;        //Deklarujemy zmienną typu TherapistsView
        TherapistsViewModel getter = new TherapistsViewModel();        //Deklarujemy zmienną typu TherapistsView
        private TherapistsView _therapistsView;

        public string FirstNameForm { get; private set; }
        public static string[] selectedTherapist = new string[9];
        public static string[] selectedTherapistAfterChange = new string[9];

        string temp = "";

        #region Properties

        private string _selectedIDTherapist;
        private string _selectedFirstName;
        private string _selectedLastName;
        private string _selectedEmailTherapist;
        private string _selectedPhoneTherapist;
        private string _selectedNoteTherapist;

        public SaveCommand saveCommand { get; set; }
        public string SelectedIDTherapist
        {
            get { return _selectedIDTherapist; }
            set
            {
                if (value != _selectedIDTherapist)
                {
                    _selectedIDTherapist = value;
                    OnPropertyChanged("SelectedIDTherapist");

                    selectedTherapistAfterChange[0] = SelectedIDTherapist;
                }
            }
        }
        public string SelectedFirstName
        {
            get { return _selectedFirstName; }
            set
            {
                if (value != _selectedFirstName)
                {
                    _selectedFirstName = value;
                    OnPropertyChanged("SelectedFirstName");
                    selectedTherapistAfterChange[1] = SelectedFirstName;
                    //Console.WriteLine("Jestem w Selekcie U " + _selectedIDTherapist + SelectedFirstName + SelectedLastName + selectedTherapistAfterChange[1]);
                }
            }
        }
        public string SelectedLastName
        {
            get { return _selectedLastName; }
            set
            {
                if (value != _selectedLastName)
                {
                    _selectedLastName = value;
                    OnPropertyChanged("SelectedLastName");
                    selectedTherapistAfterChange[2] = SelectedLastName;
                }
            }
        }
        public string SelectedEmailTherapist
        {
            get { return _selectedEmailTherapist; }
            set
            {
                if (value != _selectedEmailTherapist)
                {
                    _selectedEmailTherapist = value;
                    OnPropertyChanged("SelectedEmailTherapist");
                    selectedTherapistAfterChange[3] = SelectedEmailTherapist;
                }
            }
        }
        public string SelectedPhoneTherapist
        {
            get { return _selectedPhoneTherapist; }
            set
            {
                if (value != _selectedPhoneTherapist)
                {
                    _selectedPhoneTherapist = value;
                    OnPropertyChanged("SelectedPhoneTherapist");
                    selectedTherapistAfterChange[4] = SelectedPhoneTherapist;
                }
            }
        }
        public string SelectedNoteTherapist
        {
            get { return _selectedNoteTherapist; }
            set
            {
                if (value != _selectedNoteTherapist)
                {
                    _selectedNoteTherapist = value;
                    OnPropertyChanged("SelectedNoteTherapist");
                    selectedTherapistAfterChange[5] = SelectedNoteTherapist;
                }
            }
        }
        #endregion


        public UpdateTherapistSubViewModel()
        {

            //saveCommand = new SaveCommand(this);
            //Console.WriteLine("Getterrek - " + SelectedFirstName);
            selectedTherapist = new string[9];
            // wyciągnięcie danych z tabeli - therapists
            selectedTherapist = getter.InfoTherapistGetter();
            SelectedIDTherapist = selectedTherapist[0];
            SelectedFirstName = selectedTherapist[1];
            SelectedLastName = selectedTherapist[2];
            SelectedEmailTherapist = selectedTherapist[3];
            SelectedPhoneTherapist = selectedTherapist[4];
            SelectedNoteTherapist = selectedTherapist[5];
            Console.WriteLine("W Updacie - " + SelectedFirstName);
        }

        public UpdateTherapistSubViewModel(TherapistsViewModel therapistsViewModel)
        {
            _therapistsViewModel = therapistsViewModel;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
            //this.DataContext = this;                    // Set DataContext of MainWindow to itself in constructor of MainWindow to resolve binding, bez tego nie będzie BINDOWAĆ !!
        }

        public UpdateTherapistSubViewModel(TherapistsView therapistsView)       //W konstruktorze umieszczamy TherapistsView jako parametr
        {
            //InitializeComponent();
            _therapistsView = therapistsView;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
        }

        public string EditPatienceSubViewSave(string _firstName)
        {
            FirstNameForm = _firstName;
            return FirstNameForm;
        }

        internal void infoPatience(string _IDTherapistGetter, string _firstNameTherapistGetter, string _lastNameTherapistGetter)
        {
            temp = _IDTherapistGetter;
            _selectedFirstName = _firstNameTherapistGetter;
            _selectedLastName = _lastNameTherapistGetter;
            Console.WriteLine("First name P01-" + _selectedIDTherapist + SelectedFirstName + SelectedLastName);
        }

        public void SaveUpdateTherapist()
        {
            SelectedIDTherapist = selectedTherapistAfterChange[0];
            SelectedFirstName = selectedTherapistAfterChange[1];
            SelectedLastName = selectedTherapistAfterChange[2];
            SelectedEmailTherapist = selectedTherapistAfterChange[3];
            SelectedPhoneTherapist = selectedTherapistAfterChange[4];
            SelectedNoteTherapist = selectedTherapistAfterChange[5];
            getter.DataGrid_Loaded(SelectedIDTherapist, SelectedFirstName, SelectedLastName);
            // konwersja daty z string
            //var dataToSQL = DateTime.ParseExact(SelectedDateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //Console.WriteLine("DataSzmata - " + SelectedDateOfBirth);

            DBClass.openConnection();

            DBClass.sql = "Update therapists Set FirstNameTherapist = '" + SelectedFirstName + "', LastNameTherapist = '" + SelectedLastName + "', EmailTherapist = '" + SelectedEmailTherapist + "'," +
                " PhoneTherapist = '" + SelectedPhoneTherapist + "', NoteTherapist = '" + SelectedNoteTherapist + "'where IDTherapist = '" + _selectedIDTherapist + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
