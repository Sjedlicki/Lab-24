using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab_24.Models;

namespace Lab_24.Controllers
{
    public class GameDBsController : Controller
    {
        private ItemDBEntities db = new ItemDBEntities();

        // GET: GameDBs
        public ActionResult Index()
        {
            return View(db.GameDBs.ToList());
        }

        // GET: GameDBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDB gameDB = db.GameDBs.Find(id);
            if (gameDB == null)
            {
                return HttpNotFound();
            }
            return View(gameDB);
        }

        // GET: GameDBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameDBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GamesID,Title,Rating,Release_Date")] GameDB gameDB)
        {
            if (ModelState.IsValid)
            {
                foreach(GameDB game in db.GameDBs)
                {
                    if(game.Title != gameDB.Title)
                    {
                        db.GameDBs.Add(gameDB);                    
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Game Already Exists!";
                        return View("Create");
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gameDB);
        }

        // GET: GameDBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDB gameDB = db.GameDBs.Find(id);
            if (gameDB == null)
            {
                return HttpNotFound();
            }
            return View(gameDB);
        }

        // POST: GameDBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GamesID,Title,Rating,Release_Date")] GameDB gameDB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gameDB);
        }

        // GET: GameDBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDB gameDB = db.GameDBs.Find(id);
            if (gameDB == null)
            {
                return HttpNotFound();
            }
            return View(gameDB);
        }

        // POST: GameDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameDB gameDB = db.GameDBs.Find(id);
            db.GameDBs.Remove(gameDB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
