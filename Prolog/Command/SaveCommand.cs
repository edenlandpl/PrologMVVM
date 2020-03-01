using Prolog.ViewModel;
using Prolog.ViewModel.SubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog.Command
{
    class SaveCommand
    {
       
        public UpdatePatientSubViewModel vm { get; set; }

        public SaveCommand(UpdatePatientSubViewModel VM)
        {
            vm = VM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Save Execute");
            vm.SaveUpdatePatient();
        }
    }
}
