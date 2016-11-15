namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team92 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AlbumGenres", newName: "GenreAlbums");
            DropPrimaryKey("dbo.GenreAlbums");
            AddPrimaryKey("dbo.GenreAlbums", new[] { "Genre_GenreID", "Album_AlbumID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GenreAlbums");
            AddPrimaryKey("dbo.GenreAlbums", new[] { "Album_AlbumID", "Genre_GenreID" });
            RenameTable(name: "dbo.GenreAlbums", newName: "AlbumGenres");
        }
    }
}
