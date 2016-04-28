using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Persistency;

namespace FlygoApp.Models
{
    public class FlyHandler
    {
        public ObservableCollection<Fly> Fly { get; set; }

        public DTOSingleton Dto = DTOSingleton.GetInstance();


        public FlyHandler()
        {
            Fly = new ObservableCollection<Fly>();
        }

        public void LoadDtoFly()
        {
            foreach (var f in Dto.FlyListe)
            {
                Fly.Add(f);
            }
        }

    }
}
