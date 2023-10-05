using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="inserire un nome per la categoria")]
        [StringLength(50, ErrorMessage ="il nome da della categoria deve essere reale")]

        public string Name { get; set; }

        public List<Pizza> Pizze { get; set; }

        public Categoria() { }

        public Categoria(int id, string name)
        {
            Id = id;
            Name = name;

        }
    }
}
