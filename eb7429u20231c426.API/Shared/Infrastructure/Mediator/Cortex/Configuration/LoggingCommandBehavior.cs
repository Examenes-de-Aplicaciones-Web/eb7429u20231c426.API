using Cortex.Mediator.Commands;

namespace eb7429u20231c426.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

/// <summary>
///     Logs the execution of commands.
/// </summary>
/// <typeparam name="TCommand">The type of command.</typeparam>
public class LoggingCommandBehavior<TCommand> : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken)
    {
        // Log command start
        Console.WriteLine($"Starting command: {typeof(TCommand).Name}");
        await next();
    }
}