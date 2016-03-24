using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlygoApp.Annotations;

namespace FlygoApp.Models
{
    public class Flyrute : INotifyPropertyChanged
    {
        private string _timePart;
        private string _datePart;
        public string FlyruteNr { get; set; }
        public string Flytype { get; set; }
        public DateTime Ankomst { get; set; }
        public DateTime Afgang { get; set; }
        public string DestinationFra { get; set; }
        public string DestinationTil { get; set; }
        public string TimePart
        {
            get { return _timePart; }
            set
            {
                _timePart = value;
                OnPropertyChanged();
            }
        }

        public string DatePart
        {
            get { return _datePart; }
            set
            {
                _datePart = value;
                OnPropertyChanged();
            }
        }

        public Flyrute(string flyruteNr, string flytype, DateTime ankomst, DateTime afgang, string destinationFra, string destinationTil)
        {
            CheckFlyruteNr(flyruteNr);
            CheckFlyrutetype(flytype);
            CheckDestination(destinationFra);
            CheckDestination(destinationTil);
            CheckDate(ankomst);
            CheckDate(afgang);
            FlyruteNr = flyruteNr;
            Flytype = flytype;
            Ankomst = ankomst;
            Afgang = afgang;
            DestinationFra = destinationFra;
            DestinationTil = destinationTil;
        }

        public void CheckFlyruteNr(string flyrutenr)
        {
                if(String.IsNullOrEmpty(flyrutenr) || flyrutenr.Length > 6)
                throw new ArgumentException("Flyrute nummeret eksisterer ikke");
        }
        public void CheckFlyrutetype(string flytype)
        {
            if (String.IsNullOrEmpty(flytype))
                throw new ArgumentException("Flytypen eksisterer ikke");
        }
        public void CheckDestination(string destination)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentException("Destinationen eksisterer ikke");
        }
        public void CheckDate(DateTime dt)
        {          
                int result = DateTime.Compare(DateTime.Now, dt.Date);              
                if (result > 0)
                {
                    throw new ArgumentException("Afgang og ankomst skal forekomme i efter idag");
                }            
        }
        public override string ToString()
        {
            return $"FlyruteNr: {FlyruteNr}, Flytype: {Flytype}, Ankomst: {Ankomst}, Afgang: {Afgang}, DestinationFra: {DestinationFra}, DestinationTil: {DestinationTil}";
        }

        #region Notify Changed Region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
