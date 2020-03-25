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
using YDIAB.Models;
using YDIAB.Repositories;
using YDIAB.Ressources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YDIAB.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }


        //GET: api/<controller>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemRessource>> GetById(int id, bool includeTags = true)
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = await _itemRepository.GetItemById(id, true);
                    var ressource = _mapper.Map<ItemRessource>(result);
                    return ressource;
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


        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Item model)
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    // call repo here, pass model
                    _itemRepository.CreateItem(model);
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
        [HttpPut("{id}")]
        public ActionResult Put([FromBody]Item model)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                _itemRepository.UpdateItemById(model);
                return Ok();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }
            
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                _itemRepository.RemoveItemById(id);
                return Ok();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }
        }
    }
}
