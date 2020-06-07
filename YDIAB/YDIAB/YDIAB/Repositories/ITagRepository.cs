using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YDIAB.Models;
using static YDIAB.Repositories.TagRepository;

namespace YDIAB.Repositories
{
    public interface ITagRepository
    {
        
        IEnumerable<Tag> GetAllTags();

        Tag GetItemById(int id);
        Task<Tag[]> GetTags(int id, bool includeTags = true);

        void CreateTag(Tag tag);
        public void UpdateTag(Tag tag);
        //public void UpdateTag(UpdateTagInput model);
        public Tag RemoveTagById(int id);

        
    }
}
