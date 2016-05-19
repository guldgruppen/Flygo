namespace FlygoApp.Models
{
    public class Hangar
    {
        public int Id { get; set; }
        public string Placering { get; set; }

        public Hangar(int id, string placering)
        {
            Id = id;
            Placering = placering;
        }

        public Hangar()
        {
            
        }

        public Hangar(string placering)
        {
            Placering = placering;
        }

        public override string ToString()
        {
            return $"{Placering}";
        }
    }
}
