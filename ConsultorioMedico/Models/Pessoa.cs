using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int PessoaID { get; set; }
        public string Login { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        [Display(Name ="Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Nascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public short Numero { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Display(Name = "Cargo")]
        public int CargoID { get; set; }
        
        public virtual Cargo Cargo { get; set; }
    }
}
