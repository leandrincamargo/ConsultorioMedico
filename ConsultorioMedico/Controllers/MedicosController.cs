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

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var medicos = from s in db.Medico.Include(p => p.Cidade).Include(p => p.Estado).Include(p => p.Especialidade)
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
                    medicos = medicos.OrderByDescending(s => s.FuncionarioID);
                    break;
                case "MedicoID":
                    medicos = medicos.OrderBy(s => s.FuncionarioID);
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
            Medico medico = await db.Medico.FindAsync(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.CidadeID = new SelectList(db.Cidade, "CidadeID", "Nome");
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "UF");
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PessoaID,Nome,Endereco,Nascimento,CPF,CEP,Numero,CidadeID,EstadoID,FuncionarioID,Cargo,CRM,EspecialidadeID,horarioAtendimento")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Pessoa.Add(medico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CidadeID = new SelectList(db.Cidade, "CidadeID", "Nome", medico.CidadeID);
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "UF", medico.EstadoID);
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
            Medico medico = await db.Medico.FindAsync(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeID = new SelectList(db.Cidade, "CidadeID", "Nome", medico.CidadeID);
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "UF", medico.EstadoID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PessoaID,Nome,Endereco,Nascimento,CPF,CEP,Numero,CidadeID,EstadoID,FuncionarioID,Cargo,CRM,EspecialidadeID,horarioAtendimento")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeID = new SelectList(db.Cidade, "CidadeID", "Nome", medico.CidadeID);
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "UF", medico.EstadoID);
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
            Medico medico = await db.Medico.FindAsync(id);
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
            Medico medico = await db.Medico.FindAsync(id);
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

        public JsonResult ObterCidades(int id)
        {
            //Retorna o valor em JSON
            return Json(db.Cidade.Where(e => e.EstadoID == id).Select(e => new { Text = e.Nome, Value = e.CidadeID }), JsonRequestBehavior.AllowGet);
        }
    }
}
