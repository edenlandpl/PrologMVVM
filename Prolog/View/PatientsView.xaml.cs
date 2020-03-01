﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Logika interakcji dla klasy PatientsView.xaml
    /// </summary>
    public partial class PatientsView : UserControl
    {
        public PatientsView()
        {
            InitializeComponent();
        }

        private void printingPatient_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                // Create Grid panel.
                Grid grid = new Grid();

                // Define 5 auto-sized rows and columns.
                for (int i = 0; i < 5; i++)
                {
                    ColumnDefinition coldef = new ColumnDefinition();
                    coldef.Width = GridLength.Auto;
                    grid.ColumnDefinitions.Add(coldef);

                    RowDefinition rowdef = new RowDefinition();
                    rowdef.Height = GridLength.Auto;
                    grid.RowDefinitions.Add(rowdef);
                }

                // Give the Grid a gradient brush.
                grid.Background =
                    new LinearGradientBrush(Colors.Black, Colors.White,
                                            new Point(0, 0), new Point(1, 1));

                // Every program needs some randomness.
                Random rand = new Random();

                // Fill the Grid with 25 buttons.
                for (int i = 0; i < 25; i++)
                {
                    Button btn = new Button();
                    btn.FontSize = 12 + rand.Next(8);
                    btn.Content = "Button No. " + (i + 1);
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    btn.VerticalAlignment = VerticalAlignment.Center;
                    btn.Margin = new Thickness(6);
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, i % 5);
                    Grid.SetColumn(btn, i / 5);
                }

                // Size the Grid.
                grid.Measure(new Size(Double.PositiveInfinity,
                                      Double.PositiveInfinity));

                Size sizeGrid = grid.DesiredSize;

                // Determine point for centering Grid on page.
                Point ptGrid =
                    new Point((dlg.PrintableAreaWidth - sizeGrid.Width) / 2,
                              (dlg.PrintableAreaHeight - sizeGrid.Height) / 2);

                // Layout pass.
                grid.Arrange(new Rect(ptGrid, sizeGrid));

                //Now print it.
                if (dlg.ShowDialog() == true)
                {
                    dlg.PrintVisual(DanePacjenta, "Dane Pacjenta");
                }
               
            }
            //PrintDialog printDialog = new PrintDialog();
            //    //PageMediaSize pageSize = null;
            //    //bool bA4 = true;

            //    //if (bA4)
            //    //{
            //    //    pageSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLetter);
            //    //}
            //    //else
            //    //{
            //    //    pageSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            //    //}

            //     //printDialog.PrintTicket.PageMediaSize = pageSize;
            //if (printDialog.ShowDialog() == true)
            //{
            //    printDialog.PrintVisual(DanePacjenta, "Dane Pacjenta");
            //}
            //    //PrintDialog printDlg = new PrintDialog();
            //    //PatientsView uc = new PatientsView();
            //    //printDlg.PrintVisual(uc, "User Control Printing.");
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
