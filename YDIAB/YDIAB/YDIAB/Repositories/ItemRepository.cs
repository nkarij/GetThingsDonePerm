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
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Item> GetItemById(int id, bool includeTags)
        {

            IQueryable<Item> query = _context.ListItems;

            if (includeTags)
            {
                query = query.Include(t => t.Tags);
            }

            // Add Query
            query = query.Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        //public async Task<Tag[]> GetTags(int id, bool includeTags = true)
        //{

        //    IQueryable<Tag> query = _context.ListTags;

        //    //if (includeTags)
        //    //{
        //    //    query = query
        //    //      .Include(t => t.Name);
        //    //}

        //    // Add Query
        //    query = query
        //      .Where(t => t.Item.Id == id);

        //    return await query.ToArrayAsync();
        //}


        // get
        public Item GetItemAndTags(int id, bool includeTags = true)
        {
            return null;
            //var innerGroupJoinQuery =
            //    from item in _context.ListItems
            //    where item.Id == listid
            //    join tag in _context.ListTags on item.Id equals tag.ItemId into tagGroup
            //    select new { TaskTitle = item.Title, Tags = tagGroup };
            //return innerGroupJoinQuery;
            //til controller
            //innerGroupJoinQuery.SelectMany(g => g.Tags.Select(t => t.Name));
        }


        // post
        public void CreateItem(Item item, string username)
        {
            var newTask = new Item();
            newTask.Title = item.Title;
            newTask.ListId = item.ListId;
            _context.ListItems.Add(newTask);
            _context.SaveChanges();
        }


        // put 
        public Item UpdateItemByModel(Item item)
        {
            //update syntax here
            var result = _context.ListItems.SingleOrDefault(i => i.Id == item.Id);
            if (result != null)
            {
                result.Title = item.Title;
                _context.SaveChanges();
                return result;
            } else
            {
                throw new Exception("Update Item failed");
            }
        }

        public Item UpdateSelectedItem(Item model)
        {
            //update syntax here
            var result = _context.ListItems.SingleOrDefault(i => i.Id == model.Id);
            if (result != null)
            {
                result.IsDone = model.IsDone;
                _context.SaveChanges();
                return result;
            }
            else
            {
                throw new Exception("Update Item failed");
            }
        }

        // delete
        public Item RemoveItemById(int id)
        {
            //remove syntax here
            var removeItem = _context.ListItems.SingleOrDefault(i => i.Id == id);
            if(removeItem != null)
            {
                var result = _context.ListItems.Remove(removeItem);
                _context.SaveChanges();
                return removeItem;
            } else
            {
                throw new Exception("Remove Item Failed");
            }
        }

        // save 
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<ICollection<Item>> GetAllTasksBySearchTermAsync(string term)
        {
            IQueryable<Item> query = _context.ListItems;
            query = query
              .Include(c => c.Tags);
            query = query.Where(t => t.Title == term);
            return await query.ToArrayAsync();

        }

    }
}
