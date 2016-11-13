using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team9.Models
{
    public class Genre
    {
        public Int32 GenreID { get; set; }

        public string GenreName { get; set; }

        public virtual List<Artist> Artists { get; set; }

        public virtual List<Song> Songs { get; set; }

        public virtual List<Album> Albums { get; set; }
    }
}