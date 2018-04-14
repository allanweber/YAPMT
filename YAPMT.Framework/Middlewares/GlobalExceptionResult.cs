namespace YAPMT.Framework.Middlewares
{
    public class GlobalExceptionResult
    {
        public GlobalExceptionResult(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }

        public string ExceptionMessage { get; private set; }

        public string ExceptionStackTrace { get; private set; }

        public string ExceptionDetail { get; private set; }

        public void SetDevelopmentDetails(string exceptionMessage, string exceptionStackTrace, string exceptionDetail)
        {
            this.ExceptionMessage = exceptionMessage;
            this.ExceptionStackTrace = exceptionStackTrace;
            this.ExceptionDetail = exceptionDetail;
        }
    }
}
