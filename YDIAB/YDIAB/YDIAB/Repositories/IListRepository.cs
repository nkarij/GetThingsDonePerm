using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YDIAB.Models;
using static YDIAB.Repositories.ListRepository;

namespace YDIAB.Repositories
{
    public interface IListRepository
    {

        IEnumerable<List> GetAllLists();

        IEnumerable<List> GetAllListsByUserName(string username);
        Task<List> GetList(int id, bool includeTasks = true);

        //Task<Item[]> GetItems(int id, bool includeTags = true);

        public void CreateList(List list);

        public void UpdateList(List list);

        public void RemoveListById(int id);

        public bool SaveAll();
    }
}
