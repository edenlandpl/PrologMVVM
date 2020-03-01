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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prolog.View.SubView
{
    /// <summary>
    /// Logika interakcji dla klasy AddTherapistSubView.xaml
    /// </summary>
    public partial class AddTherapistSubView
    {
        AddTherapistSubViewModel saveUpdateTherapist = new AddTherapistSubViewModel();
        public AddTherapistSubView()
        {
            InitializeComponent();
        }
        private void SaveAddTherapistButton_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Codzik bihand" + SelectedIDPatient + SelectedFirstName + SelectedLastName);
            saveUpdateTherapist.SaveAddTherapist();
        }
        private void closeTherapistButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
