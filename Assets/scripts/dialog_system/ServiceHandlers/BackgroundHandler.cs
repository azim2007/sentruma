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
        command.Dequeue();
        try
        {
            Background.sprite = Resources.Load<Sprite>("backgrounds/" + command.Dequeue());
            Background.color = Color.white;
            Foreground.color = new Color(0, 0, 0, 0);
        }
        catch
        {
            Debugger.Log("нет такого фона");
            return;
        }

        string action;
        while(command.TryDequeue(out action))
        {
            if (action == "bg-color")
            {
                if (!TryChangeBGColor(command.Dequeue(), command.Dequeue(), command.Dequeue()))
                    return;
            }
            else if (action == "fg-color")
            {
                if (!TryChangeFGColor(command.Dequeue(), command.Dequeue(), command.Dequeue(), command.Dequeue()))
                    return;
            }
        }
    }

    private bool TryChangeFGColor(string r, string g, string b, string a)
    {
        try
        {
            Foreground.color = new Color(
                ConvertToFloat(r),
                ConvertToFloat(g),
                ConvertToFloat(b),
                ConvertToFloat(a)
            );
            return true;
        }
        catch
        {
            Debugger.Log("некорректный формат цвета");
            return false;
        }
    }

    private bool TryChangeBGColor(string r, string g, string b)
    {
        try
        {
            Background.color = new Color(
                ConvertToFloat(r),
                ConvertToFloat(g),
                ConvertToFloat(b)
            );
            return true;
        }
        catch
        {
            Debugger.Log("некорректный формат цвета");
            return false;
        }
    }

    private float ConvertToFloat(string a)
    {
        float _byte = int.Parse(a);
        return _byte / 255.0f;
    }
}
