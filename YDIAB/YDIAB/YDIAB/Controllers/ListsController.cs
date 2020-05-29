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
    public class ListsController : ControllerBase
    {

        private readonly ILogger<ListsController> _logger;
        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IAccountRepository _accountRepository;

        public ListsController(
            ILogger<ListsController> logger,
            IListRepository listRepository,
            IAccountRepository accountRepository,
            IMapper mapper,
            SignInManager<StoreUser> signInManager,
            UserManager<StoreUser> userManager
            )
        {
            _listRepository = listRepository;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _accountRepository = accountRepository;
        }

        // this works fine, but is not used, since it get all users lists.
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


        // works fine
        [HttpPost]
        public async Task<ActionResult<ICollection<List>>> Post([FromBody]List model)
        {
            try
            {
                
                var userName = this.User.Identity.Name;
                var user = await _userManager.FindByEmailAsync(userName);
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    // call repo here, pass model
                    var result = _listRepository.CreateList(model, userName, user);
                    // save all fixes the model so that it contains the id from the database.
                    //return Created($"/api/lists/{model.Id}", model);
                    return Created($"/api/lists", result);
                }
                else
                {
                    
                    return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new list: {ex}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure, list was not created");
            }

            

        }

        //  works fine!
        [HttpPut]
        public ActionResult<List> Put([FromBody] List model)
        {
            var userName = this.User.Identity.Name;
            try
            {
                
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = _listRepository.UpdateList(model);
                    return Ok(result);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new list: {ex}");
            }

            return BadRequest("Failed to create new list");

        }


        // works fine
        [HttpDelete("{id}")]
        public ActionResult<ICollection<List>> Delete(int id)
        {
            var userName = this.User.Identity.Name;
            try
            {
                if (this.User.Identity.IsAuthenticated && userName != null)
                {
                    var result = _listRepository.RemoveListById(id, userName);
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to create new list: {ex}");
            }

            return BadRequest("Failed to remove list");
        }

        
    }
}
