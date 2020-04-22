﻿using ChatServer.Common.Extentions;
using ChatServer.Data;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Services
{
    using ChatServer.Common;
    using ChatServer.Common.Mapping;
    using ChatServer.Data.Models.Group;
    using ChatServer.Features.Group.Models;
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

            this.context.Groups.Add(group);
            await context.SaveChangesAsync();

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
    }
}
