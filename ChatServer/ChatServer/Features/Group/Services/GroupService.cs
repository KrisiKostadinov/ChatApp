using ChatServer.Common.Extentions;
using ChatServer.Data;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Services
{
    using ChatServer.Common;
    using ChatServer.Common.Mapping;
    using ChatServer.Data.Models.Group;
    using ChatServer.Features.Group.Models;
    using ChatServer.Features.User.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class GroupService : IGroupService
    {
        private readonly ChatContext context;

        public GroupService(ChatContext context)
        {
            this.context = context;
        }

        public async Task<Result> Add(Group group)
        {
            if (group.Subject == null || group.Subject == "")
            {
                return Result.Failed(new Error("Invalid Operation", "Please enter valid title group."));
            }

            await this.context.Groups.AddAsync(group);
            await context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> AddToGroup(int groupId, string userId)
        {
            var participant = new UserGroup
            {
                GroupId = groupId,
                UserId = userId,
            };

            var isValid = await this.ById(groupId);

            if (isValid == null)
            {
                return Result.Failed(new Error("Invalid Operation", "Please enter valid id group."));
            }

            var isInGroup = await this.context
                .UsersGroups
                .Where(g => g.GroupId == groupId && g.UserId == userId)
                .FirstOrDefaultAsync();

            if (isInGroup != null)
            {
                return Result.Failed(new Error("Invalid Operation", "This user is exist in group."));
            }

            this.context.UsersGroups.Add(participant);
            await this.context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<GroupResponseModel>> AllByUserId(string userId)
        {
            var groups = await this.context
                .Groups.Where(g => g.OwnerId == userId)
                .To<GroupResponseModel>()
                .ToListAsync();

            return groups;
        }

        public async Task<IEnumerable<AboutUserResponseModel>> AllInGroup(int groupId)
        {
            var usersInGroup = await this.context
                .UsersGroups
                .Where(x => x.GroupId == groupId)
                .Select(x => x.User)
                .To<AboutUserResponseModel>()
                .ToListAsync();
            return usersInGroup;
        }

        public async Task<GroupResponseModel> ById(int id)
        {
            var group = await this.context
                .Groups.Where(g => g.Id == id)
                .To<GroupResponseModel>()
                .FirstOrDefaultAsync();

            return group;
        }

        public async Task<Result> Dismiss(int id)
        {
            var group = await this.context
                .Groups
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                return Result.Failed(new Error("Invalid Operation", "Please enter valid id group."));
            }

            this.context.Groups.Remove(group);
            await this.context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> EditAsync(int id, Group model)
        {
            var group = await this.context
                .Groups
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                return Result.Failed(new Error("Invalid Operation", "Please enter valid id group."));
            }

            group.Description = model.Description;
            group.Subject = model.Subject;

            this.context.Groups.Update(group);

            await this.context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<bool> IsInGroup(string userId, string group)
        {
            var groupDb = await this.context
                .Groups
                .Where(g => g.Subject == group && g.User.Id == userId)
                .FirstOrDefaultAsync();

            if (groupDb == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<GroupResponseModel>> ListAll()
        {
            var groups = await this.context
                .Groups
                .To<GroupResponseModel>()
                .ToListAsync();

            return groups;
        }

        public async Task<Result> RemoveFromGroup(int groupId, string userId)
        {
            var participant = await this.context
                .UsersGroups
                .Where(g => g.GroupId == groupId && g.UserId == userId)
                .FirstOrDefaultAsync();
            if (participant == null)
            {
                return Result.Failed(new Error("Invalid Operation", "This user is not exist in group."));
            }

            this.context.UsersGroups.Remove(participant);
            await this.context.SaveChangesAsync();
            return Result.Success;
        }
    }
}
