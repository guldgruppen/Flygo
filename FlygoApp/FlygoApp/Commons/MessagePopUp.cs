using System;
using Windows.UI.Popups;

namespace FlygoApp.Commons
{
    public class MessagePopUp
    {
        public static async void YesNoMessageDialog(string message,Action action)
        {
            var myMessageDialog = new MessageDialog(message);
            myMessageDialog.Commands.Add(new UICommand("YES", command =>
            {
                action();
            }));
            myMessageDialog.Commands.Add(new UICommand("NO", command => { }));
            await myMessageDialog.ShowAsync();
        }
    }
}
