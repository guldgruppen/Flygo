using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Annotations;
using FlygoApp.Persistency;
using FlyGoWebService;

namespace FlygoApp.ViewModels
{
    public class BrugerDataViewModel : INotifyPropertyChanged
    {
        #region instance fields

        private DtoBrugerLoginSingleton _dtoBrugerLogin;
        #endregion
        #region Properties

        public ObservableCollection<BrugerLogIn> BrugerLogIns { get; set; } = new ObservableCollection<BrugerLogIn>();
        #endregion

        public BrugerDataViewModel()
        {
            _dtoBrugerLogin = DtoBrugerLoginSingleton.GetInstance();
        }
        #region Metoder

        public void LoadBrugerData()
        {
            
        }
        #endregion

        #region OnPropertyChanged region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
