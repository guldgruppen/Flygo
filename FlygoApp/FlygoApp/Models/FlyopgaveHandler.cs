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
   
        //Kaldes for at oprette et flyopgave.
        public async void Add(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            try
            {
                //Kontrollere om flyopgavenummer har 2 bogstaver og 3 til 4 tal
                bool match = Regex.IsMatch(nummer, @"^[a-zA-Z]{2}\d{3,4}$");
                if (!match)
                {
                    throw new ArgumentException("Flyopgavenummer skal starte 2 bogstaver og slutte med 4 cifre");
                }

                //Hvor flyopgave objektet bliver oprettet.
                Flyopgave rute = FlyopgaveFactory.CreateFlyopgave(afgang, ankomst, flyid, hangarid, nummer);
                
                //indsætter i databasen
                await DtoFlyopgave.PostFlyopgaver(rute);
                //Loader flyopgaverne igen.
                DtoFlyopgave.LoadFlyopgave();
                //Udvinder id fra flyopgaven til opgavearkiv
                int id = DtoFlyopgave.FlyopgaveListe.Last().Id;
                //Opretter et opgavearkiv objekt hvor flyopgaveid er baseret på den nylig oprettede flyopgave
                OpgaveArkiv temp = new OpgaveArkiv() {FlyopgaveId = id};
                
                //indsætter opgavearkiv i databasen
                await DtoOpgaveArkiv.PostOpgaveArkiv(temp);
                //Fortæller brugeren, at flyopgave er oprettet
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

        public void CheckEksisterendeFlyopgave(Flyopgave flyopgave)
        {
            
        }

        public Flyopgave Get(int id)
        {
            return null;
        }

        public void Remove(Flyopgave flyopgave)
        {
           
        }

        public void Update(Flyopgave flyopgave)
        {
           
        }

        
        //Loader data ind fra flyopgaver og opgavearkiv
        public void LoadDtoFlyopgaver()
        {
            foreach (var flyopgave in DtoFlyopgave.FlyopgaveListe)
            {               
                Flyopgaver.Add(flyopgave);

            }
            foreach (var opgaveArkiv in DtoOpgaveArkiv.OpgaveArkivListe)
            {
                OpgaveArkivs.Add(opgaveArkiv);
            }
            
        }

        

    }
}
