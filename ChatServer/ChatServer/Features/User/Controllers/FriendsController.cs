﻿using ChatServer.Controllers;
using ChatServer.Data.Models.User;
using ChatServer.Data.Models.User.Request;
using ChatServer.Features.User.Models.Friend;
using ChatServer.Features.User.Models.Request;
using ChatServer.Features.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;

        public FriendsController(
            IFriendsService friendsService,
            UserManager<ApplicationUser> userManager)
        {
            this.friendsService = friendsService;
            this.userManager = userManager;
        }

        //[HttpGet]
        //[Route("{userId}")]
        //public async Task<ActionResult<FriendResponseModel>> ById(string userId)
        //{
        //    var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var friend = await this.friendsService.ById(currentUserId, userId);

        //    return friend;
        //}

        [HttpGet]
        [Route("request/all")]
        public async Task<ActionResult<IEnumerable<RequestResponseModel>>> ListAllRequestsByUserId()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await this.friendsService.ListAllRequestsByUserId(currentUserId);

            return requests.ToList();
        }

        [HttpPost]
        [Route("request/{userId}")]
        public async Task<ActionResult<bool>> AddRequest(string userId)
        {
            if (userId == null)
            {
                return BadRequest($"The {nameof(userId)} not be null.");
            }

            var userIdFrom = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var request = new Request()
            {
                UserIdFrom = userIdFrom,
                UserIdTo = userId,
            };

            if (request.UserIdTo == request.UserIdFrom)
            {
                return BadRequest($"The ids not be duplicated.");
            }

            var result = await this.friendsService.AddRequest(request);

            if (result.Succeeded)
            {
                return true;
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("requests/my")]
        public async Task<IEnumerable<RequestResponseModel>> ListAllMyRequests()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var requests = await this.friendsService.ListAllMyRequests(userId);
            return requests;
        }

        [HttpGet]
        [Route("requests")]
        public async Task<IEnumerable<RequestResponseModel>> ListAllRequests()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var requests = await this.friendsService.ListAllRequestsByUserId(userId);
            return requests;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<ActionResult<bool>> AddFriendAcync(string userId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest();
            }

            var friend = new Friend
            {
                CurrentUserId = currentUserId,
                OtherUserId = userId,
            };

            var result = await this.friendsService.AddAsync(friend);

            if (result.Succeeded)
            {
                return true;
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<FriendResponseModel>>> GetAllById()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

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

        [HttpGet]
        [Route("messages/{firstUserId}/{secondUserId}")]
        public async Task<IEnumerable<MessageResponseModel>> GetAllMyMessages(string firstUserId, string secondUserId)
        {
            var messages = await this.friendsService.GetAllMyMessages(firstUserId, secondUserId);
            return messages;
        }

        [HttpDelete]
        [Route("request/{userId}")]
        public async Task<ActionResult<bool>> DismissRequest(string userId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.friendsService.DismissRequest(currentUserId, userId);

            if (result.Succeeded)
            {
                return true;
            }

            return BadRequest(result.Errors);
        }
    }
}
