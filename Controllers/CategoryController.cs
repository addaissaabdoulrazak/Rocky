using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext _db;
        //injection de Dependance 
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> ObjectList = _db.category.ToList();
            return View(ObjectList);
        } 

    // GET - CREATE
        [HttpGet]
        public IActionResult Create()
        {
            //on recupère un model Vide et on Le passe A la vue Pour Le remplir 
            Category category = new Category();
            return View(category);



 /*REMARQUE : Comme que tout est Vide on peut ne pas instancier le Model de Maniere explicite
  * normalement le Controleur accède au modèle lorsque une mis-a-jour de celui ci doit être effcetuer.
  * Par contre a ce niveau nous avons Besoin de passer un model Vide donc aucune mis-a-jour est a effectuer ici et donc
  * on peut s'empacer du Controleur car je Rappel que la vue a la possiblité d'acceder au Model sans passer par le Controler/
 */
        }  




        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            /*faut jamais faire Confiance A un Utilisateur ainsi qu'a sa saisie. Effectuer tjr une Vérification*/
            if(ModelState.IsValid)
            {
                _db.category.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET - EDIT
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Category category = new Category();
            category = _db.category.FirstOrDefault(u => u.Id == id);
            if(category==null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST - EDIT
        //[ActionName("Update")] est utiliser pour les Web Api
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = new Category();
            category = _db.category.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST - DELETE
        [HttpPost]
        public IActionResult DeleteComfir(int id)
        {
            if(id!=0)
            {
                _db.category.Remove(_db.category.Find(id));
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
