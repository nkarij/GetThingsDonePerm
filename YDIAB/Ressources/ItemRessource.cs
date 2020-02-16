using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Models;

namespace YDIAB.Ressources
{
    public class ItemRessource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }

        public int ListId { get; set; }
        public ICollection<TagRessource> Tags { get; set; }
    }
}
