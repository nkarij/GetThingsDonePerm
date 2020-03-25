using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Ressources;

namespace YDIAB.Models
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            this.CreateMap<Tag, TagRessource>();
        }
    }
}
