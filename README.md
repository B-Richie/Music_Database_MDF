# Generate an MDF File to Use in ASP .Net and Core Projects

Run EF Migrations to populate database

# How it Works

Add mp3 music files to the following location:

      C:\Music\

Open Package Manager Console in Visual Studio

Enable migrations for your project:

        enable-migrations
      
Update the database:

      update-database
      
      
After updating the database in the Package Manager Console, your .MDF file will appear inside the App_Data folder within your solution file

Configuration.cs

To change the default path to search for .mp3 files, change the seed method path:

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
          
  The TagLib 3rd-party library is used to read the metadata inside the mp3 file
  

201905242151517_InitialCreate.cs

The migration class used to create the database


MusicContext.cs

The Entity Framework db context file repository
