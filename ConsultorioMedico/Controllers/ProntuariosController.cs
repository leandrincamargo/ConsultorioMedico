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

namespace ConsultorioMedico.Controllers
{
    public class ProntuariosController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Prontuarios
        public async Task<ActionResult> Index()
        {
            var prontuario = db.Prontuario.Include(p => p.Consulta).Include(p => p.Exame).Include(p => p.Medico).Include(p => p.Paciente);
            return View(await prontuario.ToListAsync());
        }

        // GET: Prontuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prontuario prontuario = await db.Prontuario.FindAsync(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            return View(prontuario);
        }

        // GET: Prontuarios/Create
        public ActionResult Create()
        {
            ViewBag.ConsultaID = new SelectList(db.Consulta, "ConsultaID", "ConsultaID");
            ViewBag.ExameID = new SelectList(db.Exame, "ExameID", "Nome");
            ViewBag.MedicoID = new SelectList(db.Pessoa, "PessoaID", "Login");
            ViewBag.PacienteID = new SelectList(db.Pessoa, "PessoaID", "Login");
            return View();
        }

        // POST: Prontuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProntuarioID,Informacoes,MedicoID,PacienteID,ExameID,ConsultaID")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                db.Prontuario.Add(prontuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ConsultaID = new SelectList(db.Consulta, "ConsultaID", "ConsultaID", prontuario.ConsultaID);
            ViewBag.ExameID = new SelectList(db.Exame, "ExameID", "Nome", prontuario.ExameID);
            ViewBag.MedicoID = new SelectList(db.Pessoa, "PessoaID", "Login", prontuario.MedicoID);
            ViewBag.PacienteID = new SelectList(db.Pessoa, "PessoaID", "Login", prontuario.PacienteID);
            return View(prontuario);
        }

        // GET: Prontuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prontuario prontuario = await db.Prontuario.FindAsync(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsultaID = new SelectList(db.Consulta, "ConsultaID", "ConsultaID", prontuario.ConsultaID);
            ViewBag.ExameID = new SelectList(db.Exame, "ExameID", "Nome", prontuario.ExameID);
            ViewBag.MedicoID = new SelectList(db.Pessoa, "PessoaID", "Login", prontuario.MedicoID);
            ViewBag.PacienteID = new SelectList(db.Pessoa, "PessoaID", "Login", prontuario.PacienteID);
            return View(prontuario);
        }

        // POST: Prontuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProntuarioID,Informacoes,MedicoID,PacienteID,ExameID,ConsultaID")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prontuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultaID = new SelectList(db.Consulta, "ConsultaID", "ConsultaID", prontuario.ConsultaID);
            ViewBag.ExameID = new SelectList(db.Exame, "ExameID", "Nome", prontuario.ExameID);
            ViewBag.MedicoID = new SelectList(db.Pessoa, "PessoaID", "Login", prontuario.MedicoID);
            ViewBag.PacienteID = new SelectList(db.Pessoa, "PessoaID", "Login", prontuario.PacienteID);
            return View(prontuario);
        }

        // GET: Prontuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prontuario prontuario = await db.Prontuario.FindAsync(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            return View(prontuario);
        }

        // POST: Prontuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prontuario prontuario = await db.Prontuario.FindAsync(id);
            db.Prontuario.Remove(prontuario);
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
