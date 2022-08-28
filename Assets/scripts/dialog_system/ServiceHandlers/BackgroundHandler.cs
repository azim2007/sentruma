using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackgroundHandler : ICommandHandler
{
    private Image Background { get; set; }
    private Dictionary<string, Action<List<string>>> CommandAction { get; set; }
    public BackgroundHandler(Image background)
    {
        Background = background;
        CommandAction = new Dictionary<string, Action<List<string>>>
        {
            { "bg", (List<string> command) => ChangeBackground(command) }
        };
    }

    public IEnumerable<string> GetCommands()
    {
        yield return "bg";
    }

    public void HandleCommand(string command)
    {
        var listCommand = command.Split(' ');
        CommandAction[listCommand[0]].Invoke(listCommand.ToList());
    }

    void ChangeBackground(List<string> command)
    {
        try
        {
            Background.sprite = Resources.Load<Sprite>("backgrounds/" + command[1]);
        }
        catch
        {
            Debugger.Log("нет фона с именем " + command[1]);
        }

        if(command.Count > 2)
        {
            if (command[2] == "color")
            {
                try
                {
                    Background.color = new Color(
                        ConvertToFloat(command[3]),
                        ConvertToFloat(command[4]),
                        ConvertToFloat(command[5])
                    );
                }
                catch
                {
                    Debugger.Log("некорректный формат цвета");
                }
            }
        }

        float ConvertToFloat(string a)
        {
            float _byte = int.Parse(a);
            return _byte / 255.0f;
        }
    }   
}
