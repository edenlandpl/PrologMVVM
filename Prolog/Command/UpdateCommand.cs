﻿using Prolog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prolog.Command
{
    class UpdateCommand : ICommand
    {
        public PatientsViewModel vm { get; set; }

        public UpdateCommand(PatientsViewModel VM)
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
            vm.UpdatePatient();
        }
    }
}
