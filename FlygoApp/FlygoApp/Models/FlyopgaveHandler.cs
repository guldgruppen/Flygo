using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using FlygoApp.Persistency;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class FlyopgaveHandler : IHandler
    {

        public DtoFlyopgaveSingleton DtoFlyopgave;
        public DtoOpgaveArkivSingleton DtoOpgaveArkiv;
        public ObservableCollection<Flyopgave> Flyopgaver { get; set; }
        public ObservableCollection<OpgaveArkiv> OpgaveArkivs { get; set; }
        public IFactory FlyopgaveFactory { get; set; }
   
        public async void Add(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            try
            {
                bool match = Regex.IsMatch(nummer, @"^[a-zA-Z]{2}\d{3,4}$");
                if (!match)
                {
                    throw new ArgumentException("Flyopgavenummer skal starte 2 bogstaver og slutte med 4 cifre");
                }
                Flyopgave rute = FlyopgaveFactory.CreateFlyopgave(afgang, ankomst, flyid, hangarid, nummer);
                await DtoFlyopgave.PostFlyopgaver(rute);
                DtoFlyopgave.LoadFlyopgave();
                int id = DtoFlyopgave.FlyopgaveListe.Last().Id;

                OpgaveArkiv temp = new OpgaveArkiv() {FlyopgaveId = id};              
                await DtoOpgaveArkiv.PostOpgaveArkiv(temp);
                await new MessageDialog("Flyopgave er oprettet").ShowAsync();
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

        public FlyopgaveHandler()
        {
            DtoFlyopgave = DtoFlyopgaveSingleton.GetInstance();
            DtoOpgaveArkiv = DtoOpgaveArkivSingleton.GetInstance();
            FlyopgaveFactory = new FlyopgaveFactory();
            Flyopgaver = new ObservableCollection<Flyopgave>();
            OpgaveArkivs = new ObservableCollection<OpgaveArkiv>();
                                              
        }

        public void CheckEksisterendeFlyopgave(Flyopgave Flyopgave)
        {
            
        }

        public Flyopgave Get(int id)
        {
            return null;
        }

        public void Remove(Flyopgave Flyopgave)
        {
           
        }

        public void Update(Flyopgave Flyopgave)
        {
           
        }

        

        public void LoadDtoFlyopgaver()
        {
            foreach (var Flyopgave in DtoFlyopgave.FlyopgaveListe)
            {               
                Flyopgaver.Add(Flyopgave);

            }
            foreach (var opgaveArkiv in DtoOpgaveArkiv.OpgaveArkivListe)
            {
                OpgaveArkivs.Add(opgaveArkiv);
            }
            
        }

        

    }
}
