using System.Collections.Generic;
using System.Linq;
using Memphis.BusinessLogic.Interface;
using MusicBrainz.Helpers;
using MusicBrainz.WebService.Service;
using Top3000Albums.Service;

namespace Mercury.Console.Generate
{
    using System;
    using System.IO;
    using System.Net;
    using MusicBrainz.CoverArt;
    using Mbz = MusicBrainz.WebService.Entity;
    using Mem = Memphis.Database.Entity;
    using T3k = Top3000Albums.Entity;

    class Top3kArtistsAndAlbumsToMemphis
    {
        private const int StartAt = 1;
        private static bool DownloadCoverArt = false;
        private const string coverArtFolder = @"..\..\..\Memphis.Website\CoverArt\Album\";

        public static void Go(IArtistService artistService, ITagService tagService, IAlbumService albumService, IRecordingService recordingService, ITrackService trackService)
        {
            List<T3k.Album> albums = T3kAlbumService.Read();
            int counter = 1;
            int total = albums.Count;
            foreach (var album in albums)
            {
                int? memArtistId = null;
                Mbz.Artist mbzArtist = null;

                ConsoleHelpers.WriteToConsole("{0}/{1} Artist: ", ConsoleColor.Gray, false, counter, total);
                ConsoleHelpers.WriteToConsole(album.Artist, ConsoleColor.White, false);
                if (counter >= StartAt && album.Artist != "Various Artists")
                {
                    if (album.HasMbzArtist)
                    {
                        mbzArtist = ArtistWebService.GetById(album.MbzArtistIdAsGuid.Value);
                    }
                    else
                    {
                        mbzArtist = ArtistWebService.FindByName(album.Artist, album.Title);
                    }
                    if (mbzArtist != null)
                    {
                        var existingArtist = artistService.GetByMusicBrainzId(mbzArtist.ArtistId);
                        if (existingArtist == null)
                        {
                            mbzArtist.Tags.RemoveAll(x => x.Name.Length > 50);

                            var tags = mbzArtist.Tags.Select(mbzTag => new Mem.Tag { Name = mbzTag.Name }).ToList();
                            tagService.Save(tags);

                            var artistTags = mbzArtist.Tags.Select(mbzTag => new Mem.ArtistTag
                            {
                                TagId = tags.Single(x => x.Name == mbzTag.Name).TagId.Value,
                                Count = mbzTag.Count
                            }).ToList();

                            var memArtist = new Mem.Artist
                            {
                                Name = mbzArtist.Name,
                                SortName = mbzArtist.SortName,
                                MusicBrainzId = mbzArtist.ArtistId,
                                ArtistTags = artistTags
                            };
                            artistService.Save(memArtist);
                            memArtistId = memArtist.ArtistId;
                            ConsoleHelpers.WriteToConsole(" saved", System.ConsoleColor.Green, false);
                        }
                        else
                        {
                            memArtistId = existingArtist.ArtistId;
                            ConsoleHelpers.WriteToConsole(" already exists", System.ConsoleColor.Yellow, false);
                        }
                    }
                    else
                    {
                        ConsoleHelpers.WriteToConsole(" not found", System.ConsoleColor.Magenta, false);
                    }



                    ConsoleHelpers.WriteToConsole(" Album: ", System.ConsoleColor.Gray, false, counter, total);
                    ConsoleHelpers.WriteToConsole(album.Title, System.ConsoleColor.White, false, counter, total);
                    Mbz.ReleaseGroup mbzReleaseGroup = null;

                    if (album.HasMbzReleaseGroup)
                    {
                        mbzReleaseGroup = ReleaseGroupWebService.Query(album.MbzReleaseGroupIdAsGuid.Value);
                    }

                    if (mbzReleaseGroup != null)
                    {
                        if (mbzReleaseGroup.Releases.Count > 0)
                        {
                            Guid releaseId = mbzReleaseGroup.Releases[0].ReleaseId;
                            Mbz.Release release = ReleaseWebService.Query(releaseId);
                            if (release != null)
                            {
                                var memAlbum = albumService.GetByMusicBrainzId(album.MbzReleaseGroupIdAsGuid.Value);
                                if (memAlbum == null)
                                {
                                    memAlbum = new Mem.Album
                                    {
                                        MusicBrainzReleaseGroupId = album.MbzReleaseGroupIdAsGuid,
                                        Title = release.Title,
                                        ArtistId = memArtistId,
                                        Barcode = !string.IsNullOrEmpty(release.Barcode) ? release.Barcode : null,
                                        Year = mbzReleaseGroup.FirstReleaseYear,
                                        Top3kPosition = album.PlaceAsInt
                                    };
                                    albumService.Save(memAlbum);
                                    ConsoleHelpers.WriteToConsole(" saved ", System.ConsoleColor.Green, false);
                                    findCoverArt(memAlbum, albumService);
                                }
                                else
                                {
                                    ConsoleHelpers.WriteToConsole(" already exists", System.ConsoleColor.Yellow, true);
                                }
                                Top3kRecordingsForMemphis.GetRecordingsForReleaseGroup(album.MbzReleaseGroupIdAsGuid.Value, memAlbum.AlbumId.Value, recordingService, trackService);
                            }
                            else
                            {
                                ConsoleHelpers.WriteToConsole(" release not found", System.ConsoleColor.Magenta, true);
                            }
                        }
                        else
                        {
                            ConsoleHelpers.WriteToConsole(" has no releases", System.ConsoleColor.Magenta, true);
                        }
                    }
                    else
                    {
                        ConsoleHelpers.WriteToConsole(" not found", System.ConsoleColor.Magenta, true);
                    }
                }
                else
                {
                    ConsoleHelpers.WriteToConsole(" skipped", System.ConsoleColor.Magenta, true);
                }
                counter++;
            }

        }

        private static void findCoverArt(Mem.Album album, IAlbumService albumService)
        {
            var filename = string.Format("{0}{1}.jpg", coverArtFolder, album.MusicBrainzReleaseGroupId);

            if (DownloadCoverArt)
            {
                if (!File.Exists(filename))
                {
                    string url = CoverArtWebService.GetSmallThumbnailByReleaseGroupId(album.MusicBrainzReleaseGroupId.Value);
                    if (!string.IsNullOrEmpty(url))
                    {
                        using (var client = new WebClient())
                        {
                            try
                            {
                                client.DownloadFile(url, filename);
                                ConsoleHelpers.WriteToConsole("cover art downloaded", ConsoleColor.Cyan, true);
                                album.HasThumbnail = true;
                            }
                            catch
                            {
                                ConsoleHelpers.WriteToConsole("error!", ConsoleColor.Red, true);
                                album.HasThumbnail = false;
                            }
                        }
                    }
                    else
                    {
                        ConsoleHelpers.WriteToConsole("no cover art", ConsoleColor.DarkCyan, true);
                        album.HasThumbnail = false;
                    }
                }
                else
                {
                    ConsoleHelpers.WriteToConsole("cover art already exists", ConsoleColor.Cyan, true);
                    album.HasThumbnail = true;
                }
            }
            else
            {
                if (File.Exists(filename))
                {
                    ConsoleHelpers.WriteToConsole("cover art exists", ConsoleColor.Cyan, true);
                    album.HasThumbnail = true;
                }
                else
                {
                    ConsoleHelpers.WriteToConsole("cover art missing", ConsoleColor.DarkCyan, true);
                    album.HasThumbnail = false;
                }
            }

            albumService.Save(album);
        }
    }
}
