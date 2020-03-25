using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDIAB.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }

        // represents foreign key
        public int ListId { get; set; }
        // navigation object...
        public List List { get; set; }

        // 1-to-many relation
        public ICollection<Tag> Tags { get; set; }

    }
}
