using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioMedico.Models
{

    [Table("Consulta")]
    public class Consulta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consulta()
        {
            this.Prontuario = new HashSet<Prontuario>();
        }
    
        public int ConsultaID { get; set; }
        public System.DateTime dataConsulta { get; set; }
        public int MedicoID { get; set; }
        public int PacienteID { get; set; }
        public int EspecialidadeID { get; set; }
        public int ConvenioID { get; set; }
        public int AtendenteID { get; set; }
    
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Especialidade Especialidade { get; set; }
        public virtual Convenio Convenio { get; set; }
        public virtual Atendente Atendente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
