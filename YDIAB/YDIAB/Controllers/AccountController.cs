using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YDIAB.Models;
using YDIAB.Data;

namespace YDIAB.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private readonly SignInManager<StoreUser> _signInManager;
		private readonly UserManager<StoreUser> _userManager;
		private readonly IConfiguration _config;
		private readonly AppDbContext _context;

		public AccountController(ILogger<AccountController> logger,
			AppDbContext context,
			SignInManager<StoreUser> signInManager,
			UserManager<StoreUser> userManager,
			IConfiguration config)
		{
			_logger = logger;
			_signInManager = signInManager;
			_userManager = userManager;
			_config = config;
			_context = context;
		}
		public IConfiguration config { get; }

		
		[HttpGet]
		public ActionResult Get()
		{
			if (this.User.Identity.IsAuthenticated)
			{
				return Ok();
			} else
			{
				return BadRequest("login with credentials");
			}
		}

		//	THIS IS ONLY USED TO SEED THE TEST USERS ID TO THE LIST WITH ID 1.
		// ONLY USE THIS ONCE FROM POSTMAN /API/ACCOUNT/ID IF YOU HAVE DROPPED THE DATABASE.
		// IF YOU HAVE NOT DROPPED THE DATABASE, DONT USE THIS METHOD.
		//[HttpGet("{id:int}")]
		//public async Task<ActionResult> ConnectListAndUser(int id)
		//{
		//	var user = await _userManager.FindByEmailAsync("tester@test.com");
		//	var list = _context.Lists.SingleOrDefault(l => l.Id == id);
		//	list.User = user;
		//	_context.SaveChanges();
		//	return Created("", list);
		//}

		[HttpPost]
		public async Task<ActionResult> CreateToken([FromBody] StoreUser model)
		{
			if (ModelState.IsValid)
			{
				// the user manager does not find my user... Why, Why, Why???
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user != null)
				{
					var claims = new[]
					{
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					//new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
				};

					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
					var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

					var token = new JwtSecurityToken(
					  _config["Tokens:Issuer"],
					  _config["Tokens:Audience"],
					  claims,
					  expires: DateTime.Now.AddMinutes(30),
					  signingCredentials: creds
					  );

					var results = new
					{
						token = new JwtSecurityTokenHandler().WriteToken(token),
						expiration = token.ValidTo
					};

					return Created("", results);

				}
			}

			return BadRequest();
		}

	}

}