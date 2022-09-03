using System;
using System.Collections.Generic;


public class CharacterHandler : ICommandHandler
{
    private Dictionary<string, Action<Queue<string>>> CommandAction { get; set; }
    private Dictionary<string, string> Characters { get; set; }
    public IEnumerable<string> GetCommands()
    {
        yield return "define";
        yield return "show";
        yield return "hide";
        yield return "destroy";
    }

    public void HandleCommand(string command)
    {
        var listCommand = command.Split(' ');
        Queue<string> commandQueue = new Queue<string>(listCommand);
        CommandAction[listCommand[0]].Invoke(commandQueue);
    }
}
