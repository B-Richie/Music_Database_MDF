namespace Database_MDF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        AlbumID = c.Int(nullable: false, identity: true),
                        AlbumName = c.String(nullable: false),
                        NumberOfTracks = c.Int(nullable: false),
                        ReleaseYear = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, storeType: "money"),
                        Genre = c.String(),
                        ArtistID = c.Int(),
                    })
                .PrimaryKey(t => t.AlbumID)
                .ForeignKey("dbo.Artist", t => t.ArtistID)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        ArtistID = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistID);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        TrackID = c.Int(nullable: false, identity: true),
                        TrackName = c.String(nullable: false),
                        AlbumID = c.Int(),
                    })
                .PrimaryKey(t => t.TrackID)
                .ForeignKey("dbo.Album", t => t.AlbumID)
                .Index(t => t.AlbumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Track", "AlbumID", "dbo.Album");
            DropForeignKey("dbo.Album", "ArtistID", "dbo.Artist");
            DropIndex("dbo.Track", new[] { "AlbumID" });
            DropIndex("dbo.Album", new[] { "ArtistID" });
            DropTable("dbo.Track");
            DropTable("dbo.Artist");
            DropTable("dbo.Album");
        }
    }
}
