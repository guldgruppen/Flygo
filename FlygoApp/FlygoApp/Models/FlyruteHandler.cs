using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using FlygoApp.Persistency;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class FlyruteHandler : IHandler
    {

        public DtoFlyruteSingleton DtoFlyrute;
        public DtoOpgaveArkivSingleton DtoOpgaveArkiv;
        public ObservableCollection<FlyRute> Flyruter { get; set; }
        public ObservableCollection<OpgaveArkiv> OpgaveArkivs { get; set; }
        public IFactory FlyRuteFactory { get; set; }
   
        public async void Add(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            try
            {
                bool match = Regex.IsMatch(nummer, @"^[a-zA-Z]{2}\d{3,4}$");
                if (!match)
                {
                    throw new ArgumentException("Flyrutenummer skal starte 2 bogstaver og slutte med 4 cifre");
                }
                FlyRute rute = FlyRuteFactory.CreateFlyrute(afgang, ankomst, flyid, hangarid, nummer);
                await DtoFlyrute.PostFlyRuter(rute);
                DtoFlyrute.Loadflyrute();
                int id = DtoFlyrute.FlyruteListe.Last().Id;

                OpgaveArkiv temp = new OpgaveArkiv() {FlyRuteId = id};              
                await DtoOpgaveArkiv.PostOpgaveArkiv(temp);
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
            DtoFlyrute = DtoFlyruteSingleton.GetInstance();
            DtoOpgaveArkiv = DtoOpgaveArkivSingleton.GetInstance();
            FlyRuteFactory = new FlyRuteFactory();
            Flyruter = new ObservableCollection<FlyRute>();
            OpgaveArkivs = new ObservableCollection<OpgaveArkiv>();
                                              
        }

        public void CheckEksisterendeFlyrute(FlyRute flyrute)
        {
            
        }

        public FlyRute Get(int id)
        {
            return null;
        }

        public void Remove(FlyRute flyrute)
        {
           
        }

        public void Update(FlyRute flyrute)
        {
           
        }

        

        public void LoadDtoFlyruter()
        {
            foreach (var flyrute in DtoFlyrute.FlyruteListe)
            {               
                Flyruter.Add(flyrute);

            }
            foreach (var opgaveArkiv in DtoOpgaveArkiv.OpgaveArkivListe)
            {
                OpgaveArkivs.Add(opgaveArkiv);
            }
            
        }

        

    }
}
