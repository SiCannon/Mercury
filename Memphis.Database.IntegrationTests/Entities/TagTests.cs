using System;
using System.Linq;
using Memphis.Database.Infrastructure;
using Memphis.Database.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memphis.Database.IntegrationTests.Entities
{
    [TestClass]
    public class TagTests
    {
        [TestMethod]
        public void Can_Add_Tag()
        {
            int savedId;

            using (var context = new MemphisContext())
            {
                var tag = new Tag
                {
                    Name = "Test"
                };
                context.Tags.Add(tag);
                context.SaveChanges();

                Assert.IsNotNull(tag.TagId);
                Assert.AreNotEqual(0, tag.TagId);

                savedId = tag.TagId.Value;
            }

            using (var context = new MemphisContext())
            {
                var tag = context.Tags.SingleOrDefault(x => x.TagId == savedId);
                Assert.IsNotNull(tag);
            }

            // cleanup
            using (var context = new MemphisContext())
            {
                var tag = context.Tags.SingleOrDefault(x => x.TagId == savedId);
                context.Tags.Remove(tag);
                context.SaveChanges();
            }
        }
    }
}
