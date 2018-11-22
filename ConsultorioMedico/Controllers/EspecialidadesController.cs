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
    public class EspecialidadesController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Especialidades
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "EspecialidadeID" ? "id_desc" : "EspecialidadeID";
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

            var especialidades = from s in db.Especialidade
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                especialidades = especialidades.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    especialidades = especialidades.OrderByDescending(s => s.Nome);
                    break;
                case "id_desc":
                    especialidades = especialidades.OrderByDescending(s => s.EspecialidadeID);
                    break;
                case "EspecialidadeID":
                    especialidades = especialidades.OrderBy(s => s.EspecialidadeID);
                    break;
                default:
                    especialidades = especialidades.OrderBy(s => s.Nome);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(especialidades.ToPagedList(pageNumber, pageSize));
        }

        // GET: Especialidades/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await db.Especialidade.FindAsync(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // GET: Especialidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especialidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EspecialidadeID,Nome")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Especialidade.Add(especialidade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        // GET: Especialidades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await db.Especialidade.FindAsync(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EspecialidadeID,Nome")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        // GET: Especialidades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await db.Especialidade.FindAsync(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Especialidade especialidade = await db.Especialidade.FindAsync(id);
            db.Especialidade.Remove(especialidade);
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
