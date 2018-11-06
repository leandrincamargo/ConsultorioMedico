using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Medico")]
    public class Medico : Funcionario
    {
        public Medico()
        {
            this.Consulta = new HashSet<Consulta>();
            this.Prontuario = new HashSet<Prontuario>();
        }

        [Required]
        public string CRM { get; set; }
        [Required]
        [Display(Name = "Especialidade")]
        public int EspecialidadeID { get; set; }
        [Required]
        [Display(Name = "Horario de Entrada")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime horarioEntrada { get; set; }
        [Required]
        [Display(Name = "Horario de Saida")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime horarioSaida { get; set; }

        public virtual Especialidade Especialidade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
