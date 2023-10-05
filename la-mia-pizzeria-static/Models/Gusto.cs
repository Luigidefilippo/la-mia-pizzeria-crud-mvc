namespace la_mia_pizzeria_static.Models
{
    public class Gusto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Gusto() { }

        public List<Pizza> Pizze { get; set; }
    }
}
