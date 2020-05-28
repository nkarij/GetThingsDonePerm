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
    public class ListsController : ControllerBase
    {

        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;

        public ListsController(IListRepository listRepository, 
            IMapper mapper,
            SignInManager<StoreUser> signInManager,
            UserManager<StoreUser> userManager)
        {
            _listRepository = listRepository;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // this works fine
        // GET: api/<controller>
        [Route("[action]")]
        [HttpGet]
        public ActionResult GetAllLists()
        {
            try
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var result = _listRepository.GetAllLists();
                    //return Ok(result);
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


        // this works fine
        // GET: api/<controller>
        [HttpGet]
        public ActionResult GetAllListsByUser()
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = _listRepository.GetAllListsByUserName(userName);
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



        // GET api/<controller>/1
        // this works fine, except the itemlist is empty, do I have to add items before its full?
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ListRessource>> GetList(int id, bool includeTasks = true)
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = await _listRepository.GetList(id, true);
                    //return _mapper.Map<ListRessource>(result);
                    var ressource = _mapper.Map<ListRessource>(result);
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
        [HttpPost]
        public ActionResult Post([FromBody]List model)
        {
            try
            {
                // userName is not working ;/
                var userName = this.User.Identity.Name;
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    // call repo here, pass model
                    _listRepository.CreateList(model);
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
        [HttpPut]
        public ActionResult Put([FromBody] List model)
        {
            // userName is not working ;/
            var userName = this.User.Identity.Name;
            if (this.User.Identity.IsAuthenticated && userName != null)
            {
                _listRepository.UpdateList(model);
                return Ok();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
