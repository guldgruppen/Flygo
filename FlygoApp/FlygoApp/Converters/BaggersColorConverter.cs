using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using FlygoApp.Models;
using FlyGoWebService.Models;

namespace FlygoApp.Converters
{
    public class BaggersColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //#FFFF3333 - red
            //#FF51EC24 - green
            //#FFFFE43A - yellow
            //#FFFFFFFF - white
            SolidColorBrush green = new SolidColorBrush(Colors.Green);
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
            SolidColorBrush white = new SolidColorBrush(Colors.GhostWhite);

            OpgaveAdapter opg = (OpgaveAdapter)value;

            if (opg != null)
            {
                OpgaveArkiv arkiv = opg.OpgaveArkiv;
                FlyRute rute = opg.FlyRute;

                if (rute.Ankomst > DateTime.Now)
                    return white;
                if (arkiv.Baggers > rute.Afgang)
                    return yellow;
                if (arkiv.Baggers < rute.Afgang)
                    return green;
                if (arkiv.Baggers == null)
                    return red;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
