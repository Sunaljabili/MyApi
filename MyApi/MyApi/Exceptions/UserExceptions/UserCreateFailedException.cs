using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MyApi.Exceptions.UserExceptions
{
    public class UserCreateFailedException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }
        public string? ErrorDetail { get; }

        public UserCreateFailedException()
        {
            ErrorMessage = "Unexpected error occurred while creating user";
        }

        public UserCreateFailedException(IEnumerable<IdentityError> errors)
        {
            ErrorMessage = "Unexpected errors occurred while creating user";
            ErrorDetail = String.Join(' ', errors.Select(e => e.Description));
        }

        public UserCreateFailedException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public UserCreateFailedException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
