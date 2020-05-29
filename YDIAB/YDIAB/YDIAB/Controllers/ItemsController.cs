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
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(
            ILogger<ItemsController> logger, 
            IItemRepository itemRepository, 
            IMapper mapper)
        {
            
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
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
                    var result = await _itemRepository.GetItemById(id, includeTags);
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



        // POST api/<controller>
        // works fine
        [HttpPost]
        public ActionResult Post([FromBody]Item model)
        {
            try
            {
                
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    // call repo here, pass model
                    _itemRepository.CreateItem(model, userName);
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
        // works fine
        [HttpPut]

        public ActionResult<Item> Put([FromBody]Item model)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            try
            {
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = _itemRepository.UpdateItemByModel(model);
                    return Ok(result);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to updage task: {ex}");
            }

            return BadRequest("Failed to update Task");


        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<Item> PutSelectedItem([FromBody]Item model)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            try
            {
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = _itemRepository.UpdateSelectedItem(model);
                    return Ok(result);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to updage task: {ex}");
            }

            return BadRequest("Failed to update Task");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<Item> Delete(int id)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                var result = _itemRepository.RemoveItemById(id);
                // returning listId
                return Ok(result.ListId);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }
        }
    }
}
