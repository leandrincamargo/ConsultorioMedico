using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Prontuario")]
    public class Prontuario
    {
        public int ProntuarioID { get; set; }
        public string Informacoes { get; set; }
        public int MedicoID { get; set; }
        public int PacienteID { get; set; }
        public int ExameID { get; set; }
        public int ConsultaID { get; set; }
    
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Exame Exame { get; set; }
        public virtual Consulta Consulta { get; set; }
    }
}
