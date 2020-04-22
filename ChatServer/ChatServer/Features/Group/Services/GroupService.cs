using ChatServer.Common.Extentions;
using ChatServer.Data;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Services
{
    using ChatServer.Common;
    using ChatServer.Data.Models.Group;

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
    }
}
