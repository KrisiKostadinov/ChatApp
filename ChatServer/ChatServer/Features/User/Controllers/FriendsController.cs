using ChatServer.Controllers;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;
using ChatServer.Features.User.Models.Friend;
using ChatServer.Features.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Controllers
{
    [Authorize]
    public class FriendsController : ApiController
    {
        private readonly IFriendsService friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            this.friendsService = friendsService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddFriendAcync(FriendRequestModel model)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (model.UserId == null)
            {
                return BadRequest();
            }

            var friend = new Friend
            {
                CurrentUserId = currentUserId,
                OtherUserId = model.UserId,
            };

            var result = await this.friendsService.AddAsync(friend);

            if (result.Succeeded)
            {
                return Ok(model.UserId);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("all/{userId}")]
        public async Task<ActionResult<IEnumerable<FriendResponseModel>>> GetAllById(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var friends = await this.friendsService.GetAllById(userId);
            return friends.ToList();
        }

        [HttpDelete]
        [Route("dismiss/{userId}")]
        public async Task<ActionResult> RemoveAsync(string userId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null || userId == null || currentUserId == userId)
            {
                return BadRequest();
            }

            var result = await this.friendsService.RemoveAsync(userId, currentUserId);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }
    }
}
