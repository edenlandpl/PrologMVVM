using Prolog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prolog.Command
{
    class DeleteParentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ParentsViewModel vm;

        public DeleteParentCommand(ParentsViewModel VM)
        {
            vm = VM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.DeleteParent();
        }
    }
}
