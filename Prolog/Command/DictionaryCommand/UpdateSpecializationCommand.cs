﻿using Prolog.ViewModel.DictionariesViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prolog.Command.DictionaryCommand
{
    class UpdateSpecializationCommand : ICommand
    {
        public SpecialisationsDictionaryViewModel vm { get; set; }

        public UpdateSpecializationCommand(SpecialisationsDictionaryViewModel VM)
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
            vm.UpdateSpecialization();
        }
    }
}
