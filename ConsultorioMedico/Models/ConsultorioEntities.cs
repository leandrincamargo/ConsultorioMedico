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

            modelBuilder.Entity<Consulta>()
                .HasRequired<Medico>(s => s.Medico)
                .WithMany(g => g.Consulta)
                .HasForeignKey<int>(s => s.MedicoID);

            modelBuilder.Entity<Consulta>()
                .HasRequired<Atendente>(s => s.Atendente)
                .WithMany(g => g.Consulta)
                .HasForeignKey<int>(s => s.AtendenteID);

            modelBuilder.Entity<Consulta>()
                .HasRequired<Paciente>(s => s.Paciente)
                .WithMany(g => g.Consulta)
                .HasForeignKey<int>(s => s.PacienteID);

            modelBuilder.Entity<Consulta>()
                .HasRequired<Convenio>(s => s.Convenio)
                .WithMany(g => g.Consulta)
                .HasForeignKey<int>(s => s.ConvenioID);

            modelBuilder.Entity<Pessoa>()
                .HasRequired<Cargo>(s => s.Cargo)
                .WithMany(g => g.Pessoas)
                .HasForeignKey<int>(s => s.CargoID);

            modelBuilder.Entity<Paciente>()
                .HasRequired<Convenio>(s => s.Convenio)
                .WithMany(g => g.Paciente)
                .HasForeignKey<int>(s => s.ConvenioID);

            modelBuilder.Entity<Medico>()
                .HasRequired<Especialidade>(s => s.Especialidade)
                .WithMany(g => g.Medico)
                .HasForeignKey<int>(s => s.EspecialidadeID);

            modelBuilder.Entity<Consulta>()
                .HasRequired<Especialidade>(s => s.Especialidade)
                .WithMany(g => g.Consulta)
                .HasForeignKey<int>(s => s.EspecialidadeID);

            modelBuilder.Entity<Prontuario>()
                .HasRequired<Exame>(s => s.Exame)
                .WithMany(g => g.Prontuario)
                .HasForeignKey<int>(s => s.ExameID);

            modelBuilder.Entity<Prontuario>()
                .HasRequired<Medico>(s => s.Medico)
                .WithMany(g => g.Prontuario)
                .HasForeignKey<int>(s => s.MedicoID);

            modelBuilder.Entity<Prontuario>()
                .HasRequired<Paciente>(s => s.Paciente)
                .WithMany(g => g.Prontuario)
                .HasForeignKey<int>(s => s.PacienteID);
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
