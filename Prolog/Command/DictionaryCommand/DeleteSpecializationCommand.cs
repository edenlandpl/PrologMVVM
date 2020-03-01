using Prolog.ViewModel.DictionariesViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prolog.Command.DictionaryCommand
{
    class DeleteSpecializationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public SpecialisationsDictionaryViewModel vm;

        public DeleteSpecializationCommand(SpecialisationsDictionaryViewModel VM)
        {
            vm = VM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.DeleteSpecialization();
        }
    }
}
