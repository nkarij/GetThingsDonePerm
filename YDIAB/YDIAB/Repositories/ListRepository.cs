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
    public class ListRepository : IListRepository
    {
        private readonly AppDbContext _context;

        public ListRepository(AppDbContext context)
        {
            _context = context;
        }

        // get
        public IEnumerable<List> GetAllLists()
        {
            return _context.Lists;
        }

        public IEnumerable<List> GetAllListsByUserName(string username)
        {
            var result = _context.Lists.Where(l => l.User.UserName == username);
            return result;
        }

        public async Task<List> GetList(int id, bool includeTasks = true)
        {
            IQueryable<List> query = _context.Lists;

            if (includeTasks)
            {
                // the include is where the automapper problem is...
                query = query.Include(c => c.ItemList);
                    //.ThenInclude(t => t.Tags);
            }

            // Query It with id
            query = query.Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        //public async Task<Item[]> GetItems(int id, bool includeTags = true)
        //{

        //    IQueryable<Item> query = _context.ListItems;

        //    if (includeTags)
        //    {
        //        query = query
        //          .Include(t => t.Tags);
        //    }

        //    // Add Query
        //    query = query
        //      .Where(t => t.List.Id == id);

        //    return await query.ToArrayAsync();
        //}



        // post
        public void CreateList(List list)
        {
            var newList = new List();
            newList.Name = list.Name;
            newList.Description = list.Description;
            var addTask = _context.Lists.Add(newList);
            _context.SaveChanges();
        }


        // put 
        public void UpdateList(List list)
        {
            //update syntax here
            var result = _context.Lists.SingleOrDefault(i => i.Id == list.Id);
            if (list.Name != null)
            {
                result.Name = list.Name;
                _context.SaveChanges();

                if(list.Description != null)
                {
                    result.Description = list.Description;
                    _context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Update List failed");
            }
        }

        // delete
        public void RemoveListById(int id)
        {
            var removeList = _context.Lists.SingleOrDefault(i => i.Id == id);
            var result = _context.Lists.Remove(removeList).Entity;
            _context.SaveChanges();
        }

        // save 
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
