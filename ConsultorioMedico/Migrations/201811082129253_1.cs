namespace ConsultorioMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Paciente", "PacienteID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paciente", "PacienteID", c => c.Int(nullable: false));
        }
    }
}
