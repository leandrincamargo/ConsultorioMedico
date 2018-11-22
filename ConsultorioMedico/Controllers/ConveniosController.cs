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
    public class ConveniosController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Convenios
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "ConvenioID" ? "id_desc" : "ConvenioID";
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

            var convenios = from s in db.Convenio
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                convenios = convenios.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    convenios = convenios.OrderByDescending(s => s.Nome);
                    break;
                case "id_desc":
                    convenios = convenios.OrderByDescending(s => s.ConvenioID);
                    break;
                case "ConvenioID":
                    convenios = convenios.OrderBy(s => s.ConvenioID);
                    break;
                default:
                    convenios = convenios.OrderBy(s => s.Nome);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(convenios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Convenios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convenio convenio = await db.Convenio.FindAsync(id);
            if (convenio == null)
            {
                return HttpNotFound();
            }
            return View(convenio);
        }

        // GET: Convenios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Convenios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ConvenioID,Nome")] Convenio convenio)
        {
            if (ModelState.IsValid)
            {
                db.Convenio.Add(convenio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(convenio);
        }

        // GET: Convenios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convenio convenio = await db.Convenio.FindAsync(id);
            if (convenio == null)
            {
                return HttpNotFound();
            }
            return View(convenio);
        }

        // POST: Convenios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ConvenioID,Nome")] Convenio convenio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convenio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(convenio);
        }

        // GET: Convenios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Convenio convenio = await db.Convenio.FindAsync(id);
            if (convenio == null)
            {
                return HttpNotFound();
            }
            return View(convenio);
        }

        // POST: Convenios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Convenio convenio = await db.Convenio.FindAsync(id);
            db.Convenio.Remove(convenio);
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
