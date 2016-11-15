namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team91 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ArtistSongs", newName: "SongArtists");
            DropForeignKey("dbo.Songs", "SongGenre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Artists", "ArtistGenre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Albums", "AlbumGenre_GenreID", "dbo.Genres");
            DropIndex("dbo.Songs", new[] { "SongGenre_GenreID" });
            DropIndex("dbo.Albums", new[] { "AlbumGenre_GenreID" });
            DropIndex("dbo.Artists", new[] { "ArtistGenre_GenreID" });
            DropPrimaryKey("dbo.SongArtists");
            CreateTable(
                "dbo.AlbumGenres",
                c => new
                    {
                        Album_AlbumID = c.Int(nullable: false),
                        Genre_GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Album_AlbumID, t.Genre_GenreID })
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .Index(t => t.Album_AlbumID)
                .Index(t => t.Genre_GenreID);
            
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
            
            AddColumn("dbo.AspNetUsers", "FName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LName", c => c.String());
            AddColumn("dbo.Ratings", "User_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.SongArtists", new[] { "Song_SongID", "Artist_ArtistID" });
            CreateIndex("dbo.Ratings", "User_Id");
            AddForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Songs", "SongGenre_GenreID");
            DropColumn("dbo.Albums", "AlbumGenre_GenreID");
            DropColumn("dbo.Artists", "ArtistGenre_GenreID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ArtistGenre_GenreID", c => c.Int(nullable: false));
            AddColumn("dbo.Albums", "AlbumGenre_GenreID", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "SongGenre_GenreID", c => c.Int());
            DropForeignKey("dbo.GenreArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.GenreArtists", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SongGenres", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.SongGenres", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.AlbumGenres", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.AlbumGenres", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.GenreArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.GenreArtists", new[] { "Genre_GenreID" });
            DropIndex("dbo.SongGenres", new[] { "Genre_GenreID" });
            DropIndex("dbo.SongGenres", new[] { "Song_SongID" });
            DropIndex("dbo.AlbumGenres", new[] { "Genre_GenreID" });
            DropIndex("dbo.AlbumGenres", new[] { "Album_AlbumID" });
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropPrimaryKey("dbo.SongArtists");
            DropColumn("dbo.Ratings", "User_Id");
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
            DropTable("dbo.GenreArtists");
            DropTable("dbo.SongGenres");
            DropTable("dbo.AlbumGenres");
            AddPrimaryKey("dbo.SongArtists", new[] { "Artist_ArtistID", "Song_SongID" });
            CreateIndex("dbo.Artists", "ArtistGenre_GenreID");
            CreateIndex("dbo.Albums", "AlbumGenre_GenreID");
            CreateIndex("dbo.Songs", "SongGenre_GenreID");
            AddForeignKey("dbo.Albums", "AlbumGenre_GenreID", "dbo.Genres", "GenreID", cascadeDelete: true);
            AddForeignKey("dbo.Artists", "ArtistGenre_GenreID", "dbo.Genres", "GenreID", cascadeDelete: true);
            AddForeignKey("dbo.Songs", "SongGenre_GenreID", "dbo.Genres", "GenreID");
            RenameTable(name: "dbo.SongArtists", newName: "ArtistSongs");
        }
    }
}
