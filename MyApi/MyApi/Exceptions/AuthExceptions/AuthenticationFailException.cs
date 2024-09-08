using System.Net;

namespace MyApi.Exceptions.AuthExceptions
{
    public class AuthenticationFailException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage { get; }

        public AuthenticationFailException()
        {
            ErrorMessage = "Username/Email or password incorrect";
        }

        public AuthenticationFailException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public AuthenticationFailException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
