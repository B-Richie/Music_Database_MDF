using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicDatabase.Models
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string AlbumName { get; set; }


        [Display(Name = "Number of Tracks")]
        public Int32 NumberOfTracks { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Year of Release")]
        public Int32 ReleaseYear { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public string Genre { get; set; }

        public int? ArtistID { get; set; }

        public Artist Artist { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}