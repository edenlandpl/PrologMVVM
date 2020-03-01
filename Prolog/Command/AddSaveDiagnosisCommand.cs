﻿using Prolog.ViewModel;
using Prolog.ViewModel.SubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prolog.Command
{
    class AddSaveDiagnosisCommand : ICommand
    {
        public AddDiagnosisSubViewModel vm { get; set; }

        public AddSaveDiagnosisCommand(AddDiagnosisSubViewModel VM)
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
            vm.AddSaveDiagnosis();
        }
    }
}
