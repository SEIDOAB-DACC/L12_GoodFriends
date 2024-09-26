using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Configuration;
using Models;
using Models.DTO;

using Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FriendsController : Controller
    {
        loginUserSessionDto _usr = null;

        IFriendsService _service = null;
        ILogger<FriendsController> _logger = null;

        //GET: api/friends/read
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "usr, supusr")]
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(csRespPageDto<IFriend>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read(string seeded = "true", string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool _seeded = bool.Parse(seeded);
                bool _flat = bool.Parse(flat);
                int _pageNr = int.Parse(pageNr);
                int _pageSize = int.Parse(pageSize);

                var _resp = await _service.ReadFriendsAsync(_usr, _seeded, _flat, filter?.Trim()?.ToLower(), _pageNr, _pageSize);     
                return Ok(_resp);     
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET: api/friends/readitem
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
           Policy = null, Roles = "usr, supusr")]
        [HttpGet()]
        [ActionName("ReadItem")]
        [ProducesResponseType(200, Type = typeof(IFriend))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var _id = Guid.Parse(id);
                bool _flat = bool.Parse(flat);

                var item = await _service.ReadFriendAsync(_usr, _id, _flat);
                if (item == null)
                {
                    return BadRequest($"Item with id {id} does not exist");
                }

                return Ok(item);         
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "supusr")]
        //DELETE: api/friends/deleteitem/id
        [HttpDelete("{id}")]
        [ActionName("DeleteItem")]
        [ProducesResponseType(200, Type = typeof(IFriend))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                var _id = Guid.Parse(id);
                
                var item = await _service.DeleteFriendAsync(_usr, _id);
                if (item == null)
                {
                    return BadRequest($"Item with id {id} does not exist");
                }
        
                _logger.LogInformation($"item {_id} deleted");
                return Ok(item);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET: api/friends/readitemdto
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "usr, supusr")]
        [HttpGet()]
        [ActionName("ReadItemDto")]
        [ProducesResponseType(200, Type = typeof(csFriendCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
        {
            try
            {
                var _id = Guid.Parse(id);

                var item = await _service.ReadFriendAsync(_usr, _id, false);
                if (item == null)
                {
                    return BadRequest($"Item with id {id} does not exist");
                }

                var dto = new csFriendCUdto(item);
                return Ok(dto);         
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT: api/friends/updateitem/id
        //Body: csFriendCUdto in Json
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "usr, supusr")]
        [HttpPut("{id}")]
        [ActionName("UpdateItem")]
        [ProducesResponseType(200, Type = typeof(IFriend))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] csFriendCUdto item)
        {
            try
            {
                var _id = Guid.Parse(id);

                if (item.FriendId != _id)
                    throw new Exception("Id mismatch");

                var _item = await _service.UpdateFriendAsync(_usr, item);
                _logger.LogInformation($"item {_id} updated");
               
                return Ok(_item);             
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not update. Error {ex.Message}");
            }
        }

        //POST: api/friends/createitem
        //Body: csFriendCUdto in Json
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "usr, supusr")]
        [HttpPost()]
        [ActionName("CreateItem")]
        [ProducesResponseType(200, Type = typeof(IFriend))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] csFriendCUdto item)
        {
            try
            {
                var _item = await _service.CreateFriendAsync(_usr, item);
                _logger.LogInformation($"item {_item.FriendId} created");

                return Ok(_item);       
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
           //Remember async programming. .Result waits for the Task to complete
            var _token = HttpContext.GetTokenAsync("access_token").Result;
            _usr = csJWTService.DecodeToken(_token);
            base.OnActionExecuting(context);
        }

        #region constructors
        public FriendsController(IFriendsService service, ILogger<FriendsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        /*
        public FriendsController(IFriendsService service, ILogger<FriendsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        */
        #endregion
    }
}

