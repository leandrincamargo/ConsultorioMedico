using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Exame")]
    public class Exame
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exame()
        {
            this.Prontuario = new HashSet<Prontuario>();
        }

        [Key]
        public int ExameID { get; set; }
        [Required]
        public string Nome { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
