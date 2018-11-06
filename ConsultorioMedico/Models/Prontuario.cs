using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Prontuario")]
    public class Prontuario
    {
        [Key]
        public int ProntuarioID { get; set; }
        [Required]
        public string Informacoes { get; set; }
        [Required]
        [Display(Name = "Medico")]
        public int MedicoID { get; set; }
        [Required]
        [Display(Name = "Paciente")]
        public int PacienteID { get; set; }
        [Required]
        [Display(Name = "Exame")]
        public int ExameID { get; set; }
        [Required]
        [Display(Name = "Consulta")]
        public int ConsultaID { get; set; }
    
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Exame Exame { get; set; }
        public virtual Consulta Consulta { get; set; }
    }
}
