using System;
using System.Linq;
using Memphis.BusinessLogic.Interface;
using MusicBrainz.Helpers;
using MusicBrainz.WebService.Service;
using static MusicBrainz.Helpers.ConsoleHelpers;

namespace Mercury.Console.Generate
{
    using Mbz = MusicBrainz.WebService.Entity;
    using Mem = Memphis.Database.Entity;
    
    class Top3kRecordingsForMemphis
    {
        public static void Go(IAlbumService albumService, IRecordingService recordingService, ITrackService trackService)
        {
            var totalCount = albumService.Count(null, null);
            var counter = 1;

            foreach (var album in albumService.GetAll())
            {
                WriteToConsole($"{counter++}/{totalCount} ", ConsoleColor.Gray, false);
                WriteToConsole($"{album.Title}", ConsoleColor.White, true);

                if (album.MusicBrainzReleaseGroupId.HasValue)
                {
                    GetRecordingsForReleaseGroup(album.MusicBrainzReleaseGroupId.Value, album.AlbumId.Value, recordingService, trackService);
                }
                else
                {
                    WriteToConsole("missing MusicBrainz ID", ConsoleColor.Magenta, true);
                }
            }

        }

        public static void GetRecordingsForReleaseGroup(Guid mbzReleaseGroupId, int albumId, IRecordingService recordingService, ITrackService trackService)
        {
            var releaseGroup = ReleaseGroupWebService.Query(mbzReleaseGroupId);
            if (releaseGroup != null)
            {
                if (releaseGroup.Releases.Count > 0)
                {
                    var release = ReleaseWebService.Query(releaseGroup.Releases.OrderBy(x => x.Date).First().ReleaseId);
                    if (release.Mediums.Count > 0)
                    {
                        if (release.Mediums[0].Tracks.Count > 0)
                        {
                            foreach (var track in release.Mediums[0].Tracks)
                            {
                                WriteToConsole($"  {track.Position} : ", ConsoleColor.Gray, false);
                                WriteToConsole(track.Recording.Title, ConsoleColor.White, false);
                                var memRecording = recordingService.GetByMusicBrainzId(track.Recording.RecordingId);
                                if (memRecording == null)
                                {
                                    memRecording = new Mem.Recording
                                    {
                                        Title = track.Recording.Title,
                                        Length = track.Recording.Length,
                                        MusicBrainzId = track.Recording.RecordingId
                                    };
                                    recordingService.Save(memRecording);
                                    WriteToConsole(" recording created", ConsoleColor.Green, false);
                                }
                                else
                                {
                                    WriteToConsole(" recording already exists", ConsoleColor.Yellow, false);
                                }

                                var memTrack = trackService.GetByAlbumIdAndPosition(albumId, track.Position);
                                if (memTrack == null)
                                {
                                    memTrack = new Mem.Track
                                    {
                                        AlbumId = albumId,
                                        Position = track.Position,
                                        RecordingId = memRecording.RecordingId.Value
                                    };
                                    trackService.Save(memTrack);
                                    WriteToConsole(" track created", ConsoleColor.Green, true);
                                }
                                else
                                {
                                    WriteToConsole(" track already exists", ConsoleColor.Yellow, true);
                                }
                            }
                        }
                        else
                        {
                            WriteToConsole("  medium has no tracks", ConsoleColor.Magenta, true);
                        }
                    }
                    else
                    {
                        WriteToConsole("  release has no mediums", ConsoleColor.Magenta, true);
                    }
                }
                else
                {
                    WriteToConsole("  release group has no releases", ConsoleColor.Magenta, true);
                }
            }
            else
            {
                WriteToConsole("  release group not found", ConsoleColor.Magenta, true);
            }

        }
    }
}