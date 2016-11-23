namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team912 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistAlbums", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.ArtistAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.GenreAlbums", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.GenreAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.GenreArtists", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.GenreArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.SongArtists", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.SongGenres", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongGenres", "Genre_GenreID", "dbo.Genres");
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_ArtistID" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.GenreAlbums", new[] { "Genre_GenreID" });
            DropIndex("dbo.GenreAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.GenreArtists", new[] { "Genre_GenreID" });
            DropIndex("dbo.GenreArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.SongArtists", new[] { "Song_SongID" });
            DropIndex("dbo.SongArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.SongGenres", new[] { "Song_SongID" });
            DropIndex("dbo.SongGenres", new[] { "Genre_GenreID" });
            AddColumn("dbo.Artists", "Song_SongID", c => c.Int());
            AddColumn("dbo.Artists", "Album_AlbumID", c => c.Int());
            AddColumn("dbo.Genres", "Artist_ArtistID", c => c.Int());
            AddColumn("dbo.Genres", "Song_SongID", c => c.Int());
            AddColumn("dbo.Genres", "Album_AlbumID", c => c.Int());
            CreateIndex("dbo.Artists", "Song_SongID");
            CreateIndex("dbo.Artists", "Album_AlbumID");
            CreateIndex("dbo.Genres", "Artist_ArtistID");
            CreateIndex("dbo.Genres", "Song_SongID");
            CreateIndex("dbo.Genres", "Album_AlbumID");
            AddForeignKey("dbo.Genres", "Artist_ArtistID", "dbo.Artists", "ArtistID");
            AddForeignKey("dbo.Artists", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.Genres", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.Artists", "Album_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.Genres", "Album_AlbumID", "dbo.Albums", "AlbumID");
            DropTable("dbo.ArtistAlbums");
            DropTable("dbo.GenreAlbums");
            DropTable("dbo.GenreArtists");
            DropTable("dbo.SongArtists");
            DropTable("dbo.SongGenres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SongGenres",
                c => new
                    {
                        Song_SongID = c.Int(nullable: false),
                        Genre_GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Genre_GenreID });
            
            CreateTable(
                "dbo.SongArtists",
                c => new
                    {
                        Song_SongID = c.Int(nullable: false),
                        Artist_ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Artist_ArtistID });
            
            CreateTable(
                "dbo.GenreArtists",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Artist_ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Artist_ArtistID });
            
            CreateTable(
                "dbo.GenreAlbums",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Album_AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Album_AlbumID });
            
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_ArtistID = c.Int(nullable: false),
                        Album_AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_ArtistID, t.Album_AlbumID });
            
            DropForeignKey("dbo.Genres", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.Artists", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.Genres", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.Artists", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.Genres", "Artist_ArtistID", "dbo.Artists");
            DropIndex("dbo.Genres", new[] { "Album_AlbumID" });
            DropIndex("dbo.Genres", new[] { "Song_SongID" });
            DropIndex("dbo.Genres", new[] { "Artist_ArtistID" });
            DropIndex("dbo.Artists", new[] { "Album_AlbumID" });
            DropIndex("dbo.Artists", new[] { "Song_SongID" });
            DropColumn("dbo.Genres", "Album_AlbumID");
            DropColumn("dbo.Genres", "Song_SongID");
            DropColumn("dbo.Genres", "Artist_ArtistID");
            DropColumn("dbo.Artists", "Album_AlbumID");
            DropColumn("dbo.Artists", "Song_SongID");
            CreateIndex("dbo.SongGenres", "Genre_GenreID");
            CreateIndex("dbo.SongGenres", "Song_SongID");
            CreateIndex("dbo.SongArtists", "Artist_ArtistID");
            CreateIndex("dbo.SongArtists", "Song_SongID");
            CreateIndex("dbo.GenreArtists", "Artist_ArtistID");
            CreateIndex("dbo.GenreArtists", "Genre_GenreID");
            CreateIndex("dbo.GenreAlbums", "Album_AlbumID");
            CreateIndex("dbo.GenreAlbums", "Genre_GenreID");
            CreateIndex("dbo.ArtistAlbums", "Album_AlbumID");
            CreateIndex("dbo.ArtistAlbums", "Artist_ArtistID");
            AddForeignKey("dbo.SongGenres", "Genre_GenreID", "dbo.Genres", "GenreID", cascadeDelete: true);
            AddForeignKey("dbo.SongGenres", "Song_SongID", "dbo.Songs", "SongID", cascadeDelete: true);
            AddForeignKey("dbo.SongArtists", "Artist_ArtistID", "dbo.Artists", "ArtistID", cascadeDelete: true);
            AddForeignKey("dbo.SongArtists", "Song_SongID", "dbo.Songs", "SongID", cascadeDelete: true);
            AddForeignKey("dbo.GenreArtists", "Artist_ArtistID", "dbo.Artists", "ArtistID", cascadeDelete: true);
            AddForeignKey("dbo.GenreArtists", "Genre_GenreID", "dbo.Genres", "GenreID", cascadeDelete: true);
            AddForeignKey("dbo.GenreAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.GenreAlbums", "Genre_GenreID", "dbo.Genres", "GenreID", cascadeDelete: true);
            AddForeignKey("dbo.ArtistAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.ArtistAlbums", "Artist_ArtistID", "dbo.Artists", "ArtistID", cascadeDelete: true);
        }
    }
}
