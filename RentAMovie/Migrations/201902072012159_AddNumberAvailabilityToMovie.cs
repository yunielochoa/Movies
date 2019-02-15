namespace RentAMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvailabilityToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailability", c => c.Int(nullable: false));
            Sql("UPDATE Movies SET NumberAvailability = Stock ");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailability");
        }
    }
}
