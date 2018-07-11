namespace BoVoyageProjet3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifModelDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgenceVoyages", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AgenceVoyages", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Clients", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Destinations", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Destinations", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.DossierReservations", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.DossierReservations", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Voyages", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Voyages", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Participants", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Participants", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participants", "DeletedAt");
            DropColumn("dbo.Participants", "Deleted");
            DropColumn("dbo.Voyages", "DeletedAt");
            DropColumn("dbo.Voyages", "Deleted");
            DropColumn("dbo.DossierReservations", "DeletedAt");
            DropColumn("dbo.DossierReservations", "Deleted");
            DropColumn("dbo.Destinations", "DeletedAt");
            DropColumn("dbo.Destinations", "Deleted");
            DropColumn("dbo.Clients", "DeletedAt");
            DropColumn("dbo.Clients", "Deleted");
            DropColumn("dbo.AgenceVoyages", "DeletedAt");
            DropColumn("dbo.AgenceVoyages", "Deleted");
        }
    }
}
