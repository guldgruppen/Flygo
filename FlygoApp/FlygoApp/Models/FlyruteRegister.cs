using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.ViewModels;

namespace FlygoApp.Models
{
    public class FlyruteRegister
    {
        public ObservableCollection<Flyrute> Flyruter { get; set; }
        public TaskListViewModel TaskListViewModel { get; set; }
        public FlyruteRegister(TaskListViewModel tlvm)
        {
            TaskListViewModel = tlvm;
            Flyruter = new ObservableCollection<Flyrute>();
            Flyruter.Add(new Flyrute("SK400","AirBus 323",DateTime.Now,DateTime.Now, "København","Madrid"));
            Flyruter.Add(new Flyrute("SK400", "AirBus 323", DateTime.Now, DateTime.Now, "København", "Madrid"));
            Flyruter.Add(new Flyrute("SK400", "AirBus 323", DateTime.Now, DateTime.Now, "København", "Madrid"));
            Flyruter.Add(new Flyrute("SK400", "AirBus 323", DateTime.Now, DateTime.Now, "København", "Madrid"));

        }

        public void AddFlyrute(Flyrute flyrute)
        {
            Flyruter.Add(flyrute);
        }

        
    }
}
