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
    public class AtendentesController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Atendentes
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "AtendenteID" ? "id_desc" : "AtendenteID";
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LoginSortParm = sortOrder == "Login" ? "login_desc" : "Login";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var atendentes = from s in db.Atendente
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                atendentes = atendentes.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    atendentes = atendentes.OrderByDescending(s => s.Nome);
                    break;
                case "id_desc":
                    atendentes = atendentes.OrderByDescending(s => s.PessoaID);
                    break;
                case "AtendenteID":
                    atendentes = atendentes.OrderBy(s => s.PessoaID);
                    break;
                case "login_desc":
                    atendentes = atendentes.OrderByDescending(s => s.Login);
                    break;
                case "Login":
                    atendentes = atendentes.OrderBy(s => s.Login);
                    break;
                default:
                    atendentes = atendentes.OrderBy(s => s.Nome);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(atendentes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Atendentes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atendente atendente = await db.Atendente.Where(p => p.PessoaID == id).FirstAsync();
            if (atendente == null)
            {
                return HttpNotFound();
            }
            return View(atendente);
        }

        // GET: Atendentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Atendentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PessoaID,Nome,Login,Senha,Endereco,Nascimento,CPF,CEP,Numero,Cidade,Estado,Cargo")] Atendente atendente)
        {
            atendente.CargoID = db.Cargo.Where(s => s.Nome.Contains("Atendente")).First().CargoID;
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(atendente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(atendente);
        }

        // GET: Atendentes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atendente atendente = await db.Atendente.Where(p => p.PessoaID == id).FirstAsync();
            if (atendente == null)
            {
                return HttpNotFound();
            }
            return View(atendente);
        }

        // POST: Atendentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PessoaID,Nome,Endereco,Nascimento,CPF,CEP,Numero,Cidade,Estado,Cargo")] Atendente atendente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atendente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(atendente);
        }

        // GET: Atendentes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atendente atendente = await db.Atendente.Where(p => p.PessoaID == id).FirstAsync();
            if (atendente == null)
            {
                return HttpNotFound();
            }
            return View(atendente);
        }

        // POST: Atendentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Atendente atendente = await db.Atendente.Where(p => p.PessoaID == id).FirstAsync(); ;
            db.Pessoa.Remove(atendente);
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
