using System.Diagnostics;
using System.Linq;
using Memphis.Database.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memphis.Database.IntegrationTests.Entities
{
    [TestClass]
    public class AlbumTests
    {
        [TestMethod]
        public void Can_Get_Album_Tracks()
        {
            var context = new MemphisContext();
            var album = context.Albums.Single(x => x.AlbumId == 1);
            Assert.IsTrue(album.Tracks.Count > 0);
            foreach (var t in album.Tracks)
            {
                Debug.WriteLine(t.Recording.Title);
            }
        }
    }
}
