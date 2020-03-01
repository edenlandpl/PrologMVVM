using Prolog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prolog.Command
{
    class DeleteTherapistCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TherapistsViewModel vm;
        private TherapistsViewModel getter;        //Deklarujemy zmienną typu TherapistsViewModel
        public static string[] selectedTherapist = new string[9];
        public DeleteTherapistCommand(TherapistsViewModel VM)
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
            getter = new TherapistsViewModel();        //Deklarujemy zmienną typu PatientsView
            selectedTherapist = getter.InfoTherapistGetter();
            //SelectedIDPatient = selectedPatient[1];
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć dane terapeuty " + selectedTherapist[1] + " " + selectedTherapist[2], "Potwierdź usunięcie danych terapeuty", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    vm.DeleteTherapist();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }

            
        }
    }
}
