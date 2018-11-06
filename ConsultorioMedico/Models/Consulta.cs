using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{

    [Table("Consulta")]
    public class Consulta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consulta()
        {
            this.Prontuario = new HashSet<Prontuario>();
        }

        [Key]
        public int ConsultaID { get; set; }
        [Required]
        [Display(Name = "Paciente")]
        public int PacienteID { get; set; }
        [Required]
        [Display(Name = "Especialidade")]
        public int EspecialidadeID { get; set; }
        [Required]
        [Display(Name = "Medico")]
        public int MedicoID { get; set; }
        [Required]
        [Display(Name = "Data de Consulta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime dataConsulta { get; set; }
        [Required]
        [Display(Name = "Horario da Consulta")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime horarioConsulta { get; set; }
        [Required]
        [Display(Name = "Convenio")]
        public int ConvenioID { get; set; }
        [Required]
        [Display(Name = "Atendente")]
        public int AtendenteID { get; set; }
    
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Especialidade Especialidade { get; set; }
        public virtual Convenio Convenio { get; set; }
        public virtual Atendente Atendente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
