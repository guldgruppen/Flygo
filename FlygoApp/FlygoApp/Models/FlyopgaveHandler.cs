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
                CheckEksisterendeFlyopgave(nummer);
                //Hvor flyopgave objektet bliver oprettet.
                Flyopgave rute = FlyopgaveFactory.CreateFlyopgave(afgang, ankomst, flyid, hangarid, nummer);

                //indsætter i databasen
                await DtoFlyopgave.PostFlyopgaver(rute);

                //Loader flyopgaverne igen.
                await DtoFlyopgave.LoadFlyopgave();

                //Udvinder id fra flyopgaven til opgavearkiv
                int id = DtoFlyopgave.FlyopgaveListe.Last().Id;

                //Opretter et opgavearkiv objekt hvor flyopgaveid er baseret på den nylig oprettede flyopgave
                OpgaveArkiv temp = new OpgaveArkiv() {FlyopgaveId = id};

                //indsætter opgavearkiv i databasen
                await DtoOpgaveArkiv.PostOpgaveArkiv(temp);
                await DtoOpgaveArkiv.LoadOpgaveArkiv();

                await new MessageDialog("Flyopgave oprettet").ShowAsync();
            }
            catch (ArgumentException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            catch (IndexOutOfRangeException e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            

        }

        public FlyopgaveHandler()
        {
            DtoFlyopgave = DtoFlyopgaveSingleton.GetInstance;
            DtoOpgaveArkiv = DtoOpgaveArkivSingleton.GetInstance;
            FlyopgaveFactory = new FlyopgaveFactory();
            Flyopgaver = new ObservableCollection<Flyopgave>();
            OpgaveArkivs = new ObservableCollection<OpgaveArkiv>();
            
                                              
        }

        public void CheckEksisterendeFlyopgave(string flyopgaveNummer)
        {
            if (DtoFlyopgave.FlyopgaveListe.Exists(x => x.FlyopgaveNummer.Equals(flyopgaveNummer)))
            {
                throw new ArgumentException("Flyrutenummer eksisterer i forvejen");
            }
        }

        public Flyopgave Get(int id)
        {
            return null;
        }

        public void Remove(Flyopgave flyopgave)
        {
            Flyopgaver.Remove(flyopgave);
        }

        public void Update(Flyopgave flyopgave)
        {
           
        }

        
        //Loader data ind fra flyopgaver og opgavearkiv
        public void LoadDtoFlyopgaver()
        {
            foreach (var flyopgave in DtoFlyopgave.FlyopgaveListe.OrderBy((flyopgave => flyopgave.Ankomst)))
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
