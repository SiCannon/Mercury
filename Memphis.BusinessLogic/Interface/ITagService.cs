using System.Collections.Generic;
using Memphis.Database.Entity;

namespace Memphis.BusinessLogic.Interface
{
    public interface ITagService
    {
        void Save(ref Tag tag, bool saveChanges = true);
        void Save(List<Tag> tags);
        void Save(List<string> tags);
        Tag GetById(int id);
        Tag GetByName(string name);
    }
}
