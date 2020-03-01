﻿using Prolog.Command;
using Prolog.Model;
using Prolog.SQLConnection;
using Prolog.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;

namespace Prolog.ViewModel.SubViewModel
{
    class UpdatePatientSubViewModel : INotifyPropertyChanged
    {
        private PatientsViewModel _patientsViewModel;        //Deklarujemy zmienną typu PatientsView
        PatientsViewModel getter = new PatientsViewModel();        //Deklarujemy zmienną typu PatientsView
        private PatientsView _patientsView;

        public string FirstNameForm { get; private set; }
        public static string[] selectedPatient = new string[9];
        public static string[] _selectedTherapist = new string[9];
        public static string[] selectedPatientAfterChange = new string[9];
        string[] data = new string[999];

        string temp = "";

        #region Properties

        private string _selectedIDPatient;
        private string _selectedFirstName;
        private string _selectedLastName;
        private string _selectedDateOfBirth;
        private string _selectedStreet;
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

                    selectedPatientAfterChange[0] = SelectedIDPatient;
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
                    selectedPatientAfterChange[1] = SelectedFirstName;
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
                    selectedPatientAfterChange[2] = SelectedLastName;
                }
            }
        }
        public string SelectedDateOfBirth
        {
            get { return _selectedDateOfBirth; }
            set
            {
                if (value != _selectedDateOfBirth)
                {
                    _selectedDateOfBirth = value;
                    OnPropertyChanged("SelectedDateOfBirth");
                    selectedPatientAfterChange[3] = SelectedDateOfBirth;
                }
            }
        }
        public string SelectedStreet
        {
            get { return _selectedStreet; }
            set
            {
                if (value != _selectedStreet)
                {
                    _selectedStreet = value;
                    OnPropertyChanged("SelectedStreet");
                    selectedPatientAfterChange[4] = SelectedStreet;
                }
            }
        }
        public string SelectedZIP
        {
            get { return _selectedZIP; }
            set
            {
                if (value != _selectedZIP)
                {
                    _selectedZIP = value;
                    OnPropertyChanged("SelectedZIP");
                    selectedPatientAfterChange[5] = SelectedZIP;
                }
            }
        }
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (value != _selectedCity)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                    selectedPatientAfterChange[6] = SelectedCity;
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
                    selectedPatientAfterChange[7] = selectedTherapist.IDTherapist;
                }
            }
        }
        #endregion


        public UpdatePatientSubViewModel()
        {
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

            saveCommand = new SaveCommand(this);
            //Console.WriteLine("Getterrek - " + SelectedFirstName);
            selectedPatient = new string[9];
            // wyciągnięcie danych z tabeli - patients
            selectedPatient = getter.InfoPatientGetter();
            SelectedIDPatient = selectedPatient[0];
            SelectedFirstName = selectedPatient[1];
            SelectedLastName = selectedPatient[2];
            SelectedDateOfBirth = selectedPatient[3];
            SelectedStreet = selectedPatient[4];
            SelectedZIP = selectedPatient[5];
            SelectedCity = selectedPatient[6];
            //SelectedIDTherapist = selectedPatientAfterChange[7];
            //SelectedNameTherapist = selectedPatient[8];
            Console.WriteLine("Getterrek - " + selectedPatient[7]);
        }

        public UpdatePatientSubViewModel(PatientsViewModel patientsViewModel)
        {
            _patientsViewModel = patientsViewModel;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
            //this.DataContext = this;                    // Set DataContext of MainWindow to itself in constructor of MainWindow to resolve binding, bez tego nie będzie BINDOWAĆ !!
        }

        public UpdatePatientSubViewModel(PatientsView patientsView)       //W konstruktorze umieszczamy PatientsView jako parametr
        {
            //InitializeComponent();
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

        public void SaveUpdatePatient()
        {
            SelectedIDPatient = selectedPatientAfterChange[0];
            SelectedFirstName = selectedPatientAfterChange[1];
            SelectedLastName = selectedPatientAfterChange[2];
            SelectedDateOfBirth = selectedPatientAfterChange[3];
            SelectedStreet = selectedPatientAfterChange[4];
            SelectedZIP = selectedPatientAfterChange[5];
            SelectedCity = selectedPatientAfterChange[6]; 
            string _selectedIDTherapist = selectedPatientAfterChange[7];
            Console.WriteLine("Selcted Th2 - " + _selectedIDTherapist);
            //SelectedDateOfBirth = selectedPatientAfterChange[3];
            getter.DataGrid_Loaded(SelectedIDPatient, SelectedFirstName, SelectedLastName);
            // konwersja daty z string
            var dataToSQL = DateTime.ParseExact(SelectedDateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Console.WriteLine("DataSzmata - " + SelectedDateOfBirth);

            DBClass.openConnection();

            DBClass.sql = "Update patients Set FirstName = '" + SelectedFirstName + "', LastName = '" + SelectedLastName + "', DateOfBirth = '" + SelectedDateOfBirth + "', Street = '" + SelectedStreet +
                "', ZIP = '" + SelectedZIP + "', City = '" + SelectedCity + "', IDTherapist = '" + _selectedIDTherapist + "'where IDPatient = '" + _selectedIDPatient + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            #region Wyciągamy dane ... tu niepotrzebne
            //// wyciągamy dane
            //int i = 0;
            //int j = 0;
            //Console.WriteLine("Przy bazie" + DBClass.dt);
            //using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        //for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
            //        //{
            //        //    data[j] = reader.GetValue(j).ToString();
            //        //}
            //        //PatientsList.Add(new PatientModel { IDPatient = (data[0]), FirstName = data[1], LastName = data[2] });
            //        //i++;
            //        //Console.WriteLine("Id" + reader["id"].ToString());
            //        //Console.WriteLine("Id" + reader["firstName"].ToString());
            //    }
            //}
            #endregion


            #region Obsługa bazy SQLite
            //Console.WriteLine("Jestem w Savie Update " + _selectedIDPatient + SelectedFirstName + SelectedLastName);
            //var con = new SQLiteConnection("Data Source=PrologSQLLite.db;Version=3;New=False;Compress=True;");
            //try
            //{
            //    con.Open();
            //    SQLiteCommand sqlCmd = con.CreateCommand();
            //    Console.WriteLine("Jestem w Savie Update " + _selectedIDPatient + SelectedFirstName + SelectedLastName);
            //    string updateSQLStatement = "Update patients Set firstName = '" + SelectedFirstName + "', lastName = '" + SelectedLastName + "'where id = '" + _selectedIDPatient + "'";
            //    sqlCmd = new SQLiteCommand(updateSQLStatement, con);
            //    using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCmd.CommandText, con))
            //    {
            //        DataTable dataTable = new DataTable();
            //        dataAdapter.Fill(dataTable);
            //        System.Console.WriteLine("Przesłana linia zaznaczona 03 - " + _selectedIDPatient + SelectedFirstName + SelectedLastName);
            //        var sqlCmdUpdate = new SQLiteCommand(updateSQLStatement, con);
            //        sqlCmdUpdate.ExecuteNonQuery();
            //        dataAdapter.Update(dataTable);
            //        con.Close();
            //        //refresh Datagrid;
            //        getter.DataGrid_Loaded();
            //    }
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message);
            //}
            #endregion
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
