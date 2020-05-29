using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YDIAB.Models;
using static YDIAB.Repositories.ListRepository;
using System.Security.Claims;

namespace YDIAB.Repositories
{
    public interface IListRepository
    {

        IEnumerable<List> GetAllLists();

        public IEnumerable<List> GetAllListsByUserName(string username);
        public Task<List> GetList(int id, bool includeTasks = true);

        //Task<Item[]> GetItems(int id, bool includeTags = true);

        //public void CreateList(List list, string username);

        //public IEnumerable<List> CreateList(List list, string username);
        public ICollection<List> CreateList(List list, string username, StoreUser user);

        public List UpdateList(List list);

        public ICollection<List> RemoveListById(int id, string username);
    }
}
