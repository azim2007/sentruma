using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// интерфейс, реализуемый всеми классами-обработчиками сервисных реплик (реплик для изменения фона, героев, музыки и т д)
/// </summary>
public interface ICommandHandler
{
    /// <summary>
    /// коллекция названия всех команд, которые обрабатывает класс, например define show hide destroy
    /// </summary>
    /// <returns>первые слова каждой команды</returns>
    IEnumerable<string> GetCommands();

    /// <summary>
    /// если эта функция вызвалась, значит надо обрабатывать команду
    /// </summary>
    /// <param name="command">пришедшая строка из сервисной реплики</param>
    void HandleCommand(string command);
}


/// <summary>
/// класс-оболочка над всеми классами-обработчиками сервисных реплик
/// </summary>
public class CommandHandler
{
    private Dictionary<string, ICommandHandler> CommandHandlerId { get; set; }

    /// <summary>
    /// передавать все имеющиеся обработчики
    /// </summary>
    /// <param name="handlers">лист классов-обработчиков сервисных реплик</param>
    public CommandHandler(List<ICommandHandler> handlers)
    {
        CommandHandlerId = handlers
            .SelectMany(handler => handler.GetCommands()
            .Select(command => Tuple.Create(command, handler)))
            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
    }

    /// <summary>
    /// пихаем строку из сервисной реплики прям суда и не паримся
    /// </summary>
    /// <param name="command">строка из сервисной реплики</param>
    public void HandleCommand(string command)
    {
        var name = command.Split(' ')[0];
        if(CommandHandlerId.ContainsKey(name))
            CommandHandlerId[name].HandleCommand(command);
        else
            Debugger.LogError("нет команды " + command);
    }
}