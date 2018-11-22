using ConsultorioMedico.Models;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace ConsultorioMedico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        class Dpc : DayPilotCalendar
        {
            protected override void OnInit(InitArgs e)
            {
                Update(CallBackUpdateType.Full);
            }
            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                var db = new ConsultorioEntities();
                Events = from ev in db.Consulta
                         join med in db.Medico on ev.MedicoID equals med.PessoaID
                         where !((ev.dataConsulta <= VisibleStart) || (ev.dataConsulta >= VisibleEnd))
                         select new { ConsultaID = ev.ConsultaID, Nome = med.Nome, dataConsulta = ev.dataConsulta };
                
                DataIdField = "ConsultaID";
                DataTextField = "Nome";
                DataStartField = "dataConsulta";
                DataEndField = "dataConsulta";
            }
        }
    }
}