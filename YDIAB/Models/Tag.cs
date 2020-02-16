using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDIAB.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

    }
}
