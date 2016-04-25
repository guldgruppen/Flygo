using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlygoApp.Persistency;

namespace FlygoApp.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        #region Instance Fields
        #endregion
        #region Properties         
        public FlyruteRegister FlyruteRegisterProp { get; set; }
        public DateTimeOffset Now { get; set; }
        #endregion
        public TaskListViewModel()
        {
            FlyruteRegisterProp = new FlyruteRegister(this);
            FlyruteRegisterProp.AddFlyrute(new Flyrute("SK400", "Airbus 323", DateTime.Now, DateTime.Now, "København", "Stockholm"));           
            ObservableCollection.Add(new WorkerTest("Rengøring","Færdig"));
            ObservableCollection.Add(new WorkerTest("Caters", "Ikke begyndt"));
            ObservableCollection.Add(new WorkerTest("Mekaniker", "Forsinket"));
            ObservableCollection.Add(new WorkerTest("Fuelers", "Fejl"));
            ObservableCollection.Add(new WorkerTest("Crew", "Færdig"));
            ObservableCollection.Add(new WorkerTest("Baggagers", "Færdig"));
        }

        public ObservableCollection<WorkerTest> ObservableCollection { get; set; } = new ObservableCollection<WorkerTest>();
       
        #region Metoder       
        #endregion
        #region NotifyChange Region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
