using System.Net;

namespace MyApi.Exceptions
{
    public interface IBaseException
    {
        HttpStatusCode StatusCode { get; }
        string ErrorMessage { get; }
        virtual string? ErrorDetail { get => "Not any error detail"; }
    }
}
