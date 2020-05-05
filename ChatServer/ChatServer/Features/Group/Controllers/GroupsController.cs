using AutoMapper;
using ChatServer.Controllers;
using ChatServer.Features.Group.Models;
using ChatServer.Features.Group.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Controllers
{
    using ChatServer.Data.Models.User;
    using ChatServer.Features.User.Models;
    using Data.Models.Group;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    [Authorize]
    public class GroupsController : ApiController
    {
        private readonly IGroupService groupService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public GroupsController(
            IGroupService groupService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.groupService = groupService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Add(GroupRequestModel model)
        {
            var group = this.mapper.Map<Group>(model);
            var user = await this.userManager.FindByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            group.User = user;

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await this.groupService.Add(group);

            if (result.Succeeded)
            {
                return Created(nameof(this.Add), group.Id);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("all/{userId}")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<GroupResponseModel>>> All()
        {
            var groups = await this.groupService.All();
            return groups.ToList();

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupResponseModel>> ById(int id)
        {
            var group = await this.groupService.ById(id);

            group.isJoined = group.Users
                .Any(x => x.UserId.Contains(this.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            group.IsMy = this.User.FindFirstValue(ClaimTypes.NameIdentifier) == group.OwnerId;

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Edit(int id, GroupRequestModel model)
        {
            var group = this.mapper.Map<Group>(model);
            var result = await this.groupService.EditAsync(id, group);

            if (result.Succeeded)
            {
                return Ok(group.Id);
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Dismiss(int id)
        {
            var result = await this.groupService.Dismiss(id);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("addToGroup/{groupId}")]
        public async Task<ActionResult> AddToGroup(int groupId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.groupService.AddToGroup(groupId, userId);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("removeFromGroup/{groupId}")]
        public async Task<ActionResult> RemoveFromGroup(int groupId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.groupService.RemoveFromGroup(groupId, userId);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("{groupId}/all")]
        public async Task<IEnumerable<AboutUserResponseModel>> UsersInGroup(int groupId)
        {
            var users = await this.groupService.AllInGroup(groupId);

            return users;
        }

        [HttpGet]
        public async Task<IEnumerable<GroupResponseModel>> ListAll()
        {
            var groups = await this.groupService.ListAll();
            return groups;
        }
    }
}
