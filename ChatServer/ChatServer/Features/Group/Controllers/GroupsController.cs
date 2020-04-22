using AutoMapper;
using ChatServer.Controllers;
using ChatServer.Features.Group.Models;
using ChatServer.Features.Group.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Controllers
{
    using Data.Models.Group;
    using System.Security.Claims;

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
    }
}
