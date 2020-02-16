using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YDIAB.Models;
using YDIAB.Repositories;
using YDIAB.Ressources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YDIAB.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {

        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagsController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IEnumerable<Tag> Get(){
            var result = _tagRepository.GetAllTags();
            if(result != null){
                return result;
            } else {
                throw new Exception("No Tags");
            }
        }

        //GET: api/<controller>
        [HttpGet("{id:int}")]
        public ActionResult<Tag> GetById(int id)
        {
            try
            {
                var result =  _tagRepository.GetItemById(id);
                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Tag model)
        {
            try
            {
                // call repo here, pass model
               _tagRepository.CreateTag(model);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put([FromBody]Tag model)
        {
            _tagRepository.UpdateTag(model);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _tagRepository.RemoveTagById(id);
        }
    }
}
