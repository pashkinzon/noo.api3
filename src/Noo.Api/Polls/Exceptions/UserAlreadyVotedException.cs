using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.Polls.Exceptions;

public class UserAlreadyVotedException : NooException
{
    public UserAlreadyVotedException(string message = "User has already voted in this poll.") : base(message)
    {
        Id = "USER_ALREADY_VOTED";
        StatusCode = HttpStatusCode.BadRequest;
    }
}
