namespace CallTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CallRecords",
                c => new
                    {
                        call_id = c.Long(nullable: false, identity: true),
                        call_day = c.DateTime(nullable: false),
                        call_time = c.DateTime(nullable: false),
                        available = c.Boolean(nullable: false),
                        contact_id = c.String(),
                        phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.call_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CallRecords");
        }
    }
}
