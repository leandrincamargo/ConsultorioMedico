using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConsultorioMedico.Models;

namespace ConsultorioMedico.Business
{
    public class ConsultaBusiness
    {
        public List<TimeSpan> Horarios { get; private set; }
        private DateTime _dia { get; set; }
        private int _medicoID { get; set; }
        private ConsultorioEntities db = new ConsultorioEntities();

        public ConsultaBusiness (DateTime dia, int medicoID)
        {
            Horarios = new List<TimeSpan>();
            _dia = dia;
            _medicoID = medicoID;
            ObtemHorarioMedico();
            Console.Write(Horarios);
        }

        private void ObtemHorarioMedico()
        {
            var horarioConsulta = db.Medico.Where(m => m.PessoaID == _medicoID).FirstOrDefault().horarioEntrada.TimeOfDay;
            var horarioSaida = db.Medico.Where(m => m.PessoaID == _medicoID).FirstOrDefault().horarioSaida;
            while (horarioConsulta.CompareTo(horarioSaida.TimeOfDay) < 0)
            {
                Horarios.Add(horarioConsulta);
                horarioConsulta = horarioConsulta.Add(new TimeSpan(0, 30, 0));
            }
            var horariosMarcados = db.Consulta.Where(c => c.MedicoID == _medicoID && c.dataConsulta == _dia).Select(c => c.horarioConsulta).ToList();
            foreach (var item in horariosMarcados)
            {
                Horarios.Remove(item.TimeOfDay);
            }
        }
    }
}