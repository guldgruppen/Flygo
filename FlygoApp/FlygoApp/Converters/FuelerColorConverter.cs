using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using FlygoApp.Models;
using FlyGoWebService.Models;

namespace FlygoApp.Converters
{
    public class FuelerColorConverter : IValueConverter
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
                Flyopgave rute = opg.Flyopgave;

                if (arkiv.Fuelers == DateTime.Parse("01-01-1995") && rute.Ankomst > DateTime.Now)
                    return white;
                if (arkiv.Fuelers == DateTime.Parse("01-01-1995"))
                    return red;
                if (rute.Ankomst > DateTime.Now)
                    return white;
                if (arkiv.Fuelers > rute.Afgang)
                    return yellow;
                if (arkiv.Fuelers < rute.Afgang)
                    return green;
                if (arkiv.Fuelers == null)
                    return white;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
