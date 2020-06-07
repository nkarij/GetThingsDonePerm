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
        public void CreateItem(Item item, string username);
        public Item UpdateItemByModel(Item item);
        //public void UpdateItemById(UpdateItemInput model);

        public Item UpdateSelectedItem(Item item);
        public Item RemoveItemById(int id);
        public bool SaveAll();
        public Task<ICollection<Item>> GetAllTasksBySearchTermAsync(string term);

    }
}
