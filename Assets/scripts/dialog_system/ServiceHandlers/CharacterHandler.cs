﻿using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// класс для обработки реплик, связанных с собеседниками в диалоге
/// </summary>
public class CharacterHandler : ICommandHandler
{
    // в этой папке хранятся все спрайты, используемые в диалогах
    public static string GetFolderName(string name) => "charactersImages/" + name; 

    private Dictionary<string, Action<Queue<string>>> CommandAction { get; set; }

    private Image Background { get; set; }

    // словарь для дефайна персонажа на какую-то переменную
    private static Dictionary<string, CharacterImageController> DefinedCharactersNameCharControllers 
    { get; set; } 

    public CharacterHandler(Image background)
    {
        Background = background;
        DefinedCharactersNameCharControllers = new Dictionary<string, CharacterImageController>();
        CommandAction = new Dictionary<string, Action<Queue<string>>>()
        {
            {"define", (Queue<string> command) => DefineCharacter(command) },
            {"destroy", (Queue<string> command) => DestroyCharacter(command) },
            {"show", (Queue<string> command) => ShowCharacter(command) },
            {"hide", (Queue<string> command) => HideCharacter(command) },
        };
    }

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

    /// <summary>
    /// отображает персонажа на экране в определенном месте с определенной эмоцией
    /// </summary>
    /// <param name="command"></param>
    private void ShowCharacter(Queue<string> command)
    {
        #region parametrs settings
        string atDistance = "middle", atVertical = "middle", atHorizontal = "middle", animationType = "", charName, emotionName;
        int layer = 0;
        float animationDuration = 0;
        var paramsActions = new Dictionary<string, Action<string>>()
        {
            {"atHorizontal", (param) => atHorizontal = param },
            {"atVertical", (param) => atVertical = param },
            {"atDistance", (param) => atDistance = param },
            {"layer", (param) =>
                {
                    try
                    {
                        layer = int.Parse(param);
                    }
                    catch
                    {
                        Debugger.LogError("в параметр layer было передано " + param);
                    }
                }
            },

            {"with", (param) =>
                {
                    var args = param.Split(' ');
                    animationType = args[0];
                    try
                    {
                        animationDuration = float.Parse(args[1], new NumberFormatInfo(){NumberDecimalSeparator = "," });
                    }
                    catch
                    {
                        Debugger.LogError("в параметр with в качестве длительности анимации было передано " + args[1]);
                    }
                } 
            }
        };
#endregion
        command.Dequeue();
        try
        {
            charName = command.Dequeue();
            if (!DefinedCharactersNameCharControllers.ContainsKey(charName))
            {
                Debugger.LogError("нет переменной с именем " + charName);
                return;
            }

            DefinedCharactersNameCharControllers[charName].gameObject.SetActive(true);
            emotionName = command.Dequeue();
        }
        catch
        {
            Debugger.LogError("некорректный формат команды");
            return;
        }

        string action;
        while (command.TryDequeue(out action))
        {
            if (!paramsActions.ContainsKey(action))
            {
                Debugger.LogError("некорректный параметр " + action);
                return;
            }

            try
            {
                if(action == "with")
                    paramsActions[action](command.Dequeue() + " " + command.Dequeue());
                else
                    paramsActions[action](command.Dequeue());
            }
            catch
            {
                Debugger.LogError("недостаточное кол во аргументов у параметра " + action);
                return;
            }
        }

        DefinedCharactersNameCharControllers[charName]
            .Show(emotion: emotionName,
            atHorizontal: atHorizontal,
            atVertical: atVertical,
            atDistance: atDistance,
            layer: layer,
            animationDurationSeconds: animationDuration,
            animationType: animationType);
    }

    /// <summary>
    /// скрывает персонажа с экрана
    /// </summary>
    /// <param name="command"></param>
    private void HideCharacter(Queue<string> command)
    {
        #region ParametersSetting
        string animationType = "";
        float animationDuration = 0;
        var paramsActions = new Dictionary<string, Action<string>>()
        {
            {"with", (param) =>
                {
                    var args = param.Split(' ');
                    animationType = args[0];
                    try
                    {
                        animationDuration = float.Parse(args[1], new NumberFormatInfo(){NumberDecimalSeparator = "," });
                    }
                    catch
                    {
                        Debugger.LogError("в параметр with в качестве длительности анимации было передано " + args[1]);
                    }
                } 
            }
        };
        #endregion

        command.Dequeue();
        var characterName = command.Dequeue();
        if (!DefinedCharactersNameCharControllers.ContainsKey(characterName))
        {
            Debugger.LogError("нет переменной с именем " + characterName);
            return;
        }

        string action;
        while(command.TryDequeue(out action))
        {
            if (!paramsActions.ContainsKey(action))
            {
                Debugger.LogError("некорректный параметр " + action);
                return;
            }

            try
            {
                if (action == "with")
                    paramsActions[action](command.Dequeue() + " " + command.Dequeue());
                else
                    paramsActions[action](command.Dequeue());
            }
            catch
            {
                Debugger.LogError("недостаточное кол во аргументов у параметра " + action);
                return;
            }
        }

        DefinedCharactersNameCharControllers[characterName]
            .Hide(animationDurationSeconds: animationDuration,
            animationType: animationType);
    }

    /// <summary>
    /// добавляет персонажа в словарь DefinedCharacters и создает для него объект на сцене
    /// </summary>
    /// <param name="command"></param>
    private void DefineCharacter(Queue<string> command)
    {
        command.Dequeue();
        string characterName = command.Dequeue();
        try
        {
            Resources.LoadAll<Sprite>(GetFolderName(command.Peek()));
        }
        catch
        {
            Debugger.LogError("нет такой папки персонажа");
            return;
        }

        if (DefinedCharactersNameCharControllers.ContainsKey(characterName))
        {
            Debugger.LogError("нельзя создать несколько персонажей с одним именем");
            return;
        }

        DialogFactory dialogFactory = new DialogFactory();
        CharacterImageController character = dialogFactory.Instantiate(id: "chrctrImg").GetComponent<CharacterImageController>();
        character.transform.SetParent(Background.transform);
        character.transform.localScale = new Vector3(1f, 1f, 1f);
        character.DirPath = command.Dequeue();
        character.gameObject.SetActive(false);
        DefinedCharactersNameCharControllers.Add(characterName, character);
    }

    /// <summary>
    /// убирает персонажа из словаря DefinedCharacters и удаляет его объект на сцене
    /// </summary>
    /// <param name="command"></param>
    private void DestroyCharacter(Queue<string> command)
    {
        command.Dequeue();
        try
        {
            string characterName = command.Dequeue();
            if (!DefinedCharactersNameCharControllers.ContainsKey(characterName))
            {
                Debugger.LogError("нет переменной с именем " + characterName);
                return;
            }

            GameObject.Destroy(DefinedCharactersNameCharControllers[characterName].gameObject);
            DefinedCharactersNameCharControllers.Remove(characterName);
        }
        catch
        {
            Debugger.Log("некорректный формат команды");
        }
    }
}
