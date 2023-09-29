namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Pathimg { get; set; }
        public float Price { get; set; }
        public Pizza(int id, string name, string description, string pathimg, float price)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Pathimg = pathimg;
            this.Price = price;
        }
    }
}
