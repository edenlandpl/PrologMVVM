﻿using Prolog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prolog.Command
{
    class UpdateRoleUserCommand : ICommand
    {
        public UsersViewModel vm { get; set; }

        public UpdateRoleUserCommand(UsersViewModel VM)
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
            Console.WriteLine("JEstem ...Buugiem ");

            vm.UpdateRoleUser();
        }
    }
}
