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
    public class ConsultasController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Consultas
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "ConsultaID" ? "id_desc" : "ConsultaID";
            ViewBag.MedicoSortParm = sortOrder == "Medico" ? "medico_desc" : "Medico";
            ViewBag.ConvenioSortParm = sortOrder == "Convenio" ? "convenio_desc" : "Convenio";
            ViewBag.AtendenteSortParm = sortOrder == "Atendente" ? "atendente_desc" : "Atendente";
            ViewBag.DataSortParm = sortOrder == "Data" ? "data_desc" : "Data";
            ViewBag.EspecialidadeSortParm = sortOrder == "Especialidade" ? "especialidade_desc" : "Especialidade";
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
            
            var consultas = from s in db.Consulta.Include(c => c.Medico).Include(c => c.Paciente).Include(c => c.Especialidade).Include(c => c.Convenio).Include(c => c.Atendente)
            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                consultas = consultas.Where(s => s.Paciente.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    consultas = consultas.OrderByDescending(s => s.Paciente.Nome);
                    break;
                case "id_desc":
                    consultas = consultas.OrderByDescending(s => s.ConsultaID);
                    break;
                case "ConsultaID":
                    consultas = consultas.OrderBy(s => s.ConsultaID);
                    break;
                case "medico_desc":
                    consultas = consultas.OrderByDescending(s => s.Medico.Nome);
                    break;
                case "Medico":
                    consultas = consultas.OrderBy(s => s.Medico.Nome);
                    break;
                case "convenio_desc":
                    consultas = consultas.OrderByDescending(s => s.Convenio.Nome);
                    break;
                case "Convenio":
                    consultas = consultas.OrderBy(s => s.Convenio.Nome);
                    break;
                case "atendente_desc":
                    consultas = consultas.OrderByDescending(s => s.Atendente.Nome);
                    break;
                case "Atendente":
                    consultas = consultas.OrderBy(s => s.Atendente.Nome);
                    break;
                case "data_desc":
                    consultas = consultas.OrderByDescending(s => s.dataConsulta);
                    break;
                case "Data":
                    consultas = consultas.OrderBy(s => s.dataConsulta);
                    break;
                case "especialidade_desc":
                    consultas = consultas.OrderByDescending(s => s.Especialidade.Nome);
                    break;
                case "Especialidade":
                    consultas = consultas.OrderBy(s => s.Especialidade.Nome);
                    break;
                default:
                    consultas = consultas.OrderBy(s => s.Paciente.Nome);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(consultas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Consultas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = await db.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.MedicoID = new SelectList(db.Medico, "FuncionarioID", "Nome");
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "Nome");
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome");
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome");
            ViewBag.AtendenteID = new SelectList(db.Atendente, "FuncionarioID", "Nome");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ConsultaID,dataConsulta,MedicoID,PacienteID,EspecialidadeID,ConvenioID,AtendenteID,ProntuarioID")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Consulta.Add(consulta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicoID = new SelectList(db.Medico, "FuncionarioID", "Nome", consulta.MedicoID);
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "Nome", consulta.PacienteID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", consulta.EspecialidadeID);
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome", consulta.ConvenioID);
            ViewBag.AtendenteID = new SelectList(db.Atendente, "FuncionarioID", "Nome", consulta.AtendenteID);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = await db.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicoID = new SelectList(db.Medico, "FuncionarioID", "Nome", consulta.MedicoID);
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "Nome", consulta.PacienteID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", consulta.EspecialidadeID);
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome", consulta.ConvenioID);
            ViewBag.AtendenteID = new SelectList(db.Atendente, "FuncionarioID", "Nome", consulta.AtendenteID);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ConsultaID,dataConsulta,MedicoID,PacienteID,EspecialidadeID,ConvenioID,AtendenteID,ProntuarioID")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consulta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicoID = new SelectList(db.Medico, "FuncionarioID", "Nome", consulta.MedicoID);
            ViewBag.PacienteID = new SelectList(db.Paciente, "PacienteID", "Nome", consulta.PacienteID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidade, "EspecialidadeID", "Nome", consulta.EspecialidadeID);
            ViewBag.ConvenioID = new SelectList(db.Convenio, "ConvenioID", "Nome", consulta.ConvenioID);
            ViewBag.AtendenteID = new SelectList(db.Atendente, "FuncionarioID", "Nome", consulta.AtendenteID);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = await db.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Consulta consulta = await db.Consulta.FindAsync(id);
            db.Consulta.Remove(consulta);
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

        public JsonResult ObterMedico(int id)
        {
            //Retorna o valor em JSON
            return Json(db.Medico.Where(e => e.EspecialidadeID == id).Select(e => new { Text = e.Nome, Value = e.FuncionarioID }), JsonRequestBehavior.AllowGet);
        }
    }
}
