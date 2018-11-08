using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConsultorioMedico.Models
{
    public class ConsultorioEntities : DbContext
    {
        public ConsultorioEntities()
            : base("name=ConsultorioEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  throw new UnintentionalCodeFirstException();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Atendente> Atendente { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<Convenio> Convenio { get; set; }
        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<Prontuario> Prontuario { get; set; }
        public virtual DbSet<Exame> Exame { get; set; }
    }
}
