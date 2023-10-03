using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizze = db.pizze.ToList<Pizza>();

                return View("Index", pizze);
            }
        }
        public IActionResult Dettagli(int id )
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaTrovata = db.pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaTrovata == null)
                {
                    return View("NotFound");
                }else
                {
                    return View("DettagliPizza", pizzaTrovata);
                }
            }
        }
        [HttpGet]
        public IActionResult CreatePizza()
        {
            return View("CreatePizza");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePizza( Pizza nuovaPizza)
        {
            if( !ModelState.IsValid ) 
            {
                return View("CreatePizza", nuovaPizza);

            }
            using (PizzaContext db = new PizzaContext())
            {
                db.pizze.Add(nuovaPizza);
                db.SaveChanges();

                return View ("Index" , db.pizze.ToList<Pizza>());
            }
        }
        [HttpGet]
        public IActionResult AggiornaPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaEditare = db.pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaEditare == null)
                {
                    return NotFound($"La pizza con {id} non possibile modificarla!");
                }
                else
                {
                    return View("UpdatePizza", pizzaDaEditare);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AggiornaPizza(int id, Pizza pizzaModificata)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdatePizza", pizzaModificata);
            }
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaEditare = db.pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaDaEditare != null)
                {
                    pizzaDaEditare.Name = pizzaModificata.Name;
                    pizzaDaEditare.Description = pizzaModificata.Description;
                    pizzaDaEditare.Pathimg = pizzaModificata.Pathimg;
                    pizzaDaEditare.Price = pizzaModificata.Price;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("NotFound");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancellaPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaCancellare = db.pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaCancellare != null)
                {
                    db.pizze.Remove(pizzaDaCancellare);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("NotFound");
                }
            }
        }

    }
}
