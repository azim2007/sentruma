using System.Collections.Generic;

public interface ICommandHandler
{
    IEnumerable<string> GetCommands();

    void HandleCommand(string command);
}

public class CommandHandler
{
    private List<ICommandHandler> Handlers { get; set; }
    private Dictionary<string, int> CommandHandlerId { get; set; }

    public CommandHandler(List<ICommandHandler> handlers)
    {
        Handlers = handlers;
        CommandHandlerId = new Dictionary<string, int>();
        for(int i = 0; i < Handlers.Count; i++)
        {
            foreach(var command in Handlers[i].GetCommands())
            {
                CommandHandlerId.Add(command, i);
            }
        }
    }

    public void HandleCommand(string command)
    {
        var name = command.Split(' ')[0];
        try
        {
            Handlers[CommandHandlerId[name]].HandleCommand(command);
        }
        catch
        {
            Debugger.Log("there isnt command " + command);
        }
    }
}