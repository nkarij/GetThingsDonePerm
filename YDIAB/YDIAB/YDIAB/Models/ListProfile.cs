using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YDIAB.Ressources;

namespace YDIAB.Models
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            this.CreateMap<List, ListRessource>();
        }
    }
}
