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
    public class PacientesController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Pacientes
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "PacienteID" ? "id_desc" : "PacienteID";
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

            var pacientes = from s in db.Paciente.Include(p => p.Convenio)
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pacientes = pacientes.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pacientes = pacientes.OrderByDescending(s => s.Nome);
                    break;
                case "id_desc":
                    pacientes = pacientes.OrderByDescending(s => s.PacienteID);
                    break;
                case "PacienteID":
                    pacientes = pacientes.OrderBy(s => s.PacienteID);
                    break;
                case "login_desc":
                    pacientes = pacientes.OrderByDescending(s => s.Login);
                    break;
                case "Login":
                    pacientes = pacientes.OrderBy(s => s.Login);
                    break;
                default:
                    pacientes = pacientes.OrderBy(s => s.Nome);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(pacientes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Pacientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = await db.Paciente.Where(p => p.PacienteID == id).FirstAsync();
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PessoaID,Login,Senha,Nome,Endereco,Nascimento,CPF,CEP,Numero,Cidade,Estado,PacienteID,ConvenioID")] Paciente paciente)
        {
            paciente.CargoID = db.Cargo.Where(s => s.Nome.Contains("Paciente")).First().CargoID;
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(paciente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome", paciente.ConvenioID);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = await db.Paciente.Where(p => p.PacienteID == id).FirstAsync();
            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome", paciente.ConvenioID);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PessoaID,Nome,Endereco,Nascimento,CPF,CEP,Numero,Cidade,Estado,PacienteID,ConvenioID")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome", paciente.ConvenioID);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = await db.Paciente.Where(p => p.PacienteID == id).FirstAsync();
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Paciente paciente = await db.Paciente.Where(p => p.PacienteID == id).FirstAsync();
            db.Pessoa.Remove(paciente);
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
