using System.Net;

namespace MyApi.Exceptions.UserExceptions
{
    public class UserCannotAccessToUpdateException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

        public string ErrorMessage { get; }

        public UserCannotAccessToUpdateException()
        {
            ErrorMessage = "User cannot access to update another user info";
        }

        public UserCannotAccessToUpdateException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public UserCannotAccessToUpdateException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
