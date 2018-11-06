using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Paciente")]
    public class Paciente : Pessoa
    {
        public Paciente()
        {
            this.Consulta = new HashSet<Consulta>();
            this.Prontuario = new HashSet<Prontuario>();
        }
    
        public int PacienteID { get; set; }
        public int ConvenioID { get; set; }
    
        public virtual Convenio Convenio { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
