﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace Team9.Models
{
    public class Album
    {
        [Key]
        public Int32 AlbumID { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        public String AlbumName { get; set; }

        [Required(ErrorMessage = "Please enter a valid genre")]
        public virtual List<Genre> AlbumGenre { get; set; }

        [Required(ErrorMessage = "Please enter a valid price")]
        public Decimal AlbumPrice { get; set; }

        public virtual List<Artist> AlbumArtist { get; set; }

        public virtual List<Song> Songs { get; set; }

        public virtual List<Rating> AlbumRatings { get; set; }


    }
}