using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Models;

namespace YDIAB.Ressources
{
    public class ListRessource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ItemRessource> ItemList { get; set; }

        public ICollection<TagRessource> TagList { get; set; }
    }
}
