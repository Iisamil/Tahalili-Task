using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Core.Entity;
using News.Core.Repos;
using News.Core.Services;
using News.Core.Specifications;
using NewsApi.Dtos;
using News.Repository.Data.Helper;
namespace NewsApi.Controllers
{
	public class UserController : BaseApiController
	{

		private readonly ITokenService _tokenService;
		private readonly UserManager<WebUser> _userManager;
		private readonly SignInManager<WebUser> _signInManager;

		public UserController(ITokenService tokenService,
			UserManager<WebUser> userManager,
			SignInManager<WebUser> signInManager)
		{

			_tokenService = tokenService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{

			{
				var user = await _userManager.FindByEmailAsync(loginDto.UserName + "@gmail.com");

				if (user == null) return Unauthorized();

				var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

				if (!result.Succeeded) return Unauthorized();

				return Ok(
					await _tokenService.CreateToken(user, _userManager)
				);

				#region MyRegion
				//var spec = new UserSpecifications(user.UserName);
				//var result = await _userRepository.FindEntityWithSpec(spec);
				//if (result == null)
				//	return BadRequest();
				//if (Encrption.DecodePassword(result.Password) != user.Password)
				//	return BadRequest();
				//return Ok(new UserDto()
				//{
				//	UserName = user.UserName,
				//	Password = user.Password,
				//	Token = await _tokenService.CreateToken(user, _userManager)
				//});
				#endregion
			}

		}
		
	}
}
