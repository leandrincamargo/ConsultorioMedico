using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{

    [Table("Especialidade")]
    public class Especialidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especialidade()
        {
            this.Medico = new HashSet<Medico>();
            this.Consulta = new HashSet<Consulta>();
        }
        [Key]
        public int EspecialidadeID { get; set; }
        [Required]
        public string Nome { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medico> Medico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
