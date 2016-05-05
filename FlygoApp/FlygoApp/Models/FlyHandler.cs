using System.Collections.ObjectModel;
using FlygoApp.Persistency;

namespace FlygoApp.Models
{
    public class FlyHandler
    {
        public ObservableCollection<Fly> Fly { get; set; }

        private DtoFlySingleton _dtoFly = DtoFlySingleton.GetInstance();

        public FlyHandler()
        {
            Fly = new ObservableCollection<Fly>();
        }

        public void LoadDtoFly()
        {
            foreach (var f in _dtoFly.FlyListe)
            {
                Fly.Add(f);
            }
        }

    }
}
