using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackgroundHandler : ICommandHandler
{
    private Image Background { get; set; }
    private Image Foreground { get; set; }
    private Dictionary<string, Action<Queue<string>>> CommandAction { get; set; }
    public BackgroundHandler(Image background, Image foreground)
    {
        Background = background;
        Foreground = foreground;
        CommandAction = new Dictionary<string, Action<Queue<string>>>
        {
            { "bg", (Queue<string> command) => ChangeBackground(command) }
        };
    }

    public IEnumerable<string> GetCommands()
    {
        yield return "bg";
    }

    public void HandleCommand(string command)
    {
        var listCommand = command.Split(' ');
        Queue<string> commandQueue = new Queue<string>(listCommand);
        CommandAction[listCommand[0]].Invoke(commandQueue);
    }

    private void ChangeBackground(Queue<string> command)
    {
        #region parameters setting
        var paramsActions = new Dictionary<string, Action<Queue<string>>>()
        {
            { "bg-color", (queue) => 
                {
                    try
                    {
                        ChangeBGColor(command.Dequeue(), command.Dequeue(), command.Dequeue());
                    }
                    catch
                    {
                        Debugger.LogError("bg: в параметр bg-color было передано неправильное кол-во аргументов");
                    }
                } },
            { "fg-color", (queue) =>
                {
                    try
                    {
                        ChangeFGColor(command.Dequeue(), command.Dequeue(),
                            command.Dequeue(), command.Dequeue());
                    }
                    catch
                    {
                        Debugger.LogError("bg: в параметр fg-color было передано неправильное кол-во аргументов");
                    }
                } },
            { "duration", (queue) => {
                try
                {
                    SetDuration(command.Dequeue(), command.Dequeue(), command.Dequeue());
                }
                catch
                {
                    Debugger.LogError("bg: в параметр duration было передано неправильное кол-во аргументов");
                } 
            } },
        };
        #endregion
        command.Dequeue();
        try
        {
            Background.sprite = Resources.Load<Sprite>("backgrounds/" + command.Dequeue());
            Background.color = Color.white;
            Foreground.color = new Color(0, 0, 0, 0);
        }
        catch
        {
            Debugger.LogError("нет такого фона");
            return;
        }

        string action;
        while (command.TryDequeue(out action))
        {
            if (action == "bg-color")
            {
                ChangeBGColor(command.Dequeue(), command.Dequeue(), command.Dequeue());
            }
            else if (action == "fg-color")
            {
                ChangeFGColor(command.Dequeue(), command.Dequeue(), command.Dequeue(), command.Dequeue());
            }
            else
            {
                Debugger.LogError("у bg нет параметра " + action);
                return;
            }
        }
    }

    private void SetDuration(string bgDuration, string bgcolDuration, string fgcolDuration)
    {

    }
    private void ChangeFGColor(string r, string g, string b, string a)
    {
        try
        {
            Foreground.color = new Color(
                ConvertToFloat(r),
                ConvertToFloat(g),
                ConvertToFloat(b),
                ConvertToFloat(a)
            );
        }
        catch
        {
            Debugger.LogError("некорректный формат цвета");
        }
    }

    private void ChangeBGColor(string r, string g, string b)
    {
        try
        {
            Background.color = new Color(
                ConvertToFloat(r),
                ConvertToFloat(g),
                ConvertToFloat(b)
            );
        }
        catch
        {
            Debugger.LogError("некорректный формат цвета");
        }
    }

    /// <summary>
    /// превращает строку от 0 до 255 в число от 0 до 1
    /// </summary>
    /// <param name="a">строка от "0" до "255"</param>
    /// <returns>double от 0 до 1, который используется в конструкторе Color()</returns>
    private float ConvertToFloat(string a)
    {
        float _byte = int.Parse(a);
        return _byte / 255.0f;
    }
}
