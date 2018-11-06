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
        [Column(Order = 1)]
        public int PessoaID { get; set; }
        [Key]
        [Column(Order = 2)]
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
        [Display(Name = "Cidade")]
        [Required]
        public int CidadeID { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public int EstadoID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        public int CargoID { get; set; }

        public virtual Cidade Cidade { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
