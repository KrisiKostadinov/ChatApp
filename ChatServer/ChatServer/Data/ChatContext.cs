using ChatServer.Data.Models.Group;
using ChatServer.Data.Models.User;
using ChatServer.Data.Models.User.Request;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Data
{
    public class ChatContext : IdentityDbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<AboutUser> AboutUsers { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<UserGroup> UsersGroups { get; set; }

        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserGroup>()
                .HasKey(x => new { x.UserId, x.GroupId });

            builder.Entity<Request>()
                .HasKey(x => new { x.UserId, x.UserIdFrom });

            builder.Entity<Request>()
                .HasOne(x => x.UserFrom)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Group>()
                .HasOne(x => x.User)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.OwnerId);

            builder.Entity<UserGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.UsersGroups)
                .HasForeignKey(x => x.GroupId);

            builder.Entity<UserGroup>()
                .HasOne(x => x.User)
                .WithMany(x => x.UsersGroups)
                .HasForeignKey(x => x.UserId);

            base.OnModelCreating(builder);
        }
    }
}
