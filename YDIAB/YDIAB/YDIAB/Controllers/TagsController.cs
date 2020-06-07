using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YDIAB.Models;
using YDIAB.Repositories;
using YDIAB.Ressources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YDIAB.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : ControllerBase
    {

        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TagsController> _logger;

        public TagsController(ITagRepository tagRepository, IMapper mapper, ILogger<TagsController> logger)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Tag>> Get(){

            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                var result = _tagRepository.GetAllTags();
                if (result != null)
                {
                    return Ok(result.ToList());
                }
                else
                {
                    throw new Exception("No Tags");
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }

        }

        //GET: api/<controller>
        //virker fint.
        [HttpGet("{id:int}")]
        public ActionResult<Tag> GetById(int id)
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = _tagRepository.GetItemById(id);
                    return Ok(result);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST api/<controller>
        // virker fint
        [HttpPost]
        public ActionResult Post([FromBody]Tag model)
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    // call repo here, pass model
                    _tagRepository.CreateTag(model);
                    return Ok();
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }


        // PUT api/<controller>/5
        // virker fint
        // TODO : Mangler at implementere try catch. 
        // TODO: SKal også sætte tags op til at referere til listId.
        // TODO: LIste med alle tasks plus tags.
        // TODO: Task som kan tjekkes af (done).
        [HttpPut]
        public ActionResult Put([FromBody]Tag model)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                // call repo here, pass model
                _tagRepository.UpdateTag(model);
                return Ok();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }
            
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<Tag> Delete(int id)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                // call repo here, pass model
                var result = _tagRepository.RemoveTagById(id);
                return Ok(result.ItemId);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }
        }


    }
}
