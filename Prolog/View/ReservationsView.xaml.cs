using Prolog.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Prolog.View
{
    /// <summary>
    /// Logika interakcji dla klasy ReservationsView.xaml
    /// </summary>
    public partial class ReservationsView : UserControl
    {
        ReservationsViewModel dateToSetReservation = new ReservationsViewModel();
        public ReservationsView()
        {
            InitializeComponent();
            //this.DataContext = new ReservationsViewModel();
        }

        private void sendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            string url = btn.CommandParameter as string;
            if (String.IsNullOrEmpty(url))
                return;
            try
            {
                // here i wish set the parameters of email in this way 
                // 1. mailto = url;
                // 2. subject = txtSubject.Text;
                // 3. body = txtBody.Text;
                Process.Start("mailto:test@gmail.com?subject=Software&body=test ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //EmailComposeTask emailComposeTask = new EmailComposeTask();
            //emailComposeTask.Subject = "message subject";
            //emailComposeTask.Body = "message body";
            //emailComposeTask.To = "johndoe@uni.gr";
            //emailComposeTask.Show();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            string chooseDate = null;
            DateTime? date = picker.SelectedDate;

            if (date == null)
            {
                chooseDate = "No date";
            }
            else
            {
                chooseDate = date.Value.ToString("yyyy-MM-dd");
                Console.WriteLine("Data wybrana - " + chooseDate);
                dateToSetReservation.SetDateForListReservation(chooseDate);
            }

            //reservationTable.ItemsSource = dateToSetReservation.
        }
    }
}
