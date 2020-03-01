using Prolog.SQLConnection;
using Prolog.View;
using Prolog.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Prolog.PermissionsUsers;

namespace Prolog
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login
    {
        [Flags]
        enum UsersAttributes
        {
            None = 0, Administrator = 1, Therapist = 2, Recepcjonist = 4, Użytkownik = 8
        }
        
        int[] usersAttributesTemp = new int[99];
        PermissionsUsers roleUser = new PermissionsUsers();
        PreferencesSingleton userLoginName = new PreferencesSingleton();
        MainWindow visibilityByRoles = new MainWindow();
        public Login()
        {
            InitializeComponent();
            //UsersAttributes allowedAttributes = UsersAttributes.Administrator | UsersAttributes.Recepcjonist | UsersAttributes.Therapist;
        }       
        string[] data = new string[999];

        MainWindow mainWindow = new MainWindow();
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Wpisz email";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Wpisz właściwy email";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;

                DBClass.openConnection();
                DBClass.sql = "Select typeUser from users where nameUser='" + email + "'  and passwordUser='" + password + "'";
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
                        if (reader.FieldCount> 0 )
                        {
                            data[0] = reader.GetValue(0).ToString();
                            Console.WriteLine("Taki typeUser - " + data[0]);
                            userLoginName.setUserLoginName(email);
                            UsersAttributes allowedAttributes = UsersAttributes.Administrator | UsersAttributes.Recepcjonist | UsersAttributes.Therapist;
                            Console.WriteLine("Allowed 1 " + allowedAttributes + "intek " + (UsersAttributes)(int.Parse(data[0])));
                            allowedAttributes = UsersAttributes.Administrator & (UsersAttributes)(int.Parse(data[0]));
                            Console.WriteLine("Allowed 2" + allowedAttributes);
                            if (allowedAttributes > 0)
                            {
                                Console.WriteLine("Jestem adminkiem");
                                setPermission("1");
                            }
                            allowedAttributes = UsersAttributes.Therapist & (UsersAttributes)(int.Parse(data[0]));
                            if (allowedAttributes > 0)
                            {
                                Console.WriteLine("Jestem tera");
                                setPermission("2");
                                //visibilityByRoles.VisibilityDisablerTherapist = "Collapsed";
                                //visibilityByRoles.SetUserIsAdministrator(false);
                            }
                            allowedAttributes = UsersAttributes.Recepcjonist & (UsersAttributes)(int.Parse(data[0]));
                            if (allowedAttributes > 0)
                            {
                                Console.WriteLine("Jestem receptą");
                                setPermission("4");

                            }
                            mainWindow.Show();
                            Close();
                        }
                        else
                        {
                            errormessage.Text = "Przepraszamy. Wpisz właściwego użytkownika / hasło";
                        }
                        
                    }
                    errormessage.Text = "Wpisz właściwego użytkownika / hasło";
                }
                DBClass.closeConnection();                
            }
        }
        private void Furtka_Click(object sender, RoutedEventArgs e)
        {
            setPermission("1");
            //mainWindow = new Prolog.MainWindow();
            mainWindow.Show();
            visibilityByRoles.SetUserPermission();
            Close();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

    }
}
