﻿using System;
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
    public class ProntuariosController : Controller
    {
        private ConsultorioEntities db = new ConsultorioEntities();

        // GET: Prontuarios
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "ProntuarioID" ? "id_desc" : "ProntuarioID";
            ViewBag.MedicoSortParm = sortOrder == "Medico" ? "medico_desc" : "Medico";
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

            var prontuarios = from s in db.Prontuario.Include(c => c.Consulta)
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                prontuarios = prontuarios.Where(s => s.Consulta.Paciente.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    prontuarios = prontuarios.OrderByDescending(s => s.Consulta.Paciente.Nome);
                    break;
                case "id_desc":
                    prontuarios = prontuarios.OrderByDescending(s => s.ProntuarioID);
                    break;
                case "ProntuarioID":
                    prontuarios = prontuarios.OrderBy(s => s.ProntuarioID);
                    break;
                case "medico_desc":
                    prontuarios = prontuarios.OrderByDescending(s => s.Consulta.Medico.Nome);
                    break;
                case "Medico":
                    prontuarios = prontuarios.OrderBy(s => s.Consulta.Medico.Nome);
                    break;
                default:
                    prontuarios = prontuarios.OrderBy(s => s.Consulta.Paciente.Nome);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(prontuarios.ToPagedList(pageNumber, pageSize));
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
            return View();
        }


        public PartialViewResult DetalhesConsulta(int id)
        {
            return PartialView("~/Views/Prontuarios/PartialConsulta.cshtml", db.Consulta.Find(id));
        }

        // POST: Prontuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProntuarioID,Informacoes,ExameID,ConsultaID")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                db.Prontuario.Add(prontuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ConsultaID = new SelectList(db.Consulta, "ConsultaID", "ConsultaID", prontuario.ConsultaID);
            ViewBag.ExameID = new SelectList(db.Exame, "ExameID", "Nome", prontuario.ExameID);
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
