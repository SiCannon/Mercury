using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using Memphis.Database.Infrastructure;
using Memphis.Database.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memphis.Database.IntegrationTests
{
    [TestClass]
    public class ArtistTests
    {
        [TestMethod]
        public void Can_Get_Artists()
        {
            var context = new MemphisContext();
            var artists = context.Artists.Take(10).ToList();
            Assert.IsNotNull(artists);
            Debug.WriteLine("{0} artists found", artists.Count);
        }

        [TestMethod]
        public void Can_Add_Artist()
        {
            int savedId;

            using (var context = new MemphisContext())
            {
                var artist = new Artist
                {
                    Name = "Test",
                    SortName = "Test"
                };
                context.Artists.Add(artist);
                context.SaveChanges();

                Assert.IsNotNull(artist.ArtistId);
                Assert.AreNotEqual(0, artist.ArtistId);

                savedId = artist.ArtistId.Value;
            }

            using (var context = new MemphisContext())
            {
                var artist = context.Artists.SingleOrDefault(x => x.ArtistId == savedId);
                Assert.IsNotNull(artist);
            }

            // cleanup
            using (var context = new MemphisContext())
            {
                var artist = context.Artists.SingleOrDefault(x => x.ArtistId == savedId);
                context.Artists.Remove(artist);
                context.SaveChanges();
            }
        }
    }
}
