using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    public class ApplicationTypeController : Controller
    {
        //injection de dependance au niveau du controleur
        private readonly ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> ObjectList = _db.ApplicationTypes.ToList();
            return View(ObjectList);
        }
        // GET - CREATE
        [HttpGet]
        public IActionResult Create()
        {
            //Voir Commentaire Create De Category Controleur
            return View();
            /*REMARQUE : Comme que tout est Vide on peut ne pas instancier le Model de Maniere explicite
             * normalement le Controleur accède au modèle lorsque une mis-a-jour de celui ci doit être effcetuer.
             * Par contre a ce niveau nous avons Besoin de passer un model Vide donc aucune mis-a-jour est a effectuer ici et donc
             * on peut s'empacer du Controleur car je Rappel que la vue a la possiblité d'acceder au Model sans passer par le Controler/
            */
        }




        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType application)
        {
            /*faut jamais faire Confiance A un Utilisateur ainsi qu'a sa saisie. Effectuer tjr une Vérification*/
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Add(application);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(application);
        }

        //GET - EDIT
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ApplicationType application = new ApplicationType();
            application = _db.ApplicationTypes.FirstOrDefault(u => u.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST - EDIT
        //[ActionName("Update")] est utiliser pour les Web Api
        [HttpPost]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Update(obj);
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
            ApplicationType application = new ApplicationType();
            application = _db.ApplicationTypes.FirstOrDefault(u => u.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST - DELETE
        [HttpPost]
        public IActionResult DeleteComfir(int id)
        {
            if (id != 0)
            {
                _db.ApplicationTypes.Remove(_db.ApplicationTypes.Find(id));
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
