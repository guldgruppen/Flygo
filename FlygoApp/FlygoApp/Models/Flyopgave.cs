using FlygoApp.Models;

namespace FlyGoWebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;    

    [Table("Flyopgave")]
    public partial class Flyopgave
    {
        private DateTime _ankomst;
        private DateTime _afgang;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flyopgave()
        {
            OpgaveArkiv = new HashSet<OpgaveArkiv>();
        }

        public Flyopgave(int flyId, int hangarId, string flyopgaveNummer,DateTime ankomst,DateTime afgang)
        {
            CheckAfgangAnkomst(afgang, ankomst);
            CheckFlyId(flyId);
            CheckHangarId(hangarId);
            CheckFlyopgaveNummer(flyopgaveNummer);
            FlyId = flyId;
            HangarId = hangarId;
            FlyopgaveNummer = flyopgaveNummer;
            Ankomst = ankomst;
            Afgang = afgang;

        }

        public int Id { get; set; }
        public string AfgangSomText { get; set; }
        public string AnkomstSomText { get; set; }
        public DateTime Ankomst
        {
            get { return _ankomst; }
            set
            {
                _ankomst = value;
                AnkomstSomText = Ankomst.ToString("MM/dd/yyyy HH:mm");
            }
        }
        public DateTime Afgang
        {
            get { return _afgang; }
            set
            {
                _afgang = value;
                AfgangSomText = Afgang.ToString("MM/dd/yyyy HH:mm");
            }
        }
        public int FlyId { get; set; }
        public int HangarId { get; set; }

        [StringLength(50)]
        public string FlyopgaveNummer { get; set; }

        public virtual Fly Fly { get; set; }

        public virtual Hangar Hangar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpgaveArkiv> OpgaveArkiv { get; set; }
        public void CheckFlyopgaveNummer(string nummer)
        {
            if (String.IsNullOrEmpty(nummer))
            {
                throw new ArgumentException("Venligst indtast et flyrute nummer");
            }
        }
        public void CheckFlyId(int id)
        {
            if (id < 0)
            {
                throw new IndexOutOfRangeException("Du mangler at vælge et fly");
            }
        }
        public void CheckHangarId(int id)
        {
            if (id < 0)
            {
                throw new IndexOutOfRangeException("Du mangler at vælge en hangar");
            }
        }


        public void CheckAfgangAnkomst(DateTime afgang, DateTime ankomst)
        {

            if (ankomst > afgang)
            {
                throw new ArgumentException("Ankomsttid skal forekomme før afgangstid");
            }
            if (ankomst < DateTime.Now || afgang < DateTime.Now)
            {
                throw new ArgumentException("Ankomst og afgang skal forekomme efter dags dato");
            }
        }
        public override string ToString()
        {
            return $"Id: {Id}, Ankomst: {Ankomst}, Afgang: {Afgang}, FlyRuteNummer: {FlyopgaveNummer}, Fly: {Fly}, Hangar: {Hangar}";
        }
    }
}
