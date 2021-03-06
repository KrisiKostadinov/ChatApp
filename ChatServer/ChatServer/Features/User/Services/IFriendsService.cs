﻿using ChatServer.Common.Extentions;
using ChatServer.Data.Models.Group;
using ChatServer.Data.Models.User;
using ChatServer.Data.Models.User.Request;
using ChatServer.Features.User.Models.Friend;
using ChatServer.Features.User.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Services
{
    public interface IFriendsService
    {
        Task<Result> AddAsync(Friend friend);

        Task<IEnumerable<FriendResponseModel>> GetAllById(string userId);

        Task<Result> RemoveAsync(string userId, string currentUserId);

        Task<Result> AddRequest(Request request);

        Task<IEnumerable<RequestResponseModel>> ListAllRequestsByUserId(string currentUserId);

        Task<Result> DismissRequest(string currentUserId, string userId);

        Task<Result> AddMessageOfUser(Message message);

        Task<IEnumerable<MessageResponseModel>> GetAllMyMessages(string firstUserId, string secondUserId);

        Task<IEnumerable<RequestResponseModel>> ListAllMyRequests(string userId);
    }
}
