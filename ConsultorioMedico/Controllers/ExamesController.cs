using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsultorioMedico.Models;
using PagedList;

namespace ConsultorioMedico.Controllers
{
    public class ExamesController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Exames
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "ExameID" ? "id_desc" : "ExameID";
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var exames = from s in db.Exame
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                exames = exames.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    exames = exames.OrderByDescending(s => s.Nome);
                    break;
                case "id_desc":
                    exames = exames.OrderByDescending(s => s.ExameID);
                    break;
                case "EspecialidadeID":
                    exames = exames.OrderBy(s => s.ExameID);
                    break;
                default:
                    exames = exames.OrderBy(s => s.Nome);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(exames.ToPagedList(pageNumber, pageSize));
        }

        // GET: Exames/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = await db.Exame.FindAsync(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // GET: Exames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExameID,Nome")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                db.Exame.Add(exame);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(exame);
        }

        // GET: Exames/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = await db.Exame.FindAsync(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExameID,Nome")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exame).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(exame);
        }

        // GET: Exames/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = await db.Exame.FindAsync(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exame exame = await db.Exame.FindAsync(id);
            db.Exame.Remove(exame);
            await db.SaveChangesAsync();
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
