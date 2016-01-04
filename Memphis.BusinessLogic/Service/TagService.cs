using System.Collections.Generic;
using System.Linq;
using Memphis.BusinessLogic.Base;
using Memphis.BusinessLogic.Interface;
using Memphis.Database.Infrastructure;
using Memphis.Database.Entity;

namespace Memphis.BusinessLogic.Service
{
    public class TagService : BaseService, ITagService
    {
        public TagService(IUnitOfWork work) : base(work) { }

        public void Save(ref Tag tag, bool saveChanges = true)
        {
            if (tag.IsNew)
            {
                var existingTag = GetByName(tag.Name);
                if (existingTag != null)
                {
                    tag = existingTag;
                    return;
                }
            }

            InternalSave(tag, saveChanges);
        }

        public void Save(List<Tag> tags)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                var tag = tags[i];
                Save(ref tag, false);
                tags[i] = tag;
            }
            work.SaveChanges();
        }

        public void Save(List<string> tags)
        {
            foreach (var t in tags)
            {
                var tag = new Tag { Name = t };
                Save(ref tag, false);
            }
            work.SaveChanges();
        }

        public Tag GetById(int id)
        {
            return work.Tags.SingleOrDefault(t => t.TagId == id);
        }

        public Tag GetByName(string name)
        {
            return work.Tags.SingleOrDefault(t => t.Name == name);
        }
    }
}
