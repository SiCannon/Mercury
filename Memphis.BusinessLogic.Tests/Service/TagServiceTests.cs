using System.Collections.Generic;
using System.Linq;
using Memphis.BusinessLogic.Service;
using Memphis.BusinessLogic.Tests.Fake;
using Memphis.Database.Infrastructure;
using Memphis.Database.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Memphis.BusinessLogic.Tests.Service
{
    [TestClass]
    public class TagServiceTests
    {
        [TestMethod]
        public void Save_New_When_None_Existing()
        {
            var tags = new InMemoryDbSet<Tag>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Tags).Returns(tags);
            mockUnitOfWork.Setup(x => x.GetDbSet<Tag>()).Returns(tags);

            var tagService = new TagService(mockUnitOfWork.Object);
            var newTag = new Tag { Name = "one" };
            tagService.Save(ref newTag);

            Assert.AreEqual(1, tags.Count());
        }

        [TestMethod]
        public void Save_New_When_One_Existing()
        {
            var tags = new InMemoryDbSet<Tag>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Tags).Returns(tags);
            mockUnitOfWork.Setup(x => x.GetDbSet<Tag>()).Returns(tags);

            tags.Add(new Tag { TagId = 1, Name = "one" });

            var tagService = new TagService(mockUnitOfWork.Object);
            var newTag = new Tag { Name = "two" };
            tagService.Save(ref newTag);

            Assert.AreEqual(2, tags.Count());
        }

        [TestMethod]
        public void Save_New_Returns_Existing_When_Name_Matches()
        {
            var tags = new InMemoryDbSet<Tag>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Tags).Returns(tags);
            mockUnitOfWork.Setup(x => x.GetDbSet<Tag>()).Returns(tags);

            tags.Add(new Tag { TagId = 4, Name = "one" });

            var tagService = new TagService(mockUnitOfWork.Object);
            var newTag = new Tag { Name = "one" };
            tagService.Save(ref newTag);

            Assert.AreEqual(1, tags.Count());
            Assert.AreEqual(4, newTag.TagId);
        }

        [TestMethod]
        public void Save_New_List_When_None_Existing()
        {
            var tags = new InMemoryDbSet<Tag>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Tags).Returns(tags);
            mockUnitOfWork.Setup(x => x.GetDbSet<Tag>()).Returns(tags);

            var tagService = new TagService(mockUnitOfWork.Object);
            var newTags = new List<Tag>
            {
                new Tag { Name = "one" },
                new Tag { Name = "two" }
            };
            tagService.Save(newTags);

            Assert.AreEqual(2, tags.Count());
            Assert.IsTrue(tags.Any(x => x.Name == "one"));
            Assert.IsTrue(tags.Any(x => x.Name == "two"));
        }

        [TestMethod]
        public void Save_New_List_When_Some_Existing()
        {
            var tags = new InMemoryDbSet<Tag>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Tags).Returns(tags);
            mockUnitOfWork.Setup(x => x.GetDbSet<Tag>()).Returns(tags);
            tags.Add(new Tag { TagId = 1, Name = "one" });
            tags.Add(new Tag { TagId = 2, Name = "two" });

            var tagService = new TagService(mockUnitOfWork.Object);
            var newTags = new List<Tag>
            {
                new Tag { Name = "three" },
                new Tag { Name = "four" }
            };
            tagService.Save(newTags);

            Assert.AreEqual(4, tags.Count());
            Assert.IsTrue(tags.Any(x => x.Name == "one"));
            Assert.IsTrue(tags.Any(x => x.Name == "two"));
            Assert.IsTrue(tags.Any(x => x.Name == "three"));
            Assert.IsTrue(tags.Any(x => x.Name == "four"));
        }

        [TestMethod]
        public void Save_Mixed_List_When_Some_Existing()
        {
            var tags = new InMemoryDbSet<Tag>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Tags).Returns(tags);
            mockUnitOfWork.Setup(x => x.GetDbSet<Tag>()).Returns(tags);
            tags.Add(new Tag { TagId = 10, Name = "one" });
            tags.Add(new Tag { TagId = 20, Name = "two" });

            var tagService = new TagService(mockUnitOfWork.Object);
            var newTags = new List<Tag>
            {
                new Tag { Name = "two" },
                new Tag { Name = "three" }
            };
            tagService.Save(newTags);

            Assert.AreEqual(3, tags.Count());
            Assert.IsTrue(tags.Any(x => x.Name == "one"));
            Assert.IsTrue(tags.Any(x => x.Name == "two"));
            Assert.IsTrue(tags.Any(x => x.Name == "three"));
            Assert.AreEqual(20, newTags[0].TagId);
        }
    }
}
