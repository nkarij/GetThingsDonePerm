using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Ressources;

namespace YDIAB.Models
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            this.CreateMap<Item, ItemRessource>();
        }
    }
}
