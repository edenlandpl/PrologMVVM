using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Prolog.ViewModel.SubViewModel;

namespace Prolog.ViewModel.SubView
{
    /// <summary>
    /// Logika interakcji dla klasy AddPatientSubView.xaml
    /// </summary>
    public partial class AddPatientSubView 
    {
        public AddPatientSubView()
        {
            InitializeComponent();
            this.DataContext = this;                    // Set DataContext of MainWindow to itself in constructor of MainWindow to resolve binding, bez tego nie będzie BINDOWAĆ !!
            //this.DataContext = new AddPatientSubViewModel();
        }

        AddPatientSubViewModel saveUpdatePatient = new AddPatientSubViewModel();
        private string _IDPatience;
        private string _firstNamePatience;
        private string _lastNamePatience;
        public string SelectedIDPatient
        {
            get { return _IDPatience; }
            set
            {
                if (value != _IDPatience)
                {
                    _IDPatience = value;
                    OnPropertyChanged("SelectedIDPatient");
                }
            }
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        public string SelectedFirstName
        {
            get { return _firstNamePatience; }
            set
            {
                if (value != _firstNamePatience)
                {
                    _firstNamePatience = value;
                    OnPropertyChanged("SelectedFirstName");
                }
            }
        }
        public string SelectedLastName
        {
            get { return _lastNamePatience; }
            set
            {
                if (value != _lastNamePatience)
                {
                    _lastNamePatience = value;
                    OnPropertyChanged("SelectedLastName");
                }
            }
        }
       
        private void SaveAddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Codzik bihand" + SelectedIDPatient + SelectedFirstName + SelectedLastName);
            saveUpdatePatient.SaveAddPatient();
        }
        private void closePatientButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
