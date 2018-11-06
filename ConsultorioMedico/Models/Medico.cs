using System;
using System.Collections.Generic;
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
    
        public string CRM { get; set; }
        public int EspecialidadeID { get; set; }
        public System.DateTime horarioAtendimento { get; set; }
    
        public virtual Especialidade Especialidade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
