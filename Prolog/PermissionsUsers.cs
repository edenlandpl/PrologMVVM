using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolog
{
    public class PermissionsUsers : INotifyPropertyChanged
    {
        public static int PermissionUser { get; set; }
        public static void setPermission(string roleUser)
        {
            PermissionUser += Int32.Parse(roleUser);
            Console.WriteLine("Rola ustawiona - " + PermissionUser);
        }
        public int getPermission()
        {            
            Console.WriteLine("Rola pobrana - " + PermissionUser);
            return PermissionUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            private void INotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
    }
}
