namespace FFmpeg_wrapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateVideoJobTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.Int(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 255),
                        Title = c.String(maxLength: 255),
                        Ingest = c.DateTime(nullable: false),
                        TranscodeAttempt = c.Byte(),
                        TranscodeFilename = c.String(maxLength: 255),
                        TranscodeSuccess = c.DateTime(),
                        PublishAttempt = c.Byte(),
                        PublishLocation = c.String(maxLength: 255),
                        PublishSuccess = c.DateTime(),
                        Cleanup = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideoJobs");
        }
    }
}
