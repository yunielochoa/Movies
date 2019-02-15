namespace RentAMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                   INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3e5a2140-3ae2-4a83-a894-91f75aeaed85', N'guess@rentamovie.com', 0, N'AMHAEu2yyBwR/tFLxwV9ePzuaIcx9isz8Th+vomJaAyp89EWoHmqUfYxnf0izKgJYw==', N'77a2a13e-2f2a-40ba-8088-429801e079a7', NULL, 0, 0, NULL, 1, 0, N'guess@rentamovie.com')
                   INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8bdb8e5f-85f0-49af-aee9-6a9eabbd7224', N'admin@rentamovie.com', 0, N'AMkIJWEoLsAfAHEAUJ1yNuhbh07597goe3JiUCfp3/QAR7MtEBllqrKUimH2Nz9fHQ==', N'c866dfc4-28cb-4377-aea4-3275be6bb669', NULL, 0, 0, NULL, 1, 0, N'admin@rentamovie.com')
                         
                   INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'31b319b0-05b1-41d5-b1b6-07ada150ec6a', N'CanManageMovie')
                   
                   INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8bdb8e5f-85f0-49af-aee9-6a9eabbd7224', N'31b319b0-05b1-41d5-b1b6-07ada150ec6a')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
