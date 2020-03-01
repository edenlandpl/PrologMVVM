using Prolog.Command;
using Prolog.Model;
using Prolog.SQLConnection;
using Prolog.ViewModel.SubView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Prolog.ViewModel
{
    class PatientsViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        string infoPatientGetter = "";
        public static string[] infoPatientTemp = new string[99];
        public static DateTime dateOfBirthTemp;
        public static string selectedIDPatientTemp = "";
        public static string selectedFirstNameTemp = "";
        public static string selectedLastNameTemp = "";
        public static string selectedDateOfBirthTemp = "";
        public static string IDTherapyTemp = "";
        public DeleteCommand deleteCommand { get; set; }
        public CreateCommand createCommand { get; set; }
        public UpdateCommand updateCommand { get; set; }
        public SaveCommand saveCommand { get; set; }
        public AddCommand addCommand { get; set; }
        public AddDiagnosisCommand addDiagnosisCommand { get; set; }
        public AddTherapyCommand addTherapyCommand { get; set; }
        public AddActivityCommand addActivityCommand { get; set; }

        private ObservableCollection<PatientModel> patientsList;

        public ObservableCollection<PatientModel> PatientsList
        {
            get { return patientsList; }
            set
            {
                patientsList = value;
                OnPropertyChanged("PatientsList");
            }
        }
        private ObservableCollection<DiagnosisModel> diagnosisList;
        public ObservableCollection<DiagnosisModel> DiagnosisList
        {
            get { return diagnosisList; }
            set
            {
                diagnosisList = value;
                OnPropertyChanged("DiagnosisList");
            }
        }

        private ObservableCollection<TherapyModel> therapyList;
        public ObservableCollection<TherapyModel> TherapyList
        {
            get { return therapyList; }
            set
            {
                TherapyList = value;
                OnPropertyChanged("TherapyList");
            }
        }

        private ObservableCollection<ActivityModel> activityList;
        public ObservableCollection<ActivityModel> ActivityList
        {
            get { return activityList; }
            set
            {
                activityList = value;
                OnPropertyChanged("ActivityList");
            }
        }
        private PatientModel selectedRow;
        public PatientModel SelectedRow
        {
            get { return selectedRow; }
            set
            {
                //Console.WriteLine("Jestem w Selekcie count0" + IsSelected);
                selectedRow = value;
                SelectedFirstName = "Testt";
                OnPropertyChanged("SelectedRow");
                if (SelectedRow != null)
                {
                    IsSelected = true;
                    Console.WriteLine("Jestem w Selekcie count " + IsSelected + SelectedRow.DateOfBirth);
                    //infoPatientTemp[4] = SelectedRow.FirstName;
                    infoPatientTemp[0] = SelectedRow.IDPatient;
                    infoPatientTemp[1] = SelectedRow.FirstName;
                    infoPatientTemp[2] = SelectedRow.LastName;
                    infoPatientTemp[3] = SelectedRow.DateOfBirth;
                    infoPatientTemp[4] = SelectedRow.Street;
                    infoPatientTemp[5] = SelectedRow.ZIP;
                    infoPatientTemp[6] = SelectedRow.City;
                    infoPatientTemp[7] = SelectedRow.IDTherapist;
                    infoPatientTemp[8] = SelectedRow.Phone;
                    infoPatientTemp[9] = SelectedRow.Email;
                    Console.WriteLine("Jestem w Selekcie03 " + infoPatientTemp[4] + infoPatientTemp[3] + infoPatientTemp[8] + infoPatientTemp[9]);
                    GetDiagnosisPatient();
                    GetTherapyPatient();
                    GetActivityPatient();
                }                
                else
                {
                    IsSelected = false;
                    //Console.WriteLine("Jestem w Selekcie02 " + SelectedRow);
                    //+ _patientsSQLTable.Rows[0]);
                }
            }
        }

        private bool isSelected = false;
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

        private string selectedFirstName;
        public string SelectedFirstName
        {
            get
            {
                //Console.WriteLine("Jestem w SelectedFirstname " + SelectedRow.IDPatient);
                return selectedFirstName;
            }
            set
            {
                OnPropertyChanged("SelectedFirstName");
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


        public PatientsViewModel()
        {
            deleteCommand = new DeleteCommand(this);
            createCommand = new CreateCommand(this);
            updateCommand = new UpdateCommand(this);
            addCommand = new AddCommand(this);
            addDiagnosisCommand = new AddDiagnosisCommand(this);
            addTherapyCommand = new AddTherapyCommand(this);
            addActivityCommand = new AddActivityCommand(this);
            //saveCommand = new SaveCommand(this);

            patientsList = new ObservableCollection<PatientModel>
            {
                //new PatientModel{IDPatient = "1",FirstName = "Joe", LastName = "Doe"},
                //new PatientModel{IDPatient = "2",FirstName = "Jof", LastName = "Boe"}
            };
            therapistsList = new ObservableCollection<TherapistModel>
            {
            };
            diagnosisList = new ObservableCollection<DiagnosisModel>
            {
            }; 
            therapyList = new ObservableCollection<TherapyModel>
            {
            };
            activityList = new ObservableCollection<ActivityModel>
            {
            };
            DataGrid_Loaded();
        }
        public void CreatePatient()
        {
            DBClass.openConnection();

            DBClass.sql = "insert into patients (FirstName, LastName) values ('" + selectedFirstNameTemp + "', '" + selectedLastNameTemp + "')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);


            #region Sekcja specjalna - SQLite wersja, w razie czego ...
            //// sekcja testowa
            //string _firstName = "Bartolinii";
            //string _lastName = "Bartłomiej20";
            //Console.WriteLine("Jestem w Creacie");
            //var con = new SQLiteConnection("Data Source=DELL-ADI\\SQLEXPRESS;Initial Catalog=Prolog;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
            //try
            //{
            //    con.Open();
            //    SQLiteCommand sqlCmd = con.CreateCommand();
            //    string updateSQLStatement = "Insert INTO patients (firstName, lastName) VALUES ('" + _firstName + "', '" + _lastName + "')";
            //    sqlCmd = new SQLiteCommand(updateSQLStatement, con);
            //    using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCmd.CommandText, con))
            //    {
            //        var sqlCmdUpdate = new SQLiteCommand(updateSQLStatement, con);
            //        sqlCmdUpdate.ExecuteNonQuery();
            //        con.Close();
            //        Console.WriteLine("Jestem w Creacie po zamknięciu");
            //    }
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message);
            //}
            #endregion

            DataGrid refresh;
            //DataGrid_Loaded();
        }

        public void UpdatePatient()
        {
            //UpdatePatientSubViewModel updateInfoPatient = new UpdatePatientSubViewModel();
            //updateInfoPatient.infoPatience(SelectedRow.IDPatient, SelectedRow.FirstName, SelectedRow.LastName);            
            //Console.WriteLine("Jestem w Updacie " + SelectedRow.IDPatient + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
            UpdatePatientSubView updatePatientSubView = new UpdatePatientSubView();
            updatePatientSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("PatientsList");
        }


        public void AddPatient()
        {
            //Console.WriteLine("Jestem w Adzie ");
            Therapists_Loaded();
            AddPatientSubView addPatientSubView = new AddPatientSubView();
            addPatientSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            OnPropertyChanged("PatientsList");
            DataGrid_Loaded();
        }
        public void Delete()
        {
            DBClass.openConnection();

            DBClass.sql = "delete from patients where IDPatient = '" + infoPatientTemp[0] + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            DataGrid refresh;
            DataGrid_Loaded();
        }

        public void AddDiagnosis()
        {
            AddDiagnosisSubView addDiagnosisSubView = new AddDiagnosisSubView();
            addDiagnosisSubView.ShowDialog();
            DataGrid_Loaded();
            OnPropertyChanged("PatientsList");
        }        
        public void AddTherapy()
        {
            AddTherapySubView addTherapySubView = new AddTherapySubView();
            addTherapySubView.ShowDialog();
            DataGrid_Loaded();
            OnPropertyChanged("PatientsList");
        }
        public void AddActivity()
        {
            AddActivitySubView addActivitySubView = new AddActivitySubView();
            addActivitySubView.ShowDialog();
            DataGrid_Loaded();
            OnPropertyChanged("PatientsList");
        }

        public void SavePatient()
        {
            // to jest póki co nie używane
            //Console.WriteLine("Jestem w Saviesie " + SelectedRow.IDPatient + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
        }

        public void DataGrid_Loaded()
        {
            DBClass.openConnection(); 
                //) CAST(DateOfBirth AS date), convert(Date, DateOfBirth, 23) 
            DBClass.sql = "select distinct IDPatient, FirstName, LastName, convert(VARCHAR, DateOfBirth, 23) , " +
                "Street, ZIP, City, therapists.LastNameTherapist, patients.IDTherapist, Phone, Email " +
                "from patients, therapists, therapistSpecialization where therapists.LastNameTherapist = " +
                "(select LastNameTherapist from therapists where IDTherapist = patients.IDTherapist)";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            //Console.WriteLine("Przy bazie" + data[3]);
            PatientsList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    if(data[7] == null )
                    {
                        data[7] = "";
                    }                    
                    //Console.WriteLine("Przy bazie" + data[3]);
                    PatientsList.Add(new PatientModel { IDPatient = (data[0]), FirstName = data[1], LastName = data[2], DateOfBirth = data[3], Street = data[4], ZIP = data[5], 
                                                        City = data[6], IDTherapist = data[7], NameTherapist = (data[8]), Phone = data[9], Email = data[10] });
                    //Console.WriteLine("IDTherapist - " + data[7] + data[8]);
                }
            }

            //cmd.CommandText = "update projekt set ocena_projekt = @ocenaProjekt " +
            //                "where(select ID_Studenta from Student where STU_nazwisko = @nazwiskoS) = ID_studenta and " +
            //                "(select ID_prowadzacego from Prowadzacy where nazwisko_pro = 'Wodecki') = Pro_ID_prowadzacego and " +
            //                "(select ID_projektu from Projekty where temat = @temat) = ID_projektu;";


            DBClass.closeConnection();

            //int i = 0;
            //int j = 0;
            //var con = new SQLiteConnection("Data Source=Prolog;Version=3;New=False;Compress=True;");
            try
            {
                //String con = "Server=localhost;Database=Prolog;User Id=prolog;password=prolog";
                //String con = "Server=DELL-ADI\\SQLEXPRESS; uid=prolog; pwd=prolog; database=Prolog;"; 
                ////String con = " server = 194.181.228.20; uid = edenland_prolog; pwd = adikos; database = edenland_prolog;";
                //MySqlConnection Connection = new MySqlConnection(con);
                //string CommandText = "select * from patients";
                //MySqlDataAdapter ApapterSQL = new MySqlDataAdapter();
                //ApapterSQL.SelectCommand = new MySqlCommand(CommandText, Connection);
                //MySqlCommandBuilder builder = new MySqlCommandBuilder(ApapterSQL);
                //Connection.Open();

                //DataTable dane = new DataTable();
                //ApapterSQL.Fill(dane);
                ////ReservationFromWebList.ItemsSource = dane.DefaultView;
                //ApapterSQL.Update(dane);

                //con.Open();
                //SQLiteCommand sqlCmd = con.CreateCommand();
                //sqlCmd.CommandText = "SELECT * FROM patients ";
                //using (SQLiteDataReader rdr = sqlCmd.ExecuteReader())
                //{
                //    while (rdr.Read()) // Reading Rows
                //    {
                //        for (j = 0; j <= rdr.FieldCount - 1; j++) // Looping throw colums
                //        {
                //            data[j] = rdr.GetValue(j).ToString();
                //        }
                //        PatientsList.Add(new PatientModel { IDPatient = (data[0]), FirstName = data[1], LastName = data[2] });
                //        i++;
                //    }
                //}
                //Connection.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public void GetDiagnosisPatient()
        {
            DBClass.openConnection();
            //) CAST(DateOfBirth AS date), convert(Date, DateOfBirth, 23) 
            //select distinct convert(VARCHAR, DataDiagnosis, 23) , NotesDiagnosis, LastNameTherapist from therapists, diagnosis where(diagnosis.IDPatient = '2') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '2')
            DBClass.sql = "select distinct IDDiagnosis, convert(VARCHAR, DataDiagnosis, 23) , NotesDiagnosis, LastNameTherapist " +
                "from therapists, diagnosis " +
                "where (diagnosis.IDPatient = '" + infoPatientTemp[0] +  "') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '2')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int j = 0;
            DiagnosisList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    if (data[7] == null)
                    {
                        data[7] = "";
                    }
                    DiagnosisList.Add(new DiagnosisModel
                    {
                        IDDiagnosis = (data[0]),
                        DataDiagnosis = data[1],
                        NotesDiagnosis = data[2],
                        LastNameTherapist = data[3]
                    });
                    //Console.WriteLine("IDTherapist - " + data[7] + data[8]);
                }
            }
        }
        public void GetTherapyPatient()
        {
            DBClass.openConnection();
            //) CAST(DateOfBirth AS date), convert(Date, DateOfBirth, 23) 
            //select distinct convert(VARCHAR, DataDiagnosis, 23) , NotesDiagnosis, LastNameTherapist from therapists, diagnosis where(diagnosis.IDPatient = '2') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '2')
            DBClass.sql = "select distinct IDTherapy, convert(VARCHAR, DateTherapy, 23) , TherapyNote, LastNameTherapist " +
                "from therapists, therapy " +
                "where (therapy.IDPatient = '" + infoPatientTemp[0] + "') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '2')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int j = 0;
            TherapyList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    if (data[7] == null)
                    {
                        data[7] = "";
                    }
                    TherapyList.Add(new TherapyModel
                    {
                        IDTherapy = (data[0]),
                        DataTherapy = data[1],
                        NotesTherapy = data[2],
                        LastNameTherapist = data[3]
                    });

                    //Console.WriteLine("IDTherapist - " + data[7] + data[8]);
                }
            }
        }
        public void GetActivityPatient()
        {
            DBClass.openConnection();
            //) CAST(DateOfBirth AS date), convert(Date, DateOfBirth, 23) 
            //select distinct convert(VARCHAR, DataDiagnosis, 23) , NotesDiagnosis, LastNameTherapist from therapists, diagnosis where(diagnosis.IDPatient = '2') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '2')
            DBClass.sql = "select distinct IDActivities, convert(VARCHAR, DateActivity, 23) , ActivityNote, LastNameTherapist " +
                "from therapists, activities " +
                "where (activities.IDTherapy = '" + data[0] + "') and therapists.LastNameTherapist = (select LastNameTherapist from therapists where IDTherapist = '2')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int j = 0;
            ActivityList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    if (data[7] == null)
                    {
                        data[7] = "";
                    }
                    ActivityList.Add(new ActivityModel
                    {
                        IDActivity = (data[0]),
                        DataActivity = data[1],
                        NotesActivity = data[2],
                        LastNameTherapist = data[3]
                    });
                    //Console.WriteLine("IDTherapist - " + data[7] + data[8]);
                }
            }
        }
        public void Therapists_Loaded()
        {            
            //DBClass.openConnection();
            //DBClass.sql = "select * from therapists";
            //DBClass.cmd.CommandType = CommandType.Text;
            //DBClass.cmd.CommandText = DBClass.sql;

            //DBClass.da = new SqlDataAdapter(DBClass.cmd);
            //DBClass.dt = new DataTable();
            //DBClass.da.Fill(DBClass.dt);
            //TherapistsList.Clear();
            //// wyciągamy dane
            //int j = 0;
            //using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
            //        {                        
            //            data[j] = reader.GetValue(j).ToString();
            //        }
            //        TherapistsList.Add(new TherapistModel { IDTherapist = (data[0]), FirstNameTherapist = data[1], LastNameTherapist = data[2] });
            //        Console.WriteLine("Terapeuta - " + data[1]);
            //    }
            //}
            //DBClass.closeConnection();
        }

        public string[] ListTherapistsGetter()
        {
            string[] listTherapistsGetter = new string[9];
            listTherapistsGetter[0] = infoPatientTemp[0];
            listTherapistsGetter[1] = infoPatientTemp[1];
            listTherapistsGetter[2] = infoPatientTemp[2];
            listTherapistsGetter[3] = infoPatientTemp[3];
            listTherapistsGetter[4] = infoPatientTemp[4];
            listTherapistsGetter[5] = infoPatientTemp[5];
            listTherapistsGetter[6] = infoPatientTemp[6];
            listTherapistsGetter[7] = infoPatientTemp[7];
            return listTherapistsGetter;
        }
        public void DataGrid_Loaded(string id, string fn, string ln)
        {
            SelectedFirstName = fn;
            PatientsList.Add(new PatientModel { IDPatient = id, FirstName = fn, LastName = ln });
        }

        public string[] InfoPatientGetter()
        {
            string[] selectedPatient = new string[9];
            selectedPatient[0] = infoPatientTemp[0];
            selectedPatient[1] = infoPatientTemp[1];
            selectedPatient[2] = infoPatientTemp[2];
            selectedPatient[3] = infoPatientTemp[3];
            selectedPatient[4] = infoPatientTemp[4];
            selectedPatient[5] = infoPatientTemp[5];
            selectedPatient[6] = infoPatientTemp[6];
            selectedPatient[7] = infoPatientTemp[7];
            selectedPatient[8] = infoPatientTemp[8];
            return selectedPatient;
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void INotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
