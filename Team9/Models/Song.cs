﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace Team9.Models
{
    public class Song
    {
        [Key]
        public Int32 SongID { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        public String SongName { get; set; }

        [Required(ErrorMessage = "Please enter a valid price")]
        public Decimal SongPrice { get; set; }

        [Required(ErrorMessage = "Please enter a valid length")]
        public Decimal SongLength { get; set; }

        public virtual Genre SongGenre { get; set; }

        [Required(ErrorMessage = "Please enter a valid artist")]
        public virtual List<Artist> SongArtist { get; set; }

        public virtual Album SongAlbum { get; set; }

        public virtual List<Rating> SongRatings { get; set; }
    }
}