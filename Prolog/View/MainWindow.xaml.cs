using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MahApps.Metro.Controls;
using Prolog.View;
using System.Windows.Controls.Ribbon;
using Prolog.ViewModel;
using System.ComponentModel;

namespace Prolog
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {

        PermissionsUsers permission = new PermissionsUsers();
        private bool userIsAdmininistrator = false;
        public string UserLoginName { get; set; }
        PreferencesSingleton userLoginName = new PreferencesSingleton();

        // Wyłącz dla Recepjonista, terapeuta - role - 
        public bool UserIsAdmininistrator
        {
            get
            {
                return userIsAdmininistrator;
            }
            set
            {
                //SetUserIsAdministrator();
                userIsAdmininistrator = value;
                //Console.WriteLine("User is - " + UserIsAdmininistrator);
                OnPropertyChanged("UserIsAdmininistrator");
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        private string visibilityDisablerTherapist = "Visible";
        // Wyłącz dla Terapeuta
        public string VisibilityDisablerTherapist
        {
            get
            {
                return visibilityDisablerTherapist;
            }
            set
            {
                //userIsAdmininistrator = value;
                //visibilityDisablerTherapist = "Collapsed";
                if (permission.getPermission() == 2)
                {
                    visibilityDisablerTherapist = Visibility.Collapsed.ToString();
                    Console.WriteLine("Wyłącz R - " + visibilityDisablerTherapist);
                }
                else
                {
                    visibilityDisablerTherapist = "Visible";
                    Console.WriteLine("Wyłącz R1 - " + visibilityDisablerTherapist + " ,per - " + permission.getPermission());
                }
                OnPropertyChanged("VisibilityDisablerTherapist");
            }
        }
        private string visibilityDisablerRecepcjonist = "Visible";

        public event PropertyChangedEventHandler PropertyChanged;

        // wyłącz dla Recepcjopnista
        public string VisibilityDisablerRecepcjonist
        {
            get
            {
                return visibilityDisablerRecepcjonist;
            }
            set
            {
                //userIsAdmininistrator = value;
                visibilityDisablerRecepcjonist = Visibility.Collapsed.ToString();
                OnPropertyChanged("VisibilityDisablerRecepcjonist");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new NavigationViewModel();
            UserLoginName = userLoginName.getUserLoginName();
            SetUserPermission();
            LabelUserLoginName.Content = userLoginName.getUserLoginName();
        }
        
        private void loadDictionarySpecjalizations_Click(object sender, RoutedEventArgs e)
        {
            SpecjalizationsButton.Visibility = Visibility.Collapsed;
            SpecjalizationsButton.Visibility = Visibility.Hidden;
        }

        private void OcenaEfektow_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Role - A - " + UserIsAdmininistrator + " R - " + VisibilityDisablerRecepcjonist + " T - " + VisibilityDisablerTherapist);
            //Specjalizations.Visibility = Visibility.Collapsed;
            //SpecjalizationsButton.IsEnabled = false;
            SetUserPermission();
            //LabelUserLoginName.Content = "Zalogowany: " + userLoginName.getUserLoginName();
            Console.WriteLine("Role - A - " + UserIsAdmininistrator + " R - " + VisibilityDisablerRecepcjonist + " T - " + VisibilityDisablerTherapist);
        }
        public void SetUserPermission()
        {
            Console.WriteLine("PO1bieramy perma - " + permission.getPermission() + " PObieramy admina - " + UserIsAdmininistrator);
                  
            if ((permission.getPermission() & 4) != 0)
            {
                userIsAdmininistrator = false;
                SpecjalizationsButton.IsEnabled = false;
                PatientButton.IsEnabled = false;
                TherapistsButton.IsEnabled = false;
                ParentsButton.IsEnabled = false;
                UsersButton.IsEnabled = false;
            }
            if ((permission.getPermission() & 2) != 0)
            {
                userIsAdmininistrator = false;
                SpecjalizationsButton.IsEnabled = false;
                PatientButton.IsEnabled = true;
                TherapistsButton.IsEnabled = false;
                ParentsButton.IsEnabled = true;
                UsersButton.IsEnabled = false;
                Console.WriteLine("PO2bieramy Rece - " + permission.getPermission() + " PObieramy rece - " + UserIsAdmininistrator);
            }
            if ((permission.getPermission() & 1) != 0)
            {
                userIsAdmininistrator = true;
                SpecjalizationsButton.IsEnabled = true;
                PatientButton.IsEnabled = true;
                TherapistsButton.IsEnabled = true;
                ParentsButton.IsEnabled = true;
                UsersButton.IsEnabled = true;
                Console.WriteLine("PO4bieramy Rece - " + permission.getPermission() + "PObieramy rece - " + PatientButton.IsEnabled);
            }
            LabelUserLoginName.Content = "Zalogowany: " + userLoginName.getUserLoginName(); ;
        }
    }
}
