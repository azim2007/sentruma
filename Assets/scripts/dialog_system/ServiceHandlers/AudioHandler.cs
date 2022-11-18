using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioHandler : ICommandHandler
{
    private Dictionary<string, Action<Queue<string>>> commandAction;
    private Dictionary<string, AudioSourceController> definedAudioSources;
    private string GetFolder(string path) => "sounds/" + path;

    public IEnumerable<string> GetCommands()
    {
        yield return "play";
        yield return "change-volume";
        yield return "stop";
    }

    public void HandleCommand(string command)
    {
        var listCommand = command.Split(' ');
        Queue<string> commandQueue = new Queue<string>(listCommand);
        commandAction[listCommand[0]].Invoke(commandQueue);
    }

    public AudioHandler()
    {
        definedAudioSources = new Dictionary<string, AudioSourceController>();
        commandAction = new Dictionary<string, Action<Queue<string>>>
        {
            {"play", Play },
            {"change-volume", ChangeVolume },
            {"stop", Stop },
        };
    }

    public void Play(Queue<string> command)
    {
        #region parameters settings
        AudioClip clip;
        float volume = 1;
        bool isLoop = false;
        var paramsActions = new Dictionary<string, Action<Queue<string>>>()
        {
            {"volume", (queue) =>
                {
                    try
                    {
                        volume = Parser.FloatParse(queue.Dequeue()) / 100f;
                    }
                    catch
                    {
                        Debugger.LogError(
                            "play: в параметр volume было передано неправильное кол-во аргументов или не число");
                        return;
                    }
                }
            },

            {"loop", (queue) => isLoop = true }
        };
        #endregion
        command.Dequeue();
        string clipName = "", sourceName = "";
        try
        {
            clipName = command.Dequeue();
            sourceName = command.Dequeue();
        }
        catch
        {
            Debugger.LogError("play: было передано недостаточно аргументов");
            return;
        }

        //TODO: парс аудио клип из папки Resources, конверт переменных, создание (если надо аудио сорс),
        //и обработка доп аргуметов
    }

    public void ChangeVolume(Queue<string> command)
    {
        //TODO: changing volume
    }

    public void Stop(Queue<string> command)
    {
        //TODO: stop playing
    }
}
