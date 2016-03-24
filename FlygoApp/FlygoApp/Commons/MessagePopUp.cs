using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlygoApp.Commons
{
    public class MessagePopUp
    {
        public static async void YesNoMessageDialog(string message,Action action)
        {
            var MyMessageDialog = new MessageDialog(message);
            MyMessageDialog.Commands.Add(new UICommand("YES", command =>
            {
                action();
            }));
            MyMessageDialog.Commands.Add(new UICommand("NO", command => { }));
            await MyMessageDialog.ShowAsync();
        }
    }
}
