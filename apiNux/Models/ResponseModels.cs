namespace apiNux.Models
{
    public class ResponseModel
    {
        public ResponseModel(string message, object content)
        {
            Message = message;
            Content = content;
        }
        public ResponseModel(int statusCode, object content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public ResponseModel(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public ResponseModel(string message)
        {
            Message = message;
        }
        public ResponseModel(object content)
        {
            Content = content;
        }

        public ResponseModel() { }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
    }

    public class InternalServerErrorResponse : ResponseModel
    {
        public InternalServerErrorResponse(string message) : base(message)
        {
            Message = message;
            StatusCode = 500;
        }
    }

    public class UnauthorizedResponse : ResponseModel
    {
        public UnauthorizedResponse(string message) : base(message)
        {
            Message = message;
            StatusCode = 401;
        }
    }

    public class ForbiddenResponse : ResponseModel
    {
        public ForbiddenResponse(string message) : base(message)
        {
            Message = message;
            StatusCode = 403;
        }
    }

    public class OkResponse : ResponseModel
    {
        public OkResponse(object content) : base(content)
        {
            Content = content;
            StatusCode = 200;
        }

        public OkResponse() : base()
        {
            StatusCode = 200;
        }
    }

    public class ConflictResponse : ResponseModel
    {
        public ConflictResponse(string message) : base(message)
        {
            Message = message;
            StatusCode = 409;
        }
    }

    public class UnprocessableEntityResponse : ResponseModel
    {
        public UnprocessableEntityResponse(string message) : base(message)
        {
            Message = message;
            StatusCode = 422;
        }
    }
}
