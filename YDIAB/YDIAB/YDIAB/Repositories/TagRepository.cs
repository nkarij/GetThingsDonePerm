using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Data;
using YDIAB.Models;

namespace YDIAB.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Tag> GetAllTags()
        {
            return _context.ListTags;
        }

        public Tag GetItemById(int id)
        {
            return _context.ListTags.SingleOrDefault(t => t.Id == id);
        }

        public async Task<Tag[]> GetTags(int id, bool includeTags = true)
        {

            IQueryable<Tag> query = _context.ListTags;

            if (includeTags)
            {
                query = query.Include(t => t.Name);
            }

            // Add Query
            query = query.Where(t => t.Item.Id == id);

            return await query.ToArrayAsync();
        }


        // get
        //public IEnumerable<Tag> GetAllTags()
        //{
        //    return _context.ListTags;
        //}

        // post

        //public class CreateTagInput
        //{
        //    public string name { get; set; }
        //}

        public void CreateTag(Tag tag)
        {
            var newTag = new Tag();
            newTag.Name = tag.Name;
            newTag.ItemId = tag.ItemId;
            var addTask = _context.ListTags.Add(newTag);
            _context.SaveChanges();
        }


        // put model
        public class UpdateTagInput
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        // put 
        public void UpdateTag(Tag tag)
        {
            //update syntax here
            var result = _context.ListTags.SingleOrDefault(l => l.Id == tag.Id);
            if (result != null)
            {
                result.Name = tag.Name;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Update Tag failed");
            }
        }

        // delete
        public Tag RemoveTagById(int id)
        {
            var removeTag = _context.ListTags.SingleOrDefault(i => i.Id == id);
            var result = _context.ListTags.Remove(removeTag);
            _context.SaveChanges();
            return removeTag;
        }


    }
}
