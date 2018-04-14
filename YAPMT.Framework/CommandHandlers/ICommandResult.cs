namespace YAPMT.Framework.CommandHandlers
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        object Result { get; set; }
    }
}
