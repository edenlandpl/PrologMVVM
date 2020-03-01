using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Prolog.Converters
{
    class ReservationToColorConverter : IValueConverter
    {
        string[] colorBackground = new string[999];
        private int counterColors;
        public int CounterColors
        {
            get { return counterColors; }
            set
            {
                counterColors = value;
            }
        }
        string tempReservationName = "";

        public void setCounterColors()
        {
            if (counterColors >= 5)
            {
                counterColors = 0;
            }
            counterColors++;
        }

        public ReservationToColorConverter()
        {
            //Default Colors
            colorBackground[0] = "LightGray";
            colorBackground[1] = "CornflowerBlue";
            colorBackground[2] = "SteelBlue";
            colorBackground[3] = "DeepSkyBlue";
            colorBackground[4] = "PowderBlue";
            colorBackground[5] = "SkyBlue";
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (counterColors >=6)
            {
                counterColors = 0;
            }            
            var reservationName = value as string;            
            if (reservationName != tempReservationName)
            {
                setCounterColors();
            }
            tempReservationName = reservationName;
            return colorBackground[counterColors];

            // wersja z rozpoznawaniem treści wpisu
            //switch (reservationName)
            //{
            //    case "Chef":
            //        return colorBackground[counterColors++];
            //    case "Bartolini Bartłomiej":
            //        return colorBackground[4];
            //    default:
            //        return colorBackground[5];
            //}
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
