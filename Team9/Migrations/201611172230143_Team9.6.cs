namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team96 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistAlbums", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.ArtistAlbums", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_ArtistID" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_AlbumID" });
            AddColumn("dbo.Artists", "Album_AlbumID", c => c.Int());
            CreateIndex("dbo.Artists", "Album_AlbumID");
            AddForeignKey("dbo.Artists", "Album_AlbumID", "dbo.Albums", "AlbumID");
            DropTable("dbo.ArtistAlbums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_ArtistID = c.Int(nullable: false),
                        Album_AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_ArtistID, t.Album_AlbumID });
            
            DropForeignKey("dbo.Artists", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.Artists", new[] { "Album_AlbumID" });
            DropColumn("dbo.Artists", "Album_AlbumID");
            CreateIndex("dbo.ArtistAlbums", "Album_AlbumID");
            CreateIndex("dbo.ArtistAlbums", "Artist_ArtistID");
            AddForeignKey("dbo.ArtistAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.ArtistAlbums", "Artist_ArtistID", "dbo.Artists", "ArtistID", cascadeDelete: true);
        }
    }
}
