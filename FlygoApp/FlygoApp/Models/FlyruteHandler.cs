using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Commons;
using FlygoApp.Persistency;
using FlyGoWebService.Models;
using Newtonsoft.Json.Bson;

namespace FlygoApp.Models
{
    public class FlyruteHandler : IHandler
    {
        private ICommand _createFlyruteCommand;

        public DTOSingleton  DTO { get; set; }
        public ObservableCollection<FlyRute> Flyruter { get; set; }
        public ObservableCollection<OpgaveArkiv> OpgaveArkivs { get; set; }
        public IFactory FlyRuteFactory { get; set; }
   
        public async void Add(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            try
            {
                FlyRute rute = FlyRuteFactory.CreateFlyrute(afgang, ankomst, flyid, hangarid, nummer);
                DTO.PostFlyRuter(rute);
                DTO.Loadflyrute();
                int id = DTO.FlyruteListe.Last().Id;

                OpgaveArkiv temp = new OpgaveArkiv() {FlyRuteId = id};              
                DTO.PostOpgaveArkiv(temp);
            }
            catch (ArgumentException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            catch (IndexOutOfRangeException e)
            {              
                await new MessageDialog(e.Message).ShowAsync();
            }
            


        }

        public FlyruteHandler()
        {   
            DTO = DTOSingleton.GetInstance();
            FlyRuteFactory = new FlyRuteFactory();
            Flyruter = new ObservableCollection<FlyRute>();
            OpgaveArkivs = new ObservableCollection<OpgaveArkiv>();
                                              
        }

        public void CheckEksisterendeFlyrute(FlyRute flyrute)
        {
            
        }

        public FlyRute Get(int Id)
        {
            return null;
        }

        public void Remove(FlyRute flyrute)
        {
           
        }

        public void Update(FlyRute flyrute)
        {
           
        }

        

        public void LoadDTOFlyruter()
        {
            foreach (var flyrute in DTO.FlyruteListe)
            {               
                Flyruter.Add(flyrute);

            }
            foreach (var opgaveArkiv in DTO.OpgaveArkivListe)
            {
                OpgaveArkivs.Add(opgaveArkiv);
            }
            
        }

        

    }
}
