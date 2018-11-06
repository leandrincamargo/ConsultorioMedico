namespace ConsultorioMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(nullable: false),
                        Endereco = c.String(nullable: false),
                        Nascimento = c.DateTime(nullable: false),
                        CPF = c.String(nullable: false),
                        CEP = c.String(nullable: false),
                        Numero = c.Short(nullable: false),
                        CidadeID = c.Int(nullable: false),
                        EstadoID = c.Int(nullable: false),
                        Senha = c.String(nullable: false),
                        CargoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Cargo", t => t.CargoID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .Index(t => t.CidadeID)
                .Index(t => t.EstadoID)
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
                "dbo.Cidade",
                c => new
                    {
                        CidadeID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        EstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CidadeID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoID = c.Int(nullable: false, identity: true),
                        UF = c.String(),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.EstadoID);
            
            CreateTable(
                "dbo.Consulta",
                c => new
                    {
                        ConsultaID = c.Int(nullable: false, identity: true),
                        dataConsulta = c.DateTime(nullable: false),
                        MedicoID = c.Int(nullable: false),
                        PacienteID = c.Int(nullable: false),
                        EspecialidadeID = c.Int(nullable: false),
                        ConvenioID = c.Int(nullable: false),
                        AtendenteID = c.Int(nullable: false),
                        Atendente_PessoaID = c.Int(),
                        Atendente_Login = c.String(maxLength: 128),
                        Paciente_PessoaID = c.Int(),
                        Paciente_Login = c.String(maxLength: 128),
                        Medico_PessoaID = c.Int(),
                        Medico_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ConsultaID)
                .ForeignKey("dbo.Atendente", t => new { t.Atendente_PessoaID, t.Atendente_Login })
                .ForeignKey("dbo.Convenio", t => t.ConvenioID)
                .ForeignKey("dbo.Paciente", t => new { t.Paciente_PessoaID, t.Paciente_Login })
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeID)
                .ForeignKey("dbo.Medico", t => new { t.Medico_PessoaID, t.Medico_Login })
                .Index(t => t.EspecialidadeID)
                .Index(t => t.ConvenioID)
                .Index(t => new { t.Atendente_PessoaID, t.Atendente_Login })
                .Index(t => new { t.Paciente_PessoaID, t.Paciente_Login })
                .Index(t => new { t.Medico_PessoaID, t.Medico_Login });
            
            CreateTable(
                "dbo.Convenio",
                c => new
                    {
                        ConvenioID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.ConvenioID);
            
            CreateTable(
                "dbo.Prontuario",
                c => new
                    {
                        ProntuarioID = c.Int(nullable: false, identity: true),
                        Informacoes = c.String(),
                        MedicoID = c.Int(nullable: false),
                        PacienteID = c.Int(nullable: false),
                        ExameID = c.Int(nullable: false),
                        ConsultaID = c.Int(nullable: false),
                        Medico_PessoaID = c.Int(),
                        Medico_Login = c.String(maxLength: 128),
                        Paciente_PessoaID = c.Int(),
                        Paciente_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProntuarioID)
                .ForeignKey("dbo.Consulta", t => t.ConsultaID)
                .ForeignKey("dbo.Exame", t => t.ExameID)
                .ForeignKey("dbo.Medico", t => new { t.Medico_PessoaID, t.Medico_Login })
                .ForeignKey("dbo.Paciente", t => new { t.Paciente_PessoaID, t.Paciente_Login })
                .Index(t => t.ExameID)
                .Index(t => t.ConsultaID)
                .Index(t => new { t.Medico_PessoaID, t.Medico_Login })
                .Index(t => new { t.Paciente_PessoaID, t.Paciente_Login });
            
            CreateTable(
                "dbo.Exame",
                c => new
                    {
                        ExameID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.ExameID);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadeID);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 128),
                        FuncionarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaID, t.Login })
                .Index(t => new { t.PessoaID, t.Login });
            
            CreateTable(
                "dbo.Atendente",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Funcionario", t => new { t.PessoaID, t.Login })
                .Index(t => new { t.PessoaID, t.Login });
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 128),
                        CRM = c.String(),
                        EspecialidadeID = c.Int(nullable: false),
                        horarioAtendimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Funcionario", t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeID)
                .Index(t => new { t.PessoaID, t.Login })
                .Index(t => t.EspecialidadeID);
            
            CreateTable(
                "dbo.Paciente",
                c => new
                    {
                        PessoaID = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 128),
                        PacienteID = c.Int(nullable: false),
                        ConvenioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Pessoa", t => new { t.PessoaID, t.Login })
                .ForeignKey("dbo.Convenio", t => t.ConvenioID)
                .Index(t => new { t.PessoaID, t.Login })
                .Index(t => t.ConvenioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paciente", "ConvenioID", "dbo.Convenio");
            DropForeignKey("dbo.Paciente", new[] { "PessoaID", "Login" }, "dbo.Pessoa");
            DropForeignKey("dbo.Medico", "EspecialidadeID", "dbo.Especialidade");
            DropForeignKey("dbo.Medico", new[] { "PessoaID", "Login" }, "dbo.Funcionario");
            DropForeignKey("dbo.Atendente", new[] { "PessoaID", "Login" }, "dbo.Funcionario");
            DropForeignKey("dbo.Funcionario", new[] { "PessoaID", "Login" }, "dbo.Pessoa");
            DropForeignKey("dbo.Consulta", new[] { "Medico_PessoaID", "Medico_Login" }, "dbo.Medico");
            DropForeignKey("dbo.Consulta", "EspecialidadeID", "dbo.Especialidade");
            DropForeignKey("dbo.Prontuario", new[] { "Paciente_PessoaID", "Paciente_Login" }, "dbo.Paciente");
            DropForeignKey("dbo.Prontuario", new[] { "Medico_PessoaID", "Medico_Login" }, "dbo.Medico");
            DropForeignKey("dbo.Prontuario", "ExameID", "dbo.Exame");
            DropForeignKey("dbo.Prontuario", "ConsultaID", "dbo.Consulta");
            DropForeignKey("dbo.Consulta", new[] { "Paciente_PessoaID", "Paciente_Login" }, "dbo.Paciente");
            DropForeignKey("dbo.Consulta", "ConvenioID", "dbo.Convenio");
            DropForeignKey("dbo.Consulta", new[] { "Atendente_PessoaID", "Atendente_Login" }, "dbo.Atendente");
            DropForeignKey("dbo.Pessoa", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.Pessoa", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Cidade", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Pessoa", "CargoID", "dbo.Cargo");
            DropIndex("dbo.Paciente", new[] { "ConvenioID" });
            DropIndex("dbo.Paciente", new[] { "PessoaID", "Login" });
            DropIndex("dbo.Medico", new[] { "EspecialidadeID" });
            DropIndex("dbo.Medico", new[] { "PessoaID", "Login" });
            DropIndex("dbo.Atendente", new[] { "PessoaID", "Login" });
            DropIndex("dbo.Funcionario", new[] { "PessoaID", "Login" });
            DropIndex("dbo.Prontuario", new[] { "Paciente_PessoaID", "Paciente_Login" });
            DropIndex("dbo.Prontuario", new[] { "Medico_PessoaID", "Medico_Login" });
            DropIndex("dbo.Prontuario", new[] { "ConsultaID" });
            DropIndex("dbo.Prontuario", new[] { "ExameID" });
            DropIndex("dbo.Consulta", new[] { "Medico_PessoaID", "Medico_Login" });
            DropIndex("dbo.Consulta", new[] { "Paciente_PessoaID", "Paciente_Login" });
            DropIndex("dbo.Consulta", new[] { "Atendente_PessoaID", "Atendente_Login" });
            DropIndex("dbo.Consulta", new[] { "ConvenioID" });
            DropIndex("dbo.Consulta", new[] { "EspecialidadeID" });
            DropIndex("dbo.Cidade", new[] { "EstadoID" });
            DropIndex("dbo.Pessoa", new[] { "CargoID" });
            DropIndex("dbo.Pessoa", new[] { "EstadoID" });
            DropIndex("dbo.Pessoa", new[] { "CidadeID" });
            DropTable("dbo.Paciente");
            DropTable("dbo.Medico");
            DropTable("dbo.Atendente");
            DropTable("dbo.Funcionario");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Exame");
            DropTable("dbo.Prontuario");
            DropTable("dbo.Convenio");
            DropTable("dbo.Consulta");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Cargo");
            DropTable("dbo.Pessoa");
        }
    }
}
