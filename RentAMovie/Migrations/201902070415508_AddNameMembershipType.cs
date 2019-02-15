namespace RentAMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes set Name = 'Pay as You Go' where Id=1");
            Sql("UPDATE MembershipTypes set Name = 'Monthly' where Id=2");
            Sql("UPDATE MembershipTypes set Name = 'Quarterly' where Id=3");
            Sql("UPDATE MembershipTypes set Name = 'Annual' where Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
