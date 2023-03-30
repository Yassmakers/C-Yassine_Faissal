using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

// Deze class implementeert de "IValueConverter" interface om conversie mogelijk te maken tussen string en int waarden

namespace C_Yassine_Faissal.Helpers
{


    // Dit is de definitie van de class genaamd "StringToIntConverter"
    // Deze class implementeert de "IValueConverter" interface
    public class StringToIntConverter : IValueConverter
    {

        // Dit is de implementatie van de "Convert" methode van de "IValueConverter" interface
        // Deze methode wordt gebruikt om een object van het ene type naar het andere type te converteren
        // In dit geval wordt de waarde omgezet van een string naar een int
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }


        // Dit is de implementatie van de "ConvertBack" methode van de "IValueConverter" interface
        // Deze methode wordt gebruikt om een object van het ene type naar het andere type te converteren
        // In dit geval wordt de waarde omgezet van een int naar een string
        // Als de conversie niet mogelijk is, wordt de waarde 0 geretourneerd
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int result))
            {
                return result;
            }
            return 0;
        }
    }
}

