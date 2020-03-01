using Prolog.Command;
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
    class ParentsViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        public static string selectedIDParentTemp = "";
        public static string selectedFirstNameTemp = "";
        public static string selectedLastNameTemp = "";
        public DeleteParentCommand deleteCommand { get; set; }
        public UpdateParentCommand updateCommand { get; set; }
        public AddParentCommand addCommand { get; set; }

        private ObservableCollection<ParentModel> parentsList;

        public ObservableCollection<ParentModel> ParentsList
        {
            get { return parentsList; }
            set
            {
                parentsList = value;
                OnPropertyChanged("ParentsList");
            }
        }

        private ParentModel selectedRow;
        public ParentModel SelectedRow
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
                    Console.WriteLine("Jestem w Selekcie count " + IsSelected + SelectedRow.IDParent);
                    selectedFirstName = SelectedRow.FirstNameParent;
                    selectedIDParentTemp = SelectedRow.IDParent;
                    selectedFirstNameTemp = SelectedRow.FirstNameParent;
                    selectedLastNameTemp = SelectedRow.LastNameParent;
                    Console.WriteLine("Jestem w Selekcie03 " + selectedFirstName);
                }
                else
                {
                    IsSelected = false;
                    Console.WriteLine("Jestem w Selekcie02 " + SelectedRow);
                    //+ _parentsSQLTable.Rows[0]);
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
                //selectedFirstName = SelectedRow.FirstNameParent;
                OnPropertyChanged("IsSelected");
            }
        }

        private string selectedFirstName;
        public string SelectedFirstName
        {
            get
            {
                //Console.WriteLine("Jestem w SelectedFirstname " + SelectedRow.IDParent);
                return selectedFirstName;
            }
            set
            {
                OnPropertyChanged("SelectedFirstName");
            }
        }
        public ParentsViewModel()
        {
            deleteCommand = new DeleteParentCommand(this);
            updateCommand = new UpdateParentCommand(this);
            addCommand = new AddParentCommand(this);
            //saveCommand = new SaveCommand(this);

            parentsList = new ObservableCollection<ParentModel>
            {
                ////new ParentModel{IDParent = "1",FirstName = "Joe", LastName = "Doe"},
                ////new ParentModel{IDParent = "2",FirstName = "Jof", LastName = "Boe"}
            };
            DataGrid_Loaded();
        }

        public void DeleteParent()
        {
            Console.WriteLine("Jestem w Delecie " + SelectedRow.IDParent);
        }

        public void CreateParent()
        {
            DBClass.openConnection();

            DBClass.sql = "insert into parents (FirstName, LastName) values ('" + selectedFirstNameTemp + "', '" + selectedLastNameTemp + "')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);
            DataGrid refresh;
            //DataGrid_Loaded();
        }

        public void UpdateParent()
        {
           Console.WriteLine("Jestem w Updacie " + SelectedRow.IDParent + " " + SelectedRow.FirstNameParent + " " + SelectedRow.LastNameParent);
            //UpdateParentSubView updateParentSubView = new UpdateParentSubView();
            //updateParentSubView.ShowDialog();
            // TODO: Dodać odświeżanie list ParentsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("ParentsList");
        }


        public void AddParent()
        {
            Console.WriteLine("Jestem w Adzie ");
            AddParentSubView addParentSubView = new AddParentSubView();
            addParentSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PareentsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            OnPropertyChanged("ParentsList");
        }

        public void SaveParent()
        {
            // to jest póki co nie używane
            //Console.WriteLine("Jestem w Saviesie " + SelectedRow.IDParent + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
        }

        public void DataGrid_Loaded()
        {
            ParentsList.Clear();
            DBClass.openConnection();
            DBClass.sql = "select IDParent, FirstNameParent, LastNameParent, TelephoneParent, EmailParent, StreetParent, ZIPParent, CityParent, parents.IDPatient, FirstName, LastName " +
                "from parents, patients where FirstName = (select FirstName from patients where patients.IDPatient = parents.IDPatient) and " +
                "LastName = (select LastName from patients where patients.IDPatient = parents.IDPatient)";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            int i = 0;
            int j = 0;
            //Console.WriteLine("Przy bazie" + DBClass.dt);
            using (SqlDataReader reader = DBClass.cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (j = 0; j <= reader.FieldCount - 1; j++) // Looping throw colums
                    {
                        data[j] = reader.GetValue(j).ToString();
                    }
                    ParentsList.Add(new ParentModel { IDParent = (data[0]), FirstNameParent = data[1], LastNameParent = data[2], TelephoneParent = data[3], EmailParent = data[4], StreetParent = data[5],
                        ZIPParent = data[6], CityParent = data[7], IDPatient = data[8], FirstNamePatient = data[9], LastNamePatient = data[10] });
                    i++;
                    Console.WriteLine("IdParent" + reader["IDParent"].ToString());
                    Console.WriteLine("IdParent" + reader["FirstNameParent"].ToString());
                }
            }
            DBClass.closeConnection();
        }

        public void DataGrid_Loaded(string id, string fn, string ln)
        {
            SelectedFirstName = fn;
            ParentsList.Add(new ParentModel { IDParent = id, FirstNameParent = fn, LastNameParent = ln });
        }

        public string[] InfoParentGetter()
        {
            string[] selectedParent = new string[9];
            selectedParent[0] = selectedIDParentTemp;
            selectedParent[1] = selectedFirstNameTemp;
            selectedParent[2] = selectedLastNameTemp;
            return selectedParent;
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
