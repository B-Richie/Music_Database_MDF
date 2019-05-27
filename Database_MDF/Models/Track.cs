using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicDatabase.Models
{
    public class Track
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrackID { get; set; }

        [Required]
        public string TrackName { get; set; }
        public int? AlbumID { get; set; }
        public Album Album { get; set; }
    }
}