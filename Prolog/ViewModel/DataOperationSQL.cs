using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace Prolog.Model
{
    public class DataOperationSQL : INotifyPropertyChanged
    {
        DataTable dataTable = new DataTable();
        public DataTable LoadDataFromSQL()
        {
            var con = new SQLiteConnection("Data Source=PrologSQLLite.db;Version=3;New=False;Compress=True;");
            try
            {
                con.Open();
                SQLiteCommand sqlCmd = con.CreateCommand();
                sqlCmd.CommandText = "SELECT * FROM patientss ";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCmd.CommandText, con))
                {
                    dataAdapter.Fill(dataTable);
                    Console.WriteLine("Info z LoadData - " + dataAdapter);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return dataTable;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
