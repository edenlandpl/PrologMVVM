using Prolog.ViewModel.SubViewModel;
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

namespace Prolog.ViewModel.SubView
{
    /// <summary>
    /// Logika interakcji dla klasy UpdatePatientSubView.xaml
    /// </summary>
    public partial class UpdatePatientSubView
    {
        UpdatePatientSubViewModel saveUpdatePatient = new UpdatePatientSubViewModel();
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
        public UpdatePatientSubView()
        {
            InitializeComponent();
            this.DataContext = this;                    // Set DataContext of MainWindow to itself in constructor of MainWindow to resolve binding, bez tego nie będzie BINDOWAĆ !!
            //this.DataContext = new UpdatePatientSubViewModel();
        }

        private void SavePatientButton_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Codzik bihand" + SelectedIDPatient + SelectedFirstName + SelectedLastName);
            saveUpdatePatient.SaveUpdatePatient();
        }

        private void closePatientButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
