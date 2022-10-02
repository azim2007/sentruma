using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : ICommandHandler
{
    public static string GetFolderName(string name) => "charactersImages/" + name;
    private Dictionary<string, Action<Queue<string>>> CommandAction { get; set; }
    private static Dictionary<string, CharacterImageController> DefinedCharactersNameDirPath { get; set; }
    public CharacterHandler()
    {
        CommandAction = new Dictionary<string, Action<Queue<string>>>()
        {
            {"define", (Queue<string> command) => DefineCharacter(command) },
            {"destroy", (Queue<string> command) => DestroyCharacter(command) },
            {"show", (Queue<string> command) => ShowCharacter(command) },
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

    private void ShowCharacter(Queue<string> command)
    {
        command.Dequeue();
    }

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
            Debugger.Log("нет такой папки персонажа");
            return;
        }

        if (DefinedCharactersNameDirPath.ContainsKey(characterName))
        {
            Debugger.Log("нельзя создать несколько персонажей с одним именем");
            return;
        }

        DialogFactory dialogFactory = new DialogFactory();
        CharacterImageController character = dialogFactory.Instantiate(id: "chrctrImg").GetComponent<CharacterImageController>();
        character.DirPath = command.Dequeue();
        character.gameObject.SetActive(false);
        DefinedCharactersNameDirPath.Add(characterName, character);
    }

    private void DestroyCharacter(Queue<string> command)
    {
        command.Dequeue();
        var characterName = command.Dequeue();
        if (!DefinedCharactersNameDirPath.ContainsKey(characterName))
        {
            Debugger.Log("нет переменной с именем " + characterName);
            return;
        }

        DefinedCharactersNameDirPath.Remove(characterName);
    }
}
