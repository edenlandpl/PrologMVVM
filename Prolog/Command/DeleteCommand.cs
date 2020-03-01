using Prolog.ViewModel;
using Prolog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prolog.Command
{
    class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public static string[] selectedPatient = new string[9];
        private PatientsViewModel getter;        //Deklarujemy zmienną typu PatientsViewModel

        public PatientsViewModel vm;

        public DeleteCommand(PatientsViewModel VM)
        {
            vm = VM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // wyciągnięcie danych z tabeli - patients
            getter = new PatientsViewModel();        //Deklarujemy zmienną typu PatientsView
            selectedPatient = getter.InfoPatientGetter();
            //SelectedIDPatient = selectedPatient[1];
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć pacjenta " + selectedPatient[1] + " " + selectedPatient[2], "Potwierdź usunięcie użytkownika", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    vm.Delete();
                    break;
                case MessageBoxResult.Cancel:
                    
                    break;
            }
        }
    }
}
