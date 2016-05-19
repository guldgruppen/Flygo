using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FlygoApp.Commons
{
    public class NavigationService : INavigationService
    {
        //Denne klasse bruges til at navigere fra view til view
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }
    }
}
