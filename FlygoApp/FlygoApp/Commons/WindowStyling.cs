using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace FlygoApp.Commons
{
    public class WindowStyling
    {
        public static void WindowAndTitleBarStyling()
        {

            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.BackgroundColor = Color.FromArgb(100, 60, 60, 60);
            view.TitleBar.ButtonBackgroundColor = Color.FromArgb(100, 60, 60, 60);
            view.TitleBar.ForegroundColor = Colors.WhiteSmoke;

        }
    }
}
