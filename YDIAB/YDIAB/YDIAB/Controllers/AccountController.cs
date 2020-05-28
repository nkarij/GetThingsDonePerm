﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YDIAB.Models;
using YDIAB.Data;
using Microsoft.AspNetCore.Http;

namespace YDIAB.Controllers
{
	[Route("api/[controller]")]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

		[HttpGet]
		[Route("[action]")]
		public ActionResult Login()
		{
			if (this.User.Identity.IsAuthenticated)
			{
				return Ok();
			}
			else
			{
				return BadRequest("login with credentials");
			}
		}

		//public class LoginCredentials
		//{
		//	public string username { get; set; }

		//	public string password { get; set; }
		//	public bool rememberUser { get; set; }
		//}


		//[HttpPost]
		//public async Task<ActionResult> LoginAsync([FromBody]LoginCredentials user)
		//{
		//	try
		//	{
		//		//PasswordSignInAsync (string userName, string password, bool isPersistent, bool lockoutOnFailure);
		//		var result = await _signInManager.PasswordSignInAsync(user.username, user.password, user.rememberUser, false);
		//		if (result.Succeeded)
		//		{
		//			// should pass this? to some repository, maybe the ListRepository?
		//			return Ok();
		//		} else
		//		{
		//			return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized request");
		//		}
		//	}
		//	catch (Exception)
		//	{

		//		throw new Exception("error");
		//	}
		//}

		// this is effectively the login method.
		[HttpPost]
		[Route("[action]")]
		public async Task<ActionResult> CreateToken([FromBody] StoreUser userModel)
		{
		
				if (ModelState.IsValid)
				{
					//var user = await _userManager.FindByNameAsync(userModel.UserName);
					var user = await _userManager.FindByEmailAsync(userModel.UserName);
				if (user != null)
					{
						// i dont know if the login should be placed in here, or they token plus login be seperated into 2 methods, 
						// but the methods are called in the same place. Returns false???
						var signIn = await _signInManager.CheckPasswordSignInAsync(user, user.Password, false);
						if (signIn.Succeeded) {
						// sub = subject
						// Jti = 
							var claims = new[]
								{
								new Claim(JwtRegisteredClaimNames.Sub, user.Email),
								new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
								new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
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
					return BadRequest();
				}

				return BadRequest();
			}

			return BadRequest();
		}
		
		[HttpGet]
		[Route("[action]")]
		public async Task<ActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Account", "CreateToken");
		}
	} // controller ends
}