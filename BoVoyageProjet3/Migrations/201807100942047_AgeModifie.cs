namespace BoVoyageProjet3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgeModifie : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "Age");
            DropColumn("dbo.Participants", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Age", c => c.Int(nullable: false));
        }
    }
}
