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
    /// Logika interakcji dla klasy UpdateTherapistSubView.xaml
    /// </summary>
    public partial class UpdateTherapistSubView 
    {
        UpdateTherapistSubViewModel saveUpdateTherapist = new UpdateTherapistSubViewModel();
        public UpdateTherapistSubView()
        {
            InitializeComponent();
            this.DataContext = this;                    // Set DataContext of MainWindow to itself in constructor of MainWindow to resolve binding, bez tego nie będzie BINDOWAĆ !!
        }

        private void saveTherapistButton_Click(object sender, RoutedEventArgs e)
        {
            saveUpdateTherapist.SaveUpdateTherapist();
        }
        private void closeActivityButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
