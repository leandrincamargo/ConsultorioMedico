using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{
    [Table("Funcionario")]
    public partial class Funcionario : Pessoa
    {
        public int FuncionarioID { get; set; }
    }
}
