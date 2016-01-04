using System;
using System.Linq;
using System.Threading;
using Hub.Domain.Convert;
using Hub.Domain.Infrastructure;
using Memphis.BusinessLogic.Service;
using Memphis.Database.Infrastructure;
using Mercury.Console.Generate;
using MusicBrainz.Domain.Infrastructure;
using MusicBrainz.Enrich;
using Top3000Albums.Service;
using MusicBrainz.CoverArt;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net;
using AutoMapper;

namespace Mercury.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            HubStartup.Go();
            MbzStartup.Go();

            System.Console.ResetColor();

            //Test.HubContextTests.ListArtistsToConsole();
            //Test.HubContextTests.TestUnicode();
            //Test.HubContextTests.TestEmptyArtist();
            //Test.HubContextTests.TestMakeArtistsFromTop3000();
            //Test.HubContextTests.TestIdentityInsert();

            //Test.MusicBrainzTests.GetArtistIds();
            //Test.MusicBrainzTests.TestDatabaseArtist();
            //Test.MusicBrainzTests.TestDatabaseListArtists();
            //Test.MusicBrainzTests.TestSpecificArtistSearches();
            //Test.MusicBrainzTests.TestSerializeReleaseGroup();
            //Test.MusicBrainzTests.TestReleaseGroupWebService();
            //Test.MusicBrainzTests.TestArtistSearch();
            //Test.MusicBrainzTests.TestApostopheSearch();
            //Test.MusicBrainzTests.TestReleaseGroupQuery();
            //Test.MusicBrainzTests.TestDeserialzeUnicode();
            //Test.MusicBrainzTests.TestRecordingWebService();
            //Test.MusicBrainzTests.TestWorkWebService();
            //Test.MusicBrainzTests.TestArtistWebService();
            //Test.MusicBrainzTests.TestGetArtistByName();
            //Test.MusicBrainzTests.TestArtistWebServiceWithReleaseGroups();

            //Test.GeneralTests.TestReadAnsiFile();
            //Test.GeneralTests.TestReadLargeFile();
            //Test.GeneralTests.TestConsolePaging();

            //MusicBrainz.Populate.PopulateArtists.CreateSkeletonRecordsFromTop3k();
            //MusicBrainz.Populate.PopulateArtists.GetMbzIDs(true);
            //MusicBrainz.Domain.Xml.ArtistXml.Export(@"c:\temp\artists.xml");
            //MusicBrainz.Domain.Export.ArtistExport.Import(@"c:\temp\artists.xml");

            //Test.Top3000AlbumsTests.TestRead();
            //Test.Top3000AlbumsTests.TestOddCharacters();
            //Test.Top3000AlbumsTests.TestWrite();
            //AlbumService.AddAlbumIds();
            //Top3k.PopulateMbzArtistIds();
            //RunInThread(Top3k.PopulateMbzReleaseGroupIds);
            //RunInThread(Top3k.QueryAllReleaseGroups);
            //Top3k.PopulateOneAlbum(4);
            //Top3k.PopulateOneAlbum(10);

            //Gui.Main.Loop();

            //Hub.Domain.Convert.Products.ImportT3k(10);
            //Hub.Domain.Convert.Products.SaveProductsToXml(@"c:\temp\products.xml");
            /*System.Console.WriteLine("starting");
            Hub.Domain.Convert.Products.LoadProductsFromXml(@"c:\temp\products.xml");
            System.Console.WriteLine("done");*/

            //TimeThis(Products.ImportT3k);
            //Products.SaveProductsToXml(@"c:\temp\products.xml");
            //TimeThis(Hub.Domain.Convert.Products.LoadProductsFromXml);

            //Generate.SongsForMrm.Go();

            //ConvertArtistsForMemphis();
            //ConvertRecordingsForMemphis();

            //SaveDatabaseToXml();
            //LoadDatabaseFromXml();

            //TestSerializeCoverArt();
            //TestDeserializeCoverArt();
            //TestGetCoverArtQueryResult();
            //PopulateCoverArt();

            TestEntityFrameworkConnectivity();

            System.Console.WriteLine();
            System.Console.ResetColor();
            System.Console.WriteLine("press any key to exit...");
            System.Console.ReadKey();

            Top3k.Stopped = true;
        }

        private static void TestEntityFrameworkConnectivity()
        {
            var work = new UnitOfWork();
            var albums = work.Albums.Take(10);
            foreach (var album in albums)
            {
                System.Console.WriteLine($"{album.Title}");
            }
        }

        private static void ConvertRecordingsForMemphis()
        {
            var work = new UnitOfWork();
            var albumService = new AlbumService(work, Mapper.Engine);
            var recordingService = new RecordingService(work);
            var trackService = new TrackService(work);
            Top3kRecordingsForMemphis.Go(albumService, recordingService, trackService);
        }

        private static void PopulateCoverArt()
        {
            var work = new UnitOfWork();
            var albumService = new AlbumService(work, Mapper.Engine);
            CoverArtPopulator.Go(albumService);
        }

        private static void TestGetCoverArtQueryResult()
        {
            var album = new AlbumService(new UnitOfWork(), Mapper.Engine).Query(0, 1, "title", "Pet Sounds", "title", true).FirstOrDefault();
            if (album != null && album.MusicBrainzReleaseGroupId.HasValue)
            {
                System.Console.WriteLine("found album, querying cover art webservice...");
                var res = CoverArtWebService.GetByReleaseGroupId(album.MusicBrainzReleaseGroupId.Value);
                System.Console.WriteLine("Small Thumb: " + res.images[0].thumbnails.small);
            }
            else
            {
                System.Console.WriteLine("Pet Sounds not found");
            }
        }

        private static void TestDeserializeCoverArt()
        {
            WebClient wc = new WebClient();
            //wc.Encoding = System.Text.Encoding.UTF8;
            System.Console.WriteLine("querying...");
            string result = wc.DownloadString("http://coverartarchive.org/release-group/c31a5e2b-0bf8-32e0-8aeb-ef4ba9973932");
            System.Console.WriteLine("...complete. Result:");
            System.Console.WriteLine(result);
            System.Console.WriteLine();
            System.Console.WriteLine();
            var ser = new JavaScriptSerializer();
            var q = ser.Deserialize<CoverArtQueryResult>(result);
            System.Console.WriteLine("Small Thumb: " + q.images[0].thumbnails.small);
        }

        private static void TestSerializeCoverArt()
        {
            var q = new CoverArtQueryResult
            {
                images = new List<CoverArtImageData>
                {
                    new CoverArtImageData()
                    {
                        approved = true,
                        back = false,
                        comment = "",
                        edit = 20202510,
                        front = true,
                        id = "2860563776",
                        image = "http://coverartarchive.org/release/f7638b9b-a9aa-4c03-8734-9e692699f8b1/2860563776.jpg",
                        thumbnails= new CoverArtImageThumbnails()
                        {
                           large = "http://coverartarchive.org/release/f7638b9b-a9aa-4c03-8734-9e692699f8b1/2860563776-500.jpg",
                           small = "http://coverartarchive.org/release/f7638b9b-a9aa-4c03-8734-9e692699f8b1/2860563776-250.jpg"
                        },
                        types = new List<string>()
                        {
                           "Front"
                        }
                    }
                },
                release = "http://musicbrainz.org/release/f7638b9b-a9aa-4c03-8734-9e692699f8b1"
            };

            var ser = new JavaScriptSerializer();
            string json = ser.Serialize(q);

            System.Console.WriteLine(json);
        }

        private static void SaveDatabaseToXml()
        {
            var work = new UnitOfWork();
            var tagService = new TagService(work);
            var artistService = new ArtistService(work, tagService);
            var albumService = new AlbumService(work, Mapper.Engine);
            new Mercury.Console.BackupRestoreXml.Backup().Go(artistService, albumService);
        }

        private static void LoadDatabaseFromXml()
        {
            var work = new UnitOfWork();
            var tagService = new TagService(work);
            var artistService = new ArtistService(work, tagService);
            var albumService = new AlbumService(work, Mapper.Engine);
            new Mercury.Console.BackupRestoreXml.Restore().Go(artistService, tagService, albumService);
        }

        private static void ConvertArtistsForMemphis()
        {
            var work = new UnitOfWork();
            var tagService = new TagService(work);
            var artistService = new ArtistService(work, tagService);
            var releaseService = new AlbumService(work, Mapper.Engine);
            var recordingService = new RecordingService(work);
            var trackService = new TrackService(work);
            Top3kArtistsAndAlbumsToMemphis.Go(artistService, tagService, releaseService, recordingService, trackService);
        }

        static void RunInThread(Action target)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("press any key to stop...");
            System.Console.WriteLine();

            var thread = new Thread(new ThreadStart(target));
            thread.Start();
            while (!thread.IsAlive) ;
        }

        static void TimeThis(Action target)
        {
            var startTime = DateTime.Now;
            target();
            var endTime = DateTime.Now;
            System.Console.WriteLine("time elapsed: {0}", endTime - startTime);
        }
    }
}
