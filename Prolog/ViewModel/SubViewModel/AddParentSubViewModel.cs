using Prolog.Command;
using Prolog.SQLConnection;
using Prolog.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.ViewModel.SubViewModel
{
    class AddParentSubViewModel : INotifyPropertyChanged
    {
        private ParentsViewModel _parentsViewModel;        //Deklarujemy zmienną typu ParentsView
        ParentsViewModel getter = new ParentsViewModel();        //Deklarujemy zmienną typu ParentsView
        private ParentsView _parentsView;

        public string FirstNameForm { get; private set; }
        public static string[] selectedParent = new string[9];
        public static string[] selectedParentAfterChange = new string[9];

        string temp = "";
        private string _selectedIDParent;
        private string _selectedFirstName;
        private string _selectedLastName;

        public AddCommand addCommand { get; set; }
        public string SelectedIDParent
        {
            get { return _selectedIDParent; }
            set
            {
                if (value != _selectedIDParent)
                {
                    _selectedIDParent = value;
                    OnPropertyChanged("SelectedIDPatience");

                    selectedParentAfterChange[0] = SelectedIDParent;
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
                    selectedParentAfterChange[1] = SelectedFirstName;
                    Console.WriteLine("Jestem w Selekcie U " + _selectedIDParent + SelectedFirstName + SelectedLastName + selectedParentAfterChange[1]);
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
                    selectedParentAfterChange[2] = SelectedLastName;
                }
            }
        }

        public AddParentSubViewModel()
        {          
        }

        public AddParentSubViewModel(ParentsViewModel parentsViewModel)
        {
            _parentsViewModel = parentsViewModel;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
        }

        public AddParentSubViewModel(ParentsView parentsView)       //W konstruktorze umieszczamy ParentsView jako parametr
        {
            _parentsView = parentsView;              // Do zadeklarowanej wcześniej zmiennej przypisujemy uchwyt do Form1
        }

        public string EditPatienceSubViewSave(string _firstName)
        {
            FirstNameForm = _firstName;
            return FirstNameForm;
        }

        internal void infoPatience(string _IDParentGetter, string _firstNameParentGetter, string _lastNameParentGetter)
        {
            temp = _IDParentGetter;
            _selectedFirstName = _firstNameParentGetter;
            _selectedLastName = _lastNameParentGetter;
            Console.WriteLine("First name P01-" + _selectedIDParent + SelectedFirstName + SelectedLastName);
        }

        public void SaveAddParent()
        {
            SelectedIDParent = selectedParentAfterChange[0];
            SelectedFirstName = selectedParentAfterChange[1];
            SelectedLastName = selectedParentAfterChange[2];
            getter.DataGrid_Loaded(SelectedIDParent, SelectedFirstName, SelectedLastName);

            DBClass.openConnection();

            DBClass.sql = "Insert into parents( firstName, lastName ) values ('" + SelectedFirstName + "', '" + SelectedLastName + "')";
            //DBClass.sql = "Insert parents Set firstName = '" + SelectedFirstName + "', lastName = '" + SelectedLastName + "'where id = '" + _selectedIDParent + "'";
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
