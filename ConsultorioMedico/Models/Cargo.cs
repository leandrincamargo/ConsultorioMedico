using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioMedico.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        [Key]
        public int CargoID { get; set; }
        [Required]
        public string Nome { get; set; }
        public virtual ICollection<Pessoa> Pessoas { get; set; }

    }
}