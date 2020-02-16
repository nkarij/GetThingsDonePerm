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

        // GET: api/<controller>
        [HttpGet]
        public ActionResult GetAllLists()
        {
            try
            {
                // this is not working ;/
                var userName = this.User.Identity.Name;
                if(userName != null)
                {
                    var result = _listRepository.GetAllListsByUserName(userName);
                    return Ok(result);
                } else
                {
                    var result = _listRepository.GetAllLists();
                    return Ok(result);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET api/<controller>/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ListRessource>> GetList(int id, bool includeTasks = true)
        {
            try
            {
                var result = await _listRepository.GetList(id, true);
                //return _mapper.Map<ListRessource>(result);
                var ressource = _mapper.Map<ListRessource>(result);
                return ressource;
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
                // call repo here, pass model
                _listRepository.CreateList(model);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }


        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody] List model)
        {
            _listRepository.UpdateList(model);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
