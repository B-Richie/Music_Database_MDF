namespace Database_MDF.Migrations
{
    using MusicDatabase.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicDatabase.DB.MusicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicDatabase.DB.MusicContext context)
        {
            var path = @"C:\Music\";
            var files = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                var mp3 = TagLib.File.Create(file);
                var artist = new Artist { ArtistName = mp3.Tag.FirstAlbumArtist, Albums = new List<Album>() };
                context.Artists.AddOrUpdate(a => a.ArtistName, artist);
                context.SaveChanges();

                var album = new Album
                {
                    AlbumName = mp3.Tag.Album,
                    ArtistID = artist.ArtistID,
                    Genre = mp3.Tag.FirstGenre,
                    NumberOfTracks = Convert.ToInt32(mp3.Tag.TrackCount),
                    ReleaseYear = Convert.ToInt32(mp3.Tag.Year),
                    Tracks = new List<Track>()
                };
                context.Albums.AddOrUpdate(a => a.AlbumName, album);
                context.SaveChanges();

                var track = new Track { TrackName = mp3.Tag.Title, AlbumID = album.AlbumID };
                context.Tracks.AddOrUpdate(t => t.TrackName, track);
                context.SaveChanges();
            }
        }
    }
}
