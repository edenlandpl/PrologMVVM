using Prolog.Command;
using Prolog.Model;
using Prolog.SQLConnection;
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

namespace Prolog.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        string[] data = new string[999];
        public static string[] infoUserTemp = new string[9];
        string infoPatientGetter = "";
        public static string selectedIDPatientTemp = "";
        public static string selectedFirstNameTemp = "";
        public static string selectedLastNameTemp = "";
        int[] usersAttributesTemp = new int[99];
        int i;
        //public DeleteUserCommand deleteUserCommand { get; set; }
        //public UpdateUserCommand updateUserCommand { get; set; }
        //public AddUserCommand addUserCommand { get; set; }
        [Flags]
        enum UsersAttributes
        {
            None = 0, Administrator = 1, Terapeuta = 2, Recepcjonista = 4
        }
        public UpdateUserCommand updateUserCommand { get; set; }
        //public AddRoleUserCommand addRoleUserCommand { get; set; }

        private ObservableCollection<UserModel> usersList;

        public ObservableCollection<UserModel> UsersList
        {
            get { return usersList; }
            set
            {
                usersList = value;
                OnPropertyChanged("UsersList");
            }
        }

        private UserModel selectedRow;
        public UserModel SelectedRow
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
                    infoUserTemp[0] = SelectedRow.IDUser;
                    infoUserTemp[1] = "adi@gmail.com";
                    infoUserTemp[2] = SelectedRow.PasswordUser;
                    infoUserTemp[3] = SelectedRow.TypeUser;
                    infoUserTemp[4] = "Adrian";
                    infoUserTemp[5] = "Głąbik";
                    SelectedFirstName = "Adrian";
                    SelectedLastName = SelectedRow.LastNameUser;
                    //infoUserTemp[4] = ;
                    //var s = SelectedRow.TypeUser;
                    //var e2 = (UsersAttributes)Enum.Parse(typeof(UsersAttributes), s);
                    //Console.WriteLine("Jestem w Selekcie03 " + infoUserTemp[3] + " numer - " + (usersAttributesTemp[int.Parse(infoUserTemp[0])-1]));
                    UserIsAdmin = false;
                    UserIsRecepcjonist = false;
                    UserIsTherapist = false;
                    //int u = int.Parse(SelectedRow.IDUser);
                    //if (UsersAttributes.Administrator == (UsersAttributes)(usersAttributesTemp[int.Parse(infoUserTemp[0]) - 1]))
                    UsersAttributes allowedAttributes = UsersAttributes.Administrator | UsersAttributes.Recepcjonista | UsersAttributes.Terapeuta;
                    allowedAttributes = UsersAttributes.Administrator & (UsersAttributes)(usersAttributesTemp[int.Parse(infoUserTemp[0]) - 1]);
                    if (allowedAttributes > 0)
                    {
                        UserIsAdmin = true;
                    }
                    allowedAttributes = UsersAttributes.Recepcjonista & (UsersAttributes)(usersAttributesTemp[int.Parse(infoUserTemp[0]) - 1]);
                    if (allowedAttributes > 0)
                    {
                        UserIsRecepcjonist = true;
                    }
                    allowedAttributes = UsersAttributes.Terapeuta & (UsersAttributes)(usersAttributesTemp[int.Parse(infoUserTemp[0]) - 1]);
                    if (allowedAttributes > 0)
                    {
                        UserIsTherapist = true;
                    }
                }
                else
                {
                    IsSelected = false;
                    //Console.WriteLine("Jestem w Selekcie02 " + SelectedRow);
                    //+ _usersSQLTable.Rows[0]);
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
                // selectedFirstName = SelectedRow.FirstNameUser;
                OnPropertyChanged("IsSelected");
            }
        }

        private string selectedFirstName = "Adrian";
        public string SelectedFirstName
        {
            get
            {
                //Console.WriteLine("Jestem w SelectedFirstname " + SelectedRow.IDPatient);
                return selectedFirstName;
            }
            set
            {
                selectedFirstName = value;
                OnPropertyChanged("SelectedFirstName");
            }
        }
        private string selectedLastName;
        public string SelectedLastName
        {
            get
            {
                //Console.WriteLine("Jestem w SelectedFirstname " + SelectedRow.IDPatient);
                return selectedLastName;
            }
            set
            {
                selectedLastName = value;
                OnPropertyChanged("SelectedLastName");
            }
        }


        private string[] selectedAttributeUser;

        public string[] SelectedAttributeUser
        {
            get
            {
                if (UsersAttributes.Administrator == (UsersAttributes)1)
                {
                    //Console.WriteLine("User - " + UsersAttributes.Administrator);
                    //SelectedAttributeUser[0] = UsersAttributes.Administrator.ToString();
                    UserIsAdmin = true;
                    Console.WriteLine("User string - " + SelectedAttributeUser[0]);
                }
                return selectedAttributeUser;
            }
            set
            {
                OnPropertyChanged("SelectedAttributeUser");
            }
        }
        private bool userIsAdmin = false;

        public bool UserIsAdmin
        {
            get
            {
                return userIsAdmin;
            }
            set
            {
                userIsAdmin = value;
                OnPropertyChanged("UserIsAdmin");
            }
        }

        private bool userIsTherapist = false;

        public bool UserIsTherapist
        {
            get
            {
                return userIsTherapist;
            }
            set
            {
                userIsTherapist = value;
                OnPropertyChanged("UserIsTherapist");
            }
        }
        private bool userIsRecepcjonist = false;

        public bool UserIsRecepcjonist
        {
            get
            {
                return userIsRecepcjonist;
            }
            set
            {
                userIsRecepcjonist = value;
                OnPropertyChanged("UserIsRecepcjonist");
            }
        }
        private bool userIsAdminNew = false;

        public bool UserIsAdminNew
        {
            get
            {
                return userIsAdminNew;
            }
            set
            {
                userIsAdminNew = value;
                OnPropertyChanged("UserIsAdminNew");
            }
        }

        private bool userIsTherapistNew = false;

        public bool UserIsTherapistNew
        {
            get
            {
                return userIsTherapistNew;
            }
            set
            {
                userIsTherapistNew = value;
                OnPropertyChanged("UserIsTherapistNew");
            }
        }
        private bool userIsRecepcjonistNew = false;

        public bool UserIsRecepcjonistNew
        {
            get
            {
                return userIsRecepcjonistNew;
            }
            set
            {
                userIsRecepcjonistNew = value;
                OnPropertyChanged("UserIsRecepcjonistNew");
            }
        }
        public UserViewModel()
        {
            //addRoleUserCommand = new AddRoleUserCommand(this);
            updateUserCommand = new UpdateUserCommand(this);

            // atrybuty uzytkowników. 
            UsersAttributes allowedAttributes = UsersAttributes.Administrator | UsersAttributes.Recepcjonista | UsersAttributes.Terapeuta;
            int tempTypeUser = 5;
            //allowedAttributes = allowedAttributes | tempTypeUser;
            //UsersAttributes usersAttributesInstance = (UsersAttributes)tempTypeUser;
            //Console.WriteLine("Attibutes - " + usersAttributesInstance);
            // sprawdzenie czy użytkownik ma prawa Administratora - (UsersAttributes)1 sprawdza czy jest w prawach Administrator (1)
            //if (UsersAttributes.Administrator == (UsersAttributes)1)
            //{
            //    Console.WriteLine("User - " + UsersAttributes.Administrator);
            //    string ua = UsersAttributes.Administrator.ToString();
            //    Console.WriteLine("User string - " + ua);
            //}

            usersList = new ObservableCollection<UserModel>
            {
                new UserModel{IDUser = "1",NameUser = "Mariusz", PasswordUser = "pas", TypeUser = "1", FirstNameUser = "Adi", LastNameUser = "Głąbik"},
                new UserModel{IDUser = "2",NameUser = "Edyta", PasswordUser = "pas", TypeUser = "1", FirstNameUser = "Aga", LastNameUser = "Wójcik"}
            };

            selectedFirstName = "Adrian";
            DataGrid_Loaded();
        }

        public void DeleteUser()
        {
            Console.WriteLine("Jestem w Delecie " + SelectedRow.IDUser);
            DBClass.openConnection();

            DBClass.sql = "delete from users where IDUser = '" + infoUserTemp[0] + "'";
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

            DBClass.sql = "insert into users (nameUser, passwordUser) values ('" + infoUserTemp[1] + "', '" + infoUserTemp[2] + "')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            DataGrid refresh;
            //DataGrid_Loaded();
        }

        public void UpdateUserView()
        {
            //UpdatePatientSubViewModel updateInfoPatient = new UpdatePatientSubViewModel();
            //updateInfoPatient.infoPatience(SelectedRow.IDPatient, SelectedRow.FirstName, SelectedRow.LastName);            
            //Console.WriteLine("Jestem w Updacie " + SelectedRow.IDUser + " " + SelectedRow.FirstNameUser + " " + SelectedRow.LastNameUser);
            //UpdateUserSubView updateUserSubView = new UpdateUserSubView();
            //updateUserSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("UsersList");
        }


        public void AddUser()
        {
            //AddUserSubView addUserSubView = new AddUserSubView();
            //addUserSubView.ShowDialog();
            // TODO: Dodać odświeżanie list PatientsList, wtedy odświeży się również datagrid, można zmienić tylko tą jedną edytowaną linię - w OnPropertyChanges już jest
            DataGrid_Loaded();
            OnPropertyChanged("UsersList");
        }

        public void SavePatient()
        {
            // to jest póki co nie używane
            //Console.WriteLine("Jestem w Saviesie " + SelectedRow.IDPatient + " " + SelectedRow.FirstName + " " + SelectedRow.LastName);
        }

        public void DataGrid_Loaded()
        {
            DBClass.openConnection();

            DBClass.sql = "select * from users";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);

            // wyciągamy dane
            i = 0;
            int j = 0;
            //Console.WriteLine("Przy bazie" + DBClass.dt);
            UsersList.Clear();
            UsersAttributes usersAttributesInstance;
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
                        UsersList.Add(new UserModel
                        {
                            IDUser = (data[0]),
                            NameUser = data[1],
                            PasswordUser = data[2],
                            TypeUser = ((UsersAttributes)(Int32.Parse(data[3]))).ToString(),
                            FirstNameUser = data[4],
                            LastNameUser = data[5]
                        });
                    }
                    usersAttributesTemp[i] = Int32.Parse(data[3]);
                    Console.WriteLine("userAtributes in Loaded " + (usersAttributesTemp[i]) + "data - " + data[3]);
                    i++;
                    //Console.WriteLine("FirstNameUser" + reader["FirstNameUser"].ToString());
                }
            }
            selectedFirstName = "Adrian";
            DBClass.closeConnection();
        }

        public void DataGrid_Loaded(string id, string fn, string ln)
        {
            selectedFirstName = "Adrian";
            //UsersList.Add(new UserModel { IDUser = id, NameUser = fn, PasswordUser = ln });
        }

        public string[] InfoUserGetter()
        {
            string[] selectedUser = new string[9];
            selectedUser[0] = infoUserTemp[0];
            selectedUser[1] = infoUserTemp[1];
            selectedUser[2] = infoUserTemp[2];
            selectedUser[3] = infoUserTemp[3];
            selectedUser[4] = infoUserTemp[4];
            selectedUser[5] = infoUserTemp[5];
            Console.WriteLine("Jestem w Geterze " + infoUserTemp[5]);
            return selectedUser;
        }

        public void UpdateUser()
        {
            int typeUser = 0;
            if (UserIsAdmin)
            {
                typeUser += 1;
            }
            if (UserIsTherapist)
            {
                typeUser += 2;
            }
            if (UserIsRecepcjonist)
            {
                typeUser += 4;
            }
            DBClass.openConnection();
            DBClass.sql = "Update users Set firstNameUser = '" + SelectedFirstName + "', lastNameUser = '" + SelectedLastName + "', " + "nameUser = '" + SelectedRow.NameUser + "', " +
                "typeUser = '" + typeUser + "', " + "passwordUser = '" + SelectedRow.PasswordUser + "'where IDUsers = '" + SelectedRow.IDUser + "'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;
            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);
            DataGrid refresh;
            DataGrid_Loaded();
        }
        public void AddRoleUser()
        {
            int typeUser = 0;
            if (UserIsAdminNew)
            {
                typeUser += 1;
            }
            if (UserIsTherapistNew)
            {
                typeUser += 2;
            }
            if (UserIsRecepcjonistNew)
            {
                typeUser += 4;
            }
            DBClass.openConnection();
            DBClass.sql = "Insert into users ( firstNameUser, lastNameUser, nameUser, typeUser, passwordUser ) values " +
                "('" + SelectedFirstName + "', '" + SelectedLastName + "', '" + SelectedRow.NameUser + "', '" + typeUser + "', '" + SelectedRow.PasswordUser + "')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;
            DBClass.da = new SqlDataAdapter(DBClass.cmd);
            DBClass.dt = new DataTable();
            DBClass.da.Fill(DBClass.dt);
            DataGrid refresh;
            DataGrid_Loaded();
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
