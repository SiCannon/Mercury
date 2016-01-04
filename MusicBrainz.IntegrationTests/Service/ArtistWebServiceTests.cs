using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicBrainz.WebService.Service;

namespace MusicBrainz.IntegrationTests.Service
{
    [TestClass]
    public class ArtistWebServiceTests
    {
        [TestMethod]
        public void Can_QueryByName()
        {
            var result = ArtistWebService.QueryByName("Led Zeppelin");
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Artists);
            Assert.IsTrue(result.Artists.Count > 0);
        }

        [TestMethod]
        public void Can_FindByName()
        {
            var result = ArtistWebService.FindByName("The Beach Boys");
            Assert.IsNotNull(result);
            Assert.AreEqual("The Beach Boys", result.Name);
            Assert.AreEqual(new Guid("ebfc1398-8d96-47e3-82c3-f782abcdb13d"), result.ArtistId);
            Assert.IsNotNull(result.Tags);
            Assert.IsTrue(result.Tags.Count > 0);
            result.Tags.ForEach(t => Debug.WriteLine("{0} {1}", t.Name, t.Count));
        }

        [TestMethod]
        public void FindByName_Fails_When_No_Match()
        {
            var result = ArtistWebService.FindByName("This Name Does Not Exist : 44e69903-675a-4089-9bac-b054059ef2d0");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindByName_Fails_When_Duplicates()
        {
            var result = ArtistWebService.FindByName("Nirvana");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_FindByName_With_Hint_1()
        {
            var result = ArtistWebService.FindByName("Nirvana", "Nevermind");
            Assert.IsNotNull(result);
            Assert.AreEqual("Nirvana", result.Name);
            Assert.AreEqual(new Guid("5b11f4ce-a62d-471e-81fc-a69a8278c7da"), result.ArtistId);
        }

        [TestMethod]
        public void Can_GetByName_With_Hint_2()
        {
            /*var oasisGuid = new Guid("39ab1aed-75e0-4140-bd47-540276886b60");

            var artist = ArtistWebService.Query(oasisGuid, true);
            Assert.IsNotNull(artist);
            Assert.IsNotNull(artist.ReleaseGroups);
            Assert.IsTrue(artist.ReleaseGroups.Count > 0);

            foreach (var rg in artist.ReleaseGroups)
            {
                Debug.WriteLine(rg.Title);
            }*/
            
            string nameT3k = "(What's the Story) Morning Glory?";
            string nameMbz = "(What’s the Story) Morning Glory?";

            //Assert.IsTrue(nameT3k.Equals(nameMbz, StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(0, string.Compare(nameT3k, nameMbz, new CultureInfo("en-US"), CompareOptions.IgnoreSymbols));

            //Assert.AreEqual(, );

            //var result = ArtistWebService.GetByName("Oasis", "(What's the Story) Morning Glory?");
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Oasis", result.Name);
            //Assert.AreEqual(, result.ArtistId);
        }

        [TestMethod]
        public void Can_GetById()
        {
            var result = ArtistWebService.GetById(new Guid("b10bbbfc-cf9e-42e0-be17-e2c3e1d2600d"));
            Assert.IsNotNull(result);
            Assert.AreEqual("The Beatles", result.Name);
        }

        [TestMethod]
        public void Can_GetById_Including_ReleaseGroups()
        {
            var result = ArtistWebService.GetById(new Guid("b10bbbfc-cf9e-42e0-be17-e2c3e1d2600d"), true);
            Assert.IsNotNull(result);
            Assert.AreEqual("The Beatles", result.Name);
            Assert.IsNotNull(result.ReleaseGroups);
            Assert.IsTrue(result.ReleaseGroups.Count > 0);
        }

    }
}
