using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDIAB.Models
{
    public class List
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public StoreUser User { get; set; }

        // navigation object? 1 to many
        public ICollection<Item> ItemList { get; set; }

    }
}
