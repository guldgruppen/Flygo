using System.Collections.ObjectModel;
using FlygoApp.Persistency;

namespace FlygoApp.Models
{
    public class HangarHandler
    {
        public  ObservableCollection<Hangar> Hangar { get; set; }

        private DtoHangarSingleton _dtoHangar;

        public HangarHandler()
        {
            _dtoHangar = DtoHangarSingleton.GetInstance;
           Hangar= new ObservableCollection<Hangar>();
        }

        //Loader data ind fra hangar
        public void LoadDtoHangar()
        {
            foreach (var h in _dtoHangar.HangarListe)
            {
                Hangar.Add(h);
            }
        }

    }
}
