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

namespace Prolog.ViewModel.SubView
{
    /// <summary>
    /// Logika interakcji dla klasy AddTherapySubView.xaml
    /// </summary>
    public partial class AddTherapySubView
    {
        public AddTherapySubView()
        {
            InitializeComponent();
        }
        private void closeTherapyButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
