namespace YAPMT.Framework.CommandHandlers
{
    public class SuccessResult : ICommandResult
    {
        public bool IsSuccess => true;

        public bool IsFailure => false;

        public object Result { get; set; }
    }
}
