using Prolog.Command;
using Prolog.ViewModel;
using Prolog.ViewModel.DictionariesViewModel;
using Prolog.ViewModel.SubView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prolog.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
         public ICommand UpdateCommand { get; set; }
        public ICommand PatientsCommand { get; set; }
        public ICommand ReservationsCommand { get; set; }
        public ICommand ParentsCommand { get; set; }
        public ICommand TherapistsCommand { get; set; }
        public ICommand SpecializationsDictionatyViewCommand { get; set; }
        public ICommand UsersCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public string UserLoginName { get; set; }
        PreferencesSingleton userLoginName = new PreferencesSingleton();

        private object selectedView;
        //public static bool p = "false";

        public object SelectedView
        {
            get { return selectedView; }
            set { selectedView = value; OnPropertyChanged("SelectedView"); }
        }
        
        
        public NavigationViewModel()
        {
            UpdateCommand = new BaseCommand(OpenUpdatePatience);
            PatientsCommand = new BaseCommand(OpenPatients);
            ReservationsCommand = new BaseCommand(OpenReservations);
            ParentsCommand = new BaseCommand(OpenParents);
            TherapistsCommand = new BaseCommand(OpenTherapists);
            SpecializationsDictionatyViewCommand = new BaseCommand(OpenSpecializationsDictionary);
            UsersCommand = new BaseCommand(OpenUsers);
            UserCommand = new BaseCommand(OpenUser);
            //UserLoginName = userLoginName.getUserLoginName();
            //SetUserIsAdministrator();
        }        

        private void OpenReservations(object obj)
        {
            SelectedView = new ReservationsViewModel();
        }

        private void OpenPatients(object obj)
        {
            SelectedView = new PatientsViewModel();
        }
        private void OpenUpdatePatience(object obj)
        {
            UpdatePatientSubView updatePatientSubView = new UpdatePatientSubView();
            updatePatientSubView.ShowDialog();
        }
        private void OpenParents(object obj)
        {
            SelectedView = new ParentsViewModel();
        }
        private void OpenTherapists(object obj)
        {
            SelectedView = new TherapistsViewModel();
        }
        private void OpenSpecializationsDictionary(object obj)
        {
            SelectedView = new SpecialisationsDictionaryViewModel();
        }
        private void OpenUsers(object obj)
        {
            SelectedView = new UsersViewModel();
        }
        private void OpenUser(object obj)
        {
            SelectedView = new UserViewModel();
        }
        //public event PropertyChangedEventHandler UpdateValue;
        public event PropertyChangedEventHandler PropertyChanged;
        

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
                //Console.WriteLine("PObieramy ON - " + permission.getPermission() + userIsAdmininistrator);
                //if (permission.getPermission() == "1")
                //{
                //    userIsAdmininistrator = true;
                //    Console.WriteLine("PObieramy ON1 - " + permission.getPermission() + userIsAdmininistrator);
                //}
                //else
                //{
                //    userIsAdmininistrator = false;
                //    Console.WriteLine("PObieramy ON2 - " + permission.getPermission() + userIsAdmininistrator);
                //}
                //Console.WriteLine(permission.getPermission() == "1");
            }
        }
    }
}
