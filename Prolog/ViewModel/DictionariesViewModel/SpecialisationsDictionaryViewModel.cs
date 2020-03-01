using Prolog.Command.DictionaryCommand;
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
using System.Windows.Controls;

namespace Prolog.ViewModel.DictionariesViewModel
{
    class SpecialisationsDictionaryViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        string infoPatientGetter = "";
        public static string selectedIDPatientTemp = "";
        public static string selectedFirstNameTemp = "";
        public static string selectedLastNameTemp = "";
        public DeleteSpecializationCommand deleteSpecializationCommand { get; set; }
        public UpdateSpecializationCommand updateSpecializationCommand { get; set; }
        public AddSpecializationsCommand addSpecializationCommand { get; set; }

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

        private PatientModel selectedRow;
        public PatientModel SelectedRow
        {
            get { return selectedRow; }
            set
            {
                Console.WriteLine("Jestem w Selekcie count0" + IsSelected);
                selectedRow = value;
                SelectedFirstName = "Testt";
                OnPropertyChanged("SelectedRow");
                if (SelectedRow != null)
                {
                    IsSelected = true;
                    Console.WriteLine("Jestem w Selekcie count " + IsSelected + SelectedRow.IDPatient);
                    selectedFirstName = SelectedRow.FirstName;
                    selectedIDPatientTemp = SelectedRow.IDPatient;
                    selectedFirstNameTemp = SelectedRow.FirstName;
                    selectedLastNameTemp = SelectedRow.LastName;
                    Console.WriteLine("Jestem w Selekcie03 " + selectedFirstName);
                }
                else
                {
                    IsSelected = false;
                    Console.WriteLine("Jestem w Selekcie02 " + SelectedRow);
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
                selectedFirstName = SelectedRow.FirstName;
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
        public SpecialisationsDictionaryViewModel()
        {
            deleteSpecializationCommand = new DeleteSpecializationCommand(this);
            updateSpecializationCommand = new UpdateSpecializationCommand(this);
            addSpecializationCommand = new AddSpecializationsCommand(this);
            //saveCommand = new SaveCommand(this);

            patientsList = new ObservableCollection<PatientModel>
            {
                new PatientModel{IDPatient = "1",FirstName = "Joe", LastName = "Doe"},
                new PatientModel{IDPatient = "2",FirstName = "Jof", LastName = "Boe"}
            };
            DataGrid_Loaded();
        }

        public void DeleteSpecialization()
        {
            Console.WriteLine("Jestem w Delecie " + SelectedRow.IDPatient);
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



            DataGrid refresh;
            //DataGrid_Loaded();
        }

        public void UpdateSpecialization()
        {
            //UpdatePatientSubViewModel updateInfoPatient = new UpdatePatientSubViewModel();
            //updateInfoPatient.infoPatience(SelectedRow.IDPatient, SelectedRow.FirstName, SelectedRow.LastName);            
            Console.WriteLine("Jestem w Updacie " + SelectedRow.IDPatient + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
            UpdatePatientSubView updatePatientSubView = new UpdatePatientSubView();
            updatePatientSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("PatientsList");
        }


        public void AddSpecialization()
        {
            Console.WriteLine("Jestem w Adzie ");
            AddPatientSubView addPatientSubView = new AddPatientSubView();
            addPatientSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            OnPropertyChanged("PatientsList");
        }

        public void SavePatient()
        {
            // to jest póki co nie używane
            //Console.WriteLine("Jestem w Saviesie " + SelectedRow.IDPatient + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
        }

        public void DataGrid_Loaded()
        {
            //DBClass.openConnection();

            //DBClass.sql = "select * from patients";
            //DBClass.cmd.CommandType = CommandType.Text;
            //DBClass.cmd.CommandText = DBClass.sql;

            //DBClass.da = new SqlDataAdapter(DBClass.cmd);
            //DBClass.dt = new DataTable();
            //DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            Console.WriteLine("Przy bazie" + DBClass.dt);
            //using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
            //        {
            //            data[j] = reader.GetValue(j).ToString();
            //        }
            //        PatientsList.Add(new PatientModel { IDPatient = (data[0]), FirstName = data[1], LastName = data[2] });
            //        i++;
            //        Console.WriteLine("IdPatient" + reader["IDPatient"].ToString());
            //        Console.WriteLine("IdPatient" + reader["FirstName"].ToString());
            //    }
            //}
            DBClass.closeConnection();
        }

        public void DataGrid_Loaded(string id, string fn, string ln)
        {
            SelectedFirstName = fn;
            PatientsList.Add(new PatientModel { IDPatient = id, FirstName = fn, LastName = ln });
        }

        public string[] InfoPatientGetter()
        {
            string[] selectedPatient = new string[9];
            selectedPatient[0] = selectedIDPatientTemp;
            selectedPatient[1] = selectedFirstNameTemp;
            selectedPatient[2] = selectedLastNameTemp;
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
