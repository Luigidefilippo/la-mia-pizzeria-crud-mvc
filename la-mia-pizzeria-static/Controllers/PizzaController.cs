﻿using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private PizzaContext _myDb;
        public PizzaController(PizzaContext db)
        {
            _myDb = db;
        }
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizze = db.Pizze.Include(pizza => pizza.Categoria).ToList<Pizza>();

                return View("Index", pizze);
            }
        }
        public IActionResult Dettagli(int id )
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaTrovata = db.Pizze.Where(pizza=>pizza.Id == id).Include(pizza => pizza.Categoria).FirstOrDefault();
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
            List<Categoria> categorie = _myDb.Categorie.ToList();
            PizzaFormModel model = new PizzaFormModel { Pizza = new Pizza(), Categorie = categorie };

            return View("CreatePizza" , model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePizza( PizzaFormModel data)
        {
            if( !ModelState.IsValid ) 
            {
                List<Categoria> categorie = _myDb.Categorie.ToList();
                data.Categorie = categorie;
                return View("CreatePizza", data);

            }
            using (PizzaContext db = new PizzaContext())
            {
                _myDb.Pizze.Add(data.Pizza);
                _myDb.SaveChanges();

                return View ("Index" , db.Pizze.ToList<Pizza>());
            }
        }
        [HttpGet]
        public IActionResult AggiornaPizza(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaEditare = _myDb.Pizze.Where(pizza =>  pizza.Id == id).FirstOrDefault();

                if (pizzaDaEditare == null)
                {
                    return NotFound($"La pizza con {id} non possibile modificarla!");
                }
                else
                {
                    List<Categoria> categorie = _myDb.Categorie.ToList();

                    PizzaFormModel model = new PizzaFormModel { Pizza = pizzaDaEditare , Categorie = categorie };

                    return View("UpdatePizza", model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AggiornaPizza(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Categoria> categorie = _myDb.Categorie.ToList();
                data.Categorie = categorie;
                return View("UpdatePizza", data);
            }
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaEditare = _myDb.Pizze.Find(id);
                if (pizzaDaEditare != null)
                {
                    EntityEntry<Pizza> entityEntry = _myDb.Entry(pizzaDaEditare);
                    entityEntry.CurrentValues.SetValues(data.Pizza);
                    _myDb.SaveChanges();
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
                Pizza? pizzaDaCancellare = _myDb.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaCancellare != null)
                {
                    _myDb.Pizze.Remove(pizzaDaCancellare);
                    _myDb.SaveChanges();
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
