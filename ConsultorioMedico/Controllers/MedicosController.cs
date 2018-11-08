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
    public class MedicosController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Medicos

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "MedicoID" ? "id_desc" : "MedicoID";
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LoginSortParm = sortOrder == "Login" ? "login_desc" : "Login";
            ViewBag.EspecialidadeSortParm = sortOrder == "Especialidade" ? "especialidade_desc" : "Especialidade";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var medicos = from s in db.Medico.Include(p => p.Especialidade)
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                medicos = medicos.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    medicos = medicos.OrderByDescending(s => s.Nome);
                    break;
                case "id_desc":
                    medicos = medicos.OrderByDescending(s => s.PessoaID);
                    break;
                case "MedicoID":
                    medicos = medicos.OrderBy(s => s.PessoaID);
                    break;
                case "login_desc":
                    medicos = medicos.OrderByDescending(s => s.Login);
                    break;
                case "Login":
                    medicos = medicos.OrderBy(s => s.Login);
                    break;
                case "especialidade_desc":
                    medicos = medicos.OrderByDescending(s => s.Especialidade.Nome);
                    break;
                case "Especialidade":
                    medicos = medicos.OrderBy(s => s.Especialidade.Nome);
                    break;
                default:
                    medicos = medicos.OrderBy(s => s.Nome);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(medicos.ToPagedList(pageNumber, pageSize));
        }
        // GET: Medicos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = await db.Medico.Where(p => p.PessoaID == id).FirstAsync();
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PessoaID,Nome,Login,Senha,Endereco,Nascimento,CPF,CEP,Numero,Cidade,Estado,Cargo,CRM,EspecialidadeID,horarioEntrada,horarioSaida")] Medico medico)
        {
            medico.CargoID = db.Cargo.Where(s => s.Nome.Contains("Medico")).First().CargoID;
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(medico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = await db.Medico.Where(p => p.PessoaID == id).FirstAsync();
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PessoaID,Nome,Endereco,Nascimento,CPF,CEP,Numero,Cidade,Estado,Cargo,CRM,EspecialidadeID,horarioAtendimento")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = await db.Medico.Where(p => p.PessoaID == id).FirstAsync();
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medico medico = await db.Medico.Where(p => p.PessoaID == id).FirstAsync();
            db.Pessoa.Remove(medico);
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
