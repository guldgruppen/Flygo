using System.Collections.ObjectModel;
using FlygoApp.Persistency;

namespace FlygoApp.Models
{
    public class FlyHandler
    {
        public ObservableCollection<Fly> Fly { get; set; }

        private readonly DtoFlySingleton _dtoFly = DtoFlySingleton.GetInstance;

        public FlyHandler()
        {
            Fly = new ObservableCollection<Fly>();
        }


        //Loader data fra Dto og indsætter flyene i ObservableCollection
        public void LoadDtoFly()
        {
            foreach (var f in _dtoFly.FlyListe)
            {
                Fly.Add(f);
            }
        }

    }
}
