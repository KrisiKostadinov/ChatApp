using AutoMapper;
using ChatServer.Controllers;
using ChatServer.Features.Group.Models;
using ChatServer.Features.Group.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Controllers
{
    using Data.Models.Group;
    using Microsoft.AspNetCore.Authorization;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    [Authorize]
    public class GroupsController : ApiController
    {
        private readonly IGroupService groupService;
        private readonly IMapper mapper;

        public GroupsController(
            IGroupService groupService,
            IMapper mapper)
        {
            this.groupService = groupService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Add(GroupRequestModel model)
        {
            var group = this.mapper.Map<Group>(model);
            group.OwnerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (group.OwnerId == null)
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
        public async Task<ActionResult<IEnumerable<GroupResponseModel>>> AllByUserId(string userId)
        {
            if (userId == null)
            {
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var groups = await this.groupService.AllByUserId(userId);

            return groups.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupResponseModel>> ById(int id)
        {
            var group = await this.groupService.ById(id);
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
                var newGroup = await this.groupService.ById(id);
                return Ok(newGroup);
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
    }
}
