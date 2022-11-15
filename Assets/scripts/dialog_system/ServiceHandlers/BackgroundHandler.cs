using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;

public class BackgroundHandler : ICommandHandler
{
    private BackgroundController background;
    private ForegroundController foreground;
    private Dictionary<string, Action<Queue<string>>> commandAction;
    public BackgroundHandler(BackgroundController background, ForegroundController foreground)
    {
        this.background = background;
        this.foreground = foreground;
        commandAction = new Dictionary<string, Action<Queue<string>>>
        {
            { "bg", (Queue<string> command) => ShowBackground(command) },
            { "hide-bg", (Queue<string> command) => HideBackground(command) },
        };
    }

    public IEnumerable<string> GetCommands()
    {
        yield return "bg";
        yield return "hide-bg";
    }

    public void HandleCommand(string command)
    {
        var listCommand = command.Split(' ');
        Queue<string> commandQueue = new Queue<string>(listCommand);
        commandAction[listCommand[0]].Invoke(commandQueue);
    }

    private void HideBackground(Queue<string> command)
    {
        float duration = 0f;
        command.Dequeue();
        try
        {
            duration = Parser.FloatParse(command.Dequeue());
        }
        catch
        {
            Debugger.LogError("hide-bg: в качестве аргумента было передано не число или аргумента не было передано");
            return;
        }

        background.HideBackground(duration);
    }

    private void ShowBackground(Queue<string> command)
    {
        #region parameters setting
        Sprite sprite = null;
        Color bgColor = Color.white, fgColor = new Color(1, 1, 1, 0);
        float bgDuration = 0f, bgColorDuration = 0f, fgColorDuration = 0f;
        var paramsActions = new Dictionary<string, Action<Queue<string>>>()
        {
            { "bg-color", (queue) =>
                {
                    try
                    {
                        bgColor = GetBGColor(command.Dequeue(), command.Dequeue(), command.Dequeue());
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
                        fgColor = GetFGColor(command.Dequeue(), command.Dequeue(),
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
                    bgDuration = Parser.FloatParse(queue.Dequeue());
                    bgColorDuration = Parser.FloatParse(queue.Dequeue());
                    fgColorDuration = Parser.FloatParse(queue.Dequeue());
                }
                catch
                {
                    Debugger.LogError("bg: в параметр duration было передано неправильное кол-во аргументов");
                }
            } },
        };
        #endregion
        command.Dequeue();
        string bgName = "";
        try
        {
            bgName = command.Dequeue();
            sprite = Resources.Load<Sprite>("backgrounds/" + bgName);
        }
        catch
        {
            Debugger.LogError("bg: нет такого фона " + bgName);
            return;
        }

        string action;
        while (command.TryDequeue(out action))
        {
            if (!paramsActions.ContainsKey(action))
            {
                Debugger.LogError("bg: нет параметра " + action);
            }

            paramsActions[action](command);
        }

        background.SetBackground(sprite, bgColor, bgDuration, bgColorDuration);
        foreground.SetForegroundColor(fgColor, fgColorDuration);
    }

    private Color GetFGColor(string r, string g, string b, string a)
    {
        try
        {
            return new Color(
                ConvertToFloat(r),
                ConvertToFloat(g),
                ConvertToFloat(b),
                ConvertToFloat(a)
            );
        }
        catch
        {
            Debugger.LogError("bg: некорректный формат цвета");
            return new Color(1, 1, 1, 0);
        }
    }

    private Color GetBGColor(string r, string g, string b)
    {
        try
        {
            return new Color(
                ConvertToFloat(r),
                ConvertToFloat(g),
                ConvertToFloat(b)
            );
        }
        catch
        {
            Debugger.LogError("bg: некорректный формат цвета");
            return Color.white;
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
