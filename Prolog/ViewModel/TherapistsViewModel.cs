using Prolog.Command;
using Prolog.Model;
using Prolog.SQLConnection;
using Prolog.View.SubView;
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
    class TherapistsViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        public static string[] infoTherapistTemp = new string[9];
        string infoPatientGetter = "";
        public static string selectedIDPatientTemp = "";
        public static string selectedFirstNameTemp = "";
        public static string selectedLastNameTemp = "";
        public DeleteTherapistCommand deleteTherapistCommand { get; set; }
        public UpdateTherapistCommand updateTherapistCommand { get; set; }
        public AddTherapistCommand addTherapistCommand { get; set; }

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

        private TherapistModel selectedRow;
        public TherapistModel SelectedRow
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
                    //Console.WriteLine("Jestem w Selekcie count " + IsSelected + SelectedRow.IDTherapist);
                    selectedFirstName = SelectedRow.FirstNameTherapist;
                    infoTherapistTemp[0] = SelectedRow.IDTherapist;
                    infoTherapistTemp[1] = SelectedRow.FirstNameTherapist;
                    infoTherapistTemp[2] = SelectedRow.LastNameTherapist;
                    infoTherapistTemp[3] = SelectedRow.EmailTherapist;
                    infoTherapistTemp[4] = SelectedRow.PhoneTherapist;
                    infoTherapistTemp[5] = SelectedRow.NoteTherapist;
                }
                else
                {
                    IsSelected = false;
                    //+ _therapistsSQLTable.Rows[0]);
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
               // selectedFirstName = SelectedRow.FirstNameTherapist;
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
        public TherapistsViewModel()
        {
            deleteTherapistCommand = new DeleteTherapistCommand(this);
            updateTherapistCommand = new UpdateTherapistCommand(this);
            addTherapistCommand = new AddTherapistCommand(this);
            //saveCommand = new SaveCommand(this);

            therapistsList = new ObservableCollection<TherapistModel>
            {
                new TherapistModel{IDTherapist = "1",FirstNameTherapist = "Mariusz", LastNameTherapist = "Testowy"},
                new TherapistModel{IDTherapist = "2",FirstNameTherapist = "Edyta", LastNameTherapist = "Sprawdzeniowa"}
            };
            DataGrid_Loaded();
        }

        public void DeleteTherapist()
        {
            Console.WriteLine("Jestem w Delecie " + SelectedRow.IDTherapist);
            DBClass.openConnection();

            DBClass.sql = "delete from therapists where IDTherapist = '" + infoTherapistTemp[0] + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            DataGrid refresh;
            DataGrid_Loaded();
        }

        public void CreatePatient()
        {
            DBClass.openConnection();

            DBClass.sql = "insert into therapists (FirstNameTherapist, LastNameTherapist) values ('" + selectedFirstNameTemp + "', '" + selectedLastNameTemp + "')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);
           
            DataGrid refresh;
            //DataGrid_Loaded();
        }

        public void UpdateTherapist()
        {
            //UpdatePatientSubViewModel updateInfoPatient = new UpdatePatientSubViewModel();
            //updateInfoPatient.infoPatience(SelectedRow.IDPatient, SelectedRow.FirstName, SelectedRow.LastName);            
            Console.WriteLine("Jestem w Updacie " + SelectedRow.IDTherapist + " " + SelectedRow.FirstNameTherapist + " " + SelectedRow.LastNameTherapist);
            UpdateTherapistSubView updateTherapistSubView = new UpdateTherapistSubView();
            updateTherapistSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("TherapistsList");
        }


        public void AddTherapist()
        {
            AddTherapistSubView addTherapistSubView = new AddTherapistSubView();
            addTherapistSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("TherapistsList");
        }

        public void SavePatient()
        {
            // to jest póki co nie używane
            //Console.WriteLine("Jestem w Saviesie " + SelectedRow.IDPatient + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
        }

        public void DataGrid_Loaded()
        {
            DBClass.openConnection();

            DBClass.sql = "select * from therapists";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int j = 0;
            //Console.WriteLine("Przy bazie" + DBClass.dt);
            TherapistsList.Clear();
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    if (data[0] != "0")
                    {
                        TherapistsList.Add(new TherapistModel { IDTherapist = (data[0]), FirstNameTherapist = data[1], LastNameTherapist = data[2], EmailTherapist = data[3], PhoneTherapist = data[4], NoteTherapist = data[5] });

                    }
                    //Console.WriteLine("IDTherapist" + reader["IDTherapist"].ToString());
                    //Console.WriteLine("FirstNameTherapist" + reader["FirstNameTherapist"].ToString());
                }
            }

            DBClass.closeConnection();            
        }

        public void DataGrid_Loaded(string id, string fn, string ln)
        {
            SelectedFirstName = fn;
            TherapistsList.Add(new TherapistModel { IDTherapist = id, FirstNameTherapist = fn, LastNameTherapist = ln });
        }

        public string[] InfoTherapistGetter()
        {
            string[] selectedTherapist = new string[9];
            selectedTherapist[0] = infoTherapistTemp[0];
            selectedTherapist[1] = infoTherapistTemp[1];
            selectedTherapist[2] = infoTherapistTemp[2];
            selectedTherapist[3] = infoTherapistTemp[3];
            selectedTherapist[4] = infoTherapistTemp[4];
            selectedTherapist[5] = infoTherapistTemp[5];
            Console.WriteLine("Jestem w Geterze " + infoTherapistTemp[5]);
            return selectedTherapist;
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
