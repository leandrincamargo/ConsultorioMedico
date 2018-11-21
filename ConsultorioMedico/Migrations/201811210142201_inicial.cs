namespace ConsultorioMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        PessoaID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Nome = c.String(nullable: false),
                        Endereco = c.String(nullable: false),
                        Nascimento = c.DateTime(nullable: false),
                        CPF = c.String(nullable: false),
                        CEP = c.String(nullable: false),
                        Numero = c.Short(nullable: false),
                        Cidade = c.String(nullable: false),
                        Estado = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        CargoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PessoaID)
                .ForeignKey("dbo.Cargo", t => t.CargoID)
                .Index(t => t.CargoID);
            
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        CargoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CargoID);
            
            CreateTable(
                "dbo.Consulta",
                c => new
                    {
                        ConsultaID = c.Int(nullable: false, identity: true),
                        PacienteID = c.Int(nullable: false),
                        EspecialidadeID = c.Int(nullable: false),
                        MedicoID = c.Int(nullable: false),
                        dataConsulta = c.DateTime(nullable: false),
                        horarioConsulta = c.DateTime(nullable: false),
                        ConvenioID = c.Int(nullable: false),
                        AtendenteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultaID)
                .ForeignKey("dbo.Atendente", t => t.AtendenteID)
                .ForeignKey("dbo.Convenio", t => t.ConvenioID)
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeID)
                .ForeignKey("dbo.Medico", t => t.MedicoID)
                .ForeignKey("dbo.Paciente", t => t.PacienteID)
                .Index(t => t.PacienteID)
                .Index(t => t.EspecialidadeID)
                .Index(t => t.MedicoID)
                .Index(t => t.ConvenioID)
                .Index(t => t.AtendenteID);
            
            CreateTable(
                "dbo.Convenio",
                c => new
                    {
                        ConvenioID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ConvenioID);
            
            CreateTable(
                "dbo.Prontuario",
                c => new
                    {
                        ProntuarioID = c.Int(nullable: false, identity: true),
                        Informacoes = c.String(nullable: false),
                        MedicoID = c.Int(nullable: false),
                        PacienteID = c.Int(nullable: false),
                        ExameID = c.Int(nullable: false),
                        ConsultaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProntuarioID)
                .ForeignKey("dbo.Consulta", t => t.ConsultaID)
                .ForeignKey("dbo.Exame", t => t.ExameID)
                .ForeignKey("dbo.Medico", t => t.MedicoID)
                .ForeignKey("dbo.Paciente", t => t.PacienteID)
                .Index(t => t.MedicoID)
                .Index(t => t.PacienteID)
                .Index(t => t.ExameID)
                .Index(t => t.ConsultaID);
            
            CreateTable(
                "dbo.Exame",
                c => new
                    {
                        ExameID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ExameID);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EspecialidadeID);
            
            CreateTable(
                "dbo.Atendente",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PessoaID)
                .ForeignKey("dbo.Pessoa", t => t.PessoaID)
                .Index(t => t.PessoaID);
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        CRM = c.String(nullable: false),
                        EspecialidadeID = c.Int(nullable: false),
                        horarioEntrada = c.DateTime(nullable: false),
                        horarioSaida = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PessoaID)
                .ForeignKey("dbo.Pessoa", t => t.PessoaID)
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeID)
                .Index(t => t.PessoaID)
                .Index(t => t.EspecialidadeID);
            
            CreateTable(
                "dbo.Paciente",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        ConvenioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PessoaID)
                .ForeignKey("dbo.Pessoa", t => t.PessoaID)
                .ForeignKey("dbo.Convenio", t => t.ConvenioID)
                .Index(t => t.PessoaID)
                .Index(t => t.ConvenioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paciente", "ConvenioID", "dbo.Convenio");
            DropForeignKey("dbo.Paciente", "PessoaID", "dbo.Pessoa");
            DropForeignKey("dbo.Medico", "EspecialidadeID", "dbo.Especialidade");
            DropForeignKey("dbo.Medico", "PessoaID", "dbo.Pessoa");
            DropForeignKey("dbo.Atendente", "PessoaID", "dbo.Pessoa");
            DropForeignKey("dbo.Consulta", "PacienteID", "dbo.Paciente");
            DropForeignKey("dbo.Consulta", "MedicoID", "dbo.Medico");
            DropForeignKey("dbo.Consulta", "EspecialidadeID", "dbo.Especialidade");
            DropForeignKey("dbo.Consulta", "ConvenioID", "dbo.Convenio");
            DropForeignKey("dbo.Prontuario", "PacienteID", "dbo.Paciente");
            DropForeignKey("dbo.Prontuario", "MedicoID", "dbo.Medico");
            DropForeignKey("dbo.Prontuario", "ExameID", "dbo.Exame");
            DropForeignKey("dbo.Prontuario", "ConsultaID", "dbo.Consulta");
            DropForeignKey("dbo.Consulta", "AtendenteID", "dbo.Atendente");
            DropForeignKey("dbo.Pessoa", "CargoID", "dbo.Cargo");
            DropIndex("dbo.Paciente", new[] { "ConvenioID" });
            DropIndex("dbo.Paciente", new[] { "PessoaID" });
            DropIndex("dbo.Medico", new[] { "EspecialidadeID" });
            DropIndex("dbo.Medico", new[] { "PessoaID" });
            DropIndex("dbo.Atendente", new[] { "PessoaID" });
            DropIndex("dbo.Prontuario", new[] { "ConsultaID" });
            DropIndex("dbo.Prontuario", new[] { "ExameID" });
            DropIndex("dbo.Prontuario", new[] { "PacienteID" });
            DropIndex("dbo.Prontuario", new[] { "MedicoID" });
            DropIndex("dbo.Consulta", new[] { "AtendenteID" });
            DropIndex("dbo.Consulta", new[] { "ConvenioID" });
            DropIndex("dbo.Consulta", new[] { "MedicoID" });
            DropIndex("dbo.Consulta", new[] { "EspecialidadeID" });
            DropIndex("dbo.Consulta", new[] { "PacienteID" });
            DropIndex("dbo.Pessoa", new[] { "CargoID" });
            DropTable("dbo.Paciente");
            DropTable("dbo.Medico");
            DropTable("dbo.Atendente");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Exame");
            DropTable("dbo.Prontuario");
            DropTable("dbo.Convenio");
            DropTable("dbo.Consulta");
            DropTable("dbo.Cargo");
            DropTable("dbo.Pessoa");
        }
    }
}
