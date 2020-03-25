using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YDIAB.Models;
using static YDIAB.Repositories.ItemRepository;

namespace YDIAB.Repositories
{
    public interface IItemRepository
    {
        public Item GetItemAndTags(int id, bool includeTags = true);

        //public void GetItemAndTags(int listid);
        Task<Item> GetItemById(int id, bool includeTags = true);
        //Task<Tag[]> GetTags(int id, bool includeTags = true);
        public void CreateItem(Item item);
        public void UpdateItemById(Item item);
        //public void UpdateItemById(UpdateItemInput model);
        public void RemoveItemById(int id);
        public bool SaveAll();

    }
}
