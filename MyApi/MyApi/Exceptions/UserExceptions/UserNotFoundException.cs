using System.Net;

namespace MyApi.Exceptions.UserExceptions
{
    public class UserNotFoundException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public UserNotFoundException()
        {
            ErrorMessage = "User not found";
        }

        public UserNotFoundException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public UserNotFoundException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
