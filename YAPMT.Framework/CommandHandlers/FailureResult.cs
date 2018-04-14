namespace YAPMT.Framework.CommandHandlers
{
    public class FailureResult : ICommandResult
    {
        public bool IsSuccess => Result == null;

        public bool IsFailure => Result != null;

        public object Result { get; set; }
    }
}
