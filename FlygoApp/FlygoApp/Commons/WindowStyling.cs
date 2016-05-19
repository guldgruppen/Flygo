using Windows.UI;
using Windows.UI.ViewManagement;

namespace FlygoApp.Commons
{
    public class WindowStyling
    {
        //Bruges til at style vinduet appen bliver vist i
        public static void WindowAndTitleBarStyling()
        {

            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.BackgroundColor = Color.FromArgb(100, 34, 49, 63);
            view.TitleBar.ButtonBackgroundColor = Color.FromArgb(100, 34, 49, 63);
            view.TitleBar.ForegroundColor = Colors.WhiteSmoke;

        }
    }
}
