namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team912 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.Artists", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.Genres", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.Artists", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.Genres", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.Artists", new[] { "Song_SongID" });
            DropIndex("dbo.Artists", new[] { "Album_AlbumID" });
            DropIndex("dbo.Genres", new[] { "Artist_ArtistID" });
            DropIndex("dbo.Genres", new[] { "Song_SongID" });
            DropIndex("dbo.Genres", new[] { "Album_AlbumID" });
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_ArtistID = c.Int(nullable: false),
                        Album_AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_ArtistID, t.Album_AlbumID })
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.Artist_ArtistID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.GenreAlbums",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Album_AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Album_AlbumID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.GenreArtists",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Artist_ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Artist_ArtistID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Artist_ArtistID);
            
            CreateTable(
                "dbo.SongArtists",
                c => new
                    {
                        Song_SongID = c.Int(nullable: false),
                        Artist_ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Artist_ArtistID })
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID, cascadeDelete: true)
                .Index(t => t.Song_SongID)
                .Index(t => t.Artist_ArtistID);
            
            CreateTable(
                "dbo.SongGenres",
                c => new
                    {
                        Song_SongID = c.Int(nullable: false),
                        Genre_GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Genre_GenreID })
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .Index(t => t.Song_SongID)
                .Index(t => t.Genre_GenreID);
            
            AddColumn("dbo.PurchaseItems", "isAlbum", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseItems", "PurchaseItemAlbum_AlbumID", c => c.Int());
            CreateIndex("dbo.PurchaseItems", "PurchaseItemAlbum_AlbumID");
            AddForeignKey("dbo.PurchaseItems", "PurchaseItemAlbum_AlbumID", "dbo.Albums", "AlbumID");
            DropColumn("dbo.Artists", "Song_SongID");
            DropColumn("dbo.Artists", "Album_AlbumID");
            DropColumn("dbo.Genres", "Artist_ArtistID");
            DropColumn("dbo.Genres", "Song_SongID");
            DropColumn("dbo.Genres", "Album_AlbumID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Album_AlbumID", c => c.Int());
            AddColumn("dbo.Genres", "Song_SongID", c => c.Int());
            AddColumn("dbo.Genres", "Artist_ArtistID", c => c.Int());
            AddColumn("dbo.Artists", "Album_AlbumID", c => c.Int());
            AddColumn("dbo.Artists", "Song_SongID", c => c.Int());
            DropForeignKey("dbo.PurchaseItems", "PurchaseItemAlbum_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.SongGenres", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.SongGenres", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.SongArtists", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.GenreArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.GenreArtists", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.GenreAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.GenreAlbums", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.ArtistAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.ArtistAlbums", "Artist_ArtistID", "dbo.Artists");
            DropIndex("dbo.SongGenres", new[] { "Genre_GenreID" });
            DropIndex("dbo.SongGenres", new[] { "Song_SongID" });
            DropIndex("dbo.SongArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.SongArtists", new[] { "Song_SongID" });
            DropIndex("dbo.GenreArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.GenreArtists", new[] { "Genre_GenreID" });
            DropIndex("dbo.GenreAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.GenreAlbums", new[] { "Genre_GenreID" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_ArtistID" });
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseItemAlbum_AlbumID" });
            DropColumn("dbo.PurchaseItems", "PurchaseItemAlbum_AlbumID");
            DropColumn("dbo.PurchaseItems", "isAlbum");
            DropTable("dbo.SongGenres");
            DropTable("dbo.SongArtists");
            DropTable("dbo.GenreArtists");
            DropTable("dbo.GenreAlbums");
            DropTable("dbo.ArtistAlbums");
            CreateIndex("dbo.Genres", "Album_AlbumID");
            CreateIndex("dbo.Genres", "Song_SongID");
            CreateIndex("dbo.Genres", "Artist_ArtistID");
            CreateIndex("dbo.Artists", "Album_AlbumID");
            CreateIndex("dbo.Artists", "Song_SongID");
            AddForeignKey("dbo.Genres", "Album_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.Artists", "Album_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.Genres", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.Artists", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.Genres", "Artist_ArtistID", "dbo.Artists", "ArtistID");
        }
    }
}
