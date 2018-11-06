using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Funcionario")]
    public class Funcionario : Pessoa
    {
        [Key]
        public int FuncionarioID { get; set; }
    }
}
