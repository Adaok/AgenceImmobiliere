using System;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oyosoft.AgenceImmobiliere.UniversalAppWin10.Converters
{
    class DisconnectionParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new Commands.NavigateToCommand(typeof(ConnexionPage), false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
