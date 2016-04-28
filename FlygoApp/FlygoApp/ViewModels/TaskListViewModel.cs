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
<<<<<<< HEAD

=======
        
>>>>>>> refs/remotes/origin/UdviklerBranch
        public DateTimeOffset Now { get; set; }
        #endregion

        public FlyHandler FlyHandler { get; set; }

        public HangarHandler HangarHandler { get; set; }

        public TaskListViewModel()
        {
<<<<<<< HEAD
          FlyHandler = new FlyHandler();  
            FlyHandler.LoadDtoFly();

            HangarHandler = new HangarHandler();
            HangarHandler.LoadDtoHangar();
=======
           
>>>>>>> refs/remotes/origin/UdviklerBranch
        }
        
        


<<<<<<< HEAD
=======
       
       
>>>>>>> refs/remotes/origin/UdviklerBranch
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
