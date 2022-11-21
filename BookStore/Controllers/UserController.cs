using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[Controller]s")]
	public class UserController:ControllerBase
	{
		private readonly IBookStoreDbContext _context;
		private readonly IMapper _mapper;
		readonly IConfiguration _configuration;

		public UserController(IBookStoreDbContext context,IConfiguration configuration,IMapper mapper)
		{
			_context = context;
			_configuration = configuration;
			_mapper = mapper;
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody]CreateUserModel newUser)
		{
			CreateUserCommand command = new CreateUserCommand(_context, _mapper);
			command.Model = newUser;
			command.Handle();
			return Ok();
		}
	}
}

