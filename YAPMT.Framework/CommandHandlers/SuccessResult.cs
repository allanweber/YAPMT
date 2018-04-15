namespace YAPMT.Framework.CommandHandlers
{
    public class SuccessResult : ICommandResult
    {
        public SuccessResult()
        {

        }

        public SuccessResult(object result)
        {
            this.Result = result;
        }

        public bool IsSuccess => true;

        public bool IsFailure => false;

        public object Result { get; set; }
    }
}
