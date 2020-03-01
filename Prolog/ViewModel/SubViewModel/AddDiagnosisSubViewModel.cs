﻿using Prolog.Command;
using Prolog.Model;
using Prolog.SQLConnection;
using Prolog.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.ViewModel.SubViewModel
{
    class AddDiagnosisSubViewModel : INotifyPropertyChanged
    {
        private PatientsViewModel _patientsViewModel;        //Deklarujemy zmienną typu PatientsView
        PatientsViewModel getter = new PatientsViewModel();        //Deklarujemy zmienną typu PatientsView
        private PatientsView _patientsView;
        public AddSaveDiagnosisCommand addSaveDiagnosisCommand { get; set; }

        public string FirstNameForm { get; private set; }
        public static string[] selectedPatient = new string[9];
        public static string[] _selectedTherapist = new string[9];
        public static string[] selectedAfterChange = new string[9];
        string[] data = new string[999];

        string temp = "";

        #region Properties

        private string _selectedIDPatient;
        private string _selectedFirstName;
        private string _selectedLastName;
        private string _selectedDateDiagnose;
        private string _selectedNoteDiagnose;
        private string _selectedZIP;
        private string _selectedCity;

        public SaveCommand saveCommand { get; set; }
        public string SelectedIDPatient
        {
            get { return _selectedIDPatient; }
            set
            {
                if (value != _selectedIDPatient)
                {
                    _selectedIDPatient = value;
                    OnPropertyChanged("SelectedIDPatience");

                    selectedAfterChange[0] = SelectedIDPatient;
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
                    selectedAfterChange[1] = SelectedFirstName;
                    //Console.WriteLine("Jestem w Selekcie U " + _selectedIDPatient + SelectedFirstName + SelectedLastName + selectedPatientAfterChange[1]);
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
                    selectedAfterChange[2] = SelectedLastName;
                }
            }
        }
        public string SelectedDateDiagnose
        {
            get { return _selectedDateDiagnose; }
            set
            {
                if (value != _selectedDateDiagnose)
                {
                    _selectedDateDiagnose = value;
                    OnPropertyChanged("SelectedDateDiagnose");
                    selectedAfterChange[3] = SelectedDateDiagnose;
                }
            }
        }
        public string SelectedNoteDiagnose
        {
            get { return _selectedNoteDiagnose; }
            set
            {
                if (value != _selectedNoteDiagnose)
                {
                    _selectedNoteDiagnose = value;
                    OnPropertyChanged("SelectedNoteDiagnose");
                    selectedAfterChange[4] = SelectedNoteDiagnose;
                }
            }
        }
       
        private ObservableCollection<TherapistModel> therapistsList;

        public ObservableCollection<TherapistModel> TherapistsList
        {
            get { return therapistsList; }
            set
            {
                therapistsList = value;
                OnPropertyChanged("TherapistsList");
            }
        }
        private TherapistModel selectedTherapist;
        public TherapistModel SelectedTherapist
        {
            get { return selectedTherapist; }
            set
            {
                if (value != selectedTherapist)
                {
                    selectedTherapist = value;
                    OnPropertyChanged("SelectedTherapist");
                    //selectedPatientAfterChange[7] = SelectedTherapist;
                    Console.WriteLine("Wybrany TH - " + selectedTherapist.IDTherapist);
                    selectedAfterChange[5] = selectedTherapist.IDTherapist;
                }
            }
        }
        #endregion


        public AddDiagnosisSubViewModel()
        {
            addSaveDiagnosisCommand = new AddSaveDiagnosisCommand(this);
            therapistsList = new ObservableCollection<TherapistModel>
            {
            };
            TherapistsList.Clear();

            DBClass.openConnection();
            DBClass.sql = "select * from therapists";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);
            TherapistsList.Clear();
            // wyciągamy dane
            int j = 0;
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    TherapistsList.Add(new TherapistModel { IDTherapist = (data[0]), FirstNameTherapist = data[1], LastNameTherapist = data[2] });
                    Console.WriteLine("Terapeuta - " + data[1]);
                }
            }
            DBClass.closeConnection();

            //saveCommand = new SaveCommand(this);
            //Console.WriteLine("Getterrek - " + SelectedFirstName);
            selectedPatient = new string[9];
            // wyciągnięcie danych z tabeli - patients
            selectedPatient = getter.InfoPatientGetter();
            SelectedIDPatient = selectedPatient[0];
            SelectedFirstName = selectedPatient[1];
            SelectedLastName = selectedPatient[2];
            //SelectedDateOfBirth = selectedPatient[3];
            //SelectedStreet = selectedPatient[4];
            //SelectedZIP = selectedPatient[5];
            //SelectedCity = selectedPatient[6];
            //SelectedIDTherapist = selectedPatientAfterChange[7];
            //SelectedNameTherapist = selectedPatient[8];
            Console.WriteLine("Getterrek - " + selectedPatient[7]);
        }

        public AddDiagnosisSubViewModel(PatientsViewModel patientsViewModel)
        {
            _patientsViewModel = patientsViewModel;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
            //this.DataContext = this;                    // Set DataContext of MainWindow to itself in constructor of MainWindow to resolve binding, bez tego nie będzie BINDOWAĆ !!
        }

        public AddDiagnosisSubViewModel(PatientsView patientsView)       //W konstruktorze umieszczamy PatientsView jako parametr
        {
            _patientsView = patientsView;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
        }

        public string EditPatienceSubViewSave(string _firstName)
        {
            FirstNameForm = _firstName;
            return FirstNameForm;
        }

        internal void infoPatience(string _IDPatientGetter, string _firstNamePatientGetter, string _lastNamePatientGetter)
        {
            temp = _IDPatientGetter;
            _selectedFirstName = _firstNamePatientGetter;
            _selectedLastName = _lastNamePatientGetter;
            Console.WriteLine("First name P01-" + _selectedIDPatient + SelectedFirstName + SelectedLastName);
        }        

        public void AddSaveDiagnosis()
        {
            SelectedIDPatient = selectedAfterChange[0];
            SelectedFirstName = selectedAfterChange[1];
            SelectedLastName = selectedAfterChange[2];
            SelectedDateDiagnose = selectedAfterChange[3];
            SelectedNoteDiagnose = selectedAfterChange[4];
            string _selectedIDTherapist = selectedAfterChange[5];
            Console.WriteLine("Selcted Th2 - " + SelectedDateDiagnose);
            getter.DataGrid_Loaded(SelectedIDPatient, SelectedFirstName, SelectedLastName);
            // konwersja daty z string
            //var dataToSQL = DateTime.ParseExact(SelectedDateDiagnose, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //Console.WriteLine("DataSzmata - " + SelectedDateDiagnose);

            DBClass.openConnection();

            DBClass.sql = "Insert into diagnosis ( DataDiagnosis, NotesDiagnosis, IDTherapist, IDDisorderDiagnosis, IDPatient ) values " +
                "('" + SelectedDateDiagnose + "', '" + SelectedNoteDiagnose + "', '" + _selectedIDTherapist + "', '" + "0" + "', '" + SelectedIDPatient + "')"; ;
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
