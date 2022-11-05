using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// класс, обработчик реплик, связанных с ожиданием в диалоге
/// </summary>
public class WaitHandler : ICommandHandler
{
    private Waiter waiter;
    private Dictionary<string, Action<Queue<string>>> CommandAction { get; set; }

    public WaitHandler(Waiter waiter)
    {
        this.waiter = waiter;
        CommandAction = new Dictionary<string, Action<Queue<string>>>
        {
            { "wait", (Queue<string> command) => Wait(command) }
        };
    }

    public IEnumerable<string> GetCommands()
    {
        yield return "wait";
    }

    public void HandleCommand(string command)
    {
        var listCommand = command.Split(' ');
        Queue<string> commandQueue = new Queue<string>(listCommand);
        CommandAction[listCommand[0]].Invoke(commandQueue);
    }

    /// <summary>
    /// присваивает в waiter необходимое кол-во секунд ожидания
    /// </summary>
    /// <param name="command"></param>
    public void Wait(Queue<string> command)
    {
        command.Dequeue();
        string arg;
        if(command.TryDequeue(out arg))
        {
            float time;
            try
            {
                time = float.Parse(arg, new NumberFormatInfo() { NumberDecimalSeparator = "," });
            }
            catch
            {
                Debugger.LogError("в качестве аргумента wait было передано " + arg);
                return;
            }

            waiter.SetTimeMeasure(new WaitForSeconds(time));
            return;
        }

        Debugger.LogError("команде wait не было передано аргументов");
    }
}
