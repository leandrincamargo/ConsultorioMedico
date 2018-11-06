using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Cidade")]
    public class Cidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cidade()
        {
            this.Pessoas = new HashSet<Pessoa>();
        }
    
        public int CidadeID { get; set; }
        public string Nome { get; set; }
        public int EstadoID { get; set; }
        
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}
