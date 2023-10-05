using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Inserire obbligatoriamente il nome della pizza")]
        [MaxLength(50 , ErrorMessage = "Inserire un nome adeguato")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Inserire obbligatoriamente una descrizione")]
        [MaxLength(200, ErrorMessage =" La descrizione inserita supera il limite di caratteri")]
        public string Description { get; set; }
        public string Pathimg { get; set; }
        [Range(4 , 50)]
        public float Price { get; set; }

        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public List<Gusto>? Gusti { get; set; }

        //Costruttore vuoto 
        public Pizza() { }
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
