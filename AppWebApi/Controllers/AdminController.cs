using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Models.DTO;
using Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "supusr")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        loginUserSessionDto _usr = null;

        IFriendsService _friendService = null;
        ILoginService _loginService = null;
        ILogger<AdminController> _logger;

        //GET: api/admin/seed?count={count}
        [HttpGet()]
        [ActionName("Seed")]
        [ProducesResponseType(200, Type = typeof(adminInfoDbDto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Seed(string count)
        {
            if (!int.TryParse(count, out int _count))
            {
                return BadRequest("count format error");
            }

            adminInfoDbDto _info = await _friendService.SeedAsync(_usr, _count);
            return Ok(_info);
        }

        //GET: api/admin/removeseed
        [HttpGet()]
        [ActionName("RemoveSeed")]
        [ProducesResponseType(200, Type = typeof(adminInfoDbDto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> RemoveSeed(string seeded = "true")
        {
            if (!bool.TryParse(seeded, out bool _seeded))
            {
                return BadRequest("count format error");
            }

            try
            {
                adminInfoDbDto _info = await _friendService.RemoveSeedAsync(_usr, _seeded);
                return Ok(_info);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        //GET: api/admin/seeduser?users={count}&superusers={count}
        [HttpGet()]
        [ActionName("SeedUsers")]
        [ProducesResponseType(200, Type = typeof(usrInfoDto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SeedUsers(string countUsr = "32", string countSupUsr = "2")
        {
            if (!int.TryParse(countUsr, out int _countUsr))
            {
                return BadRequest("count format error");
            }
            if (!int.TryParse(countSupUsr, out int _countSupUsr))
            {
                return BadRequest("count format error");
            }

            try
            {
                usrInfoDto _info = await _loginService.SeedAsync(_countUsr, _countSupUsr);
                return Ok(_info);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        #region constructors
        /*
        public AdminController(IFriendsService friendService, ILogger<AdminController> logger)
        {
            _friendService = friendService;
            _logger = logger;
        }
        */
        public AdminController(IFriendsService friendService, ILoginService loginService, ILogger<AdminController> logger)
        {
            _friendService = friendService;
            _loginService = loginService;

            _logger = logger;
        }
        #endregion
    }
}

