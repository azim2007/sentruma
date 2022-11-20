using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioHandler : ICommandHandler
{
    private Dictionary<string, Action<Queue<string>>> commandAction;
    private Dictionary<string, AudioSourceController> definedAudioSources;
    private DialogFactory dialogFactory;
    private GameObject audioSourcesParent;
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

    public AudioHandler(GameObject audioSourcesParent)
    {
        this.audioSourcesParent = audioSourcesParent;
        dialogFactory = new DialogFactory();
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
        AudioClip clip = null;
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

        try
        {
            clip = Resources.Load<AudioClip>(GetFolder(clipName));
        }
        catch
        {
            Debugger.LogError("play: нет композиции " + clipName);
        }

        if (!definedAudioSources.ContainsKey(sourceName))
        {
            definedAudioSources.Add(sourceName, dialogFactory.Instantiate("audioSrc")
                .GetComponent<AudioSourceController>());
            definedAudioSources[sourceName].transform.SetParent(audioSourcesParent.transform);
        }

        string action;
        while (command.TryDequeue(out action))
        {
            if (!paramsActions.ContainsKey(action))
            {
                Debugger.LogError("play: нет параметра " + action);
            }

            paramsActions[action](command);
        }

        definedAudioSources[sourceName].Play(clip, volume, isLoop);
    }

    public void ChangeVolume(Queue<string> command)
    {
        string sourceName, newVolume;
        float newVolumeValue;
        command.Dequeue();
        try
        {
            sourceName = command.Dequeue();
            newVolume = command.Dequeue();
        }
        catch
        {
            Debugger.LogError("change-volume: было передано недостаточно параметров");
            return;
        }

        try
        {
            newVolumeValue = Parser.FloatParse(newVolume) / 100f;
        }
        catch
        {
            Debugger.LogError("change-volume: в качестве volume было передано не число, а " + newVolume);
            return;
        }

        if (!definedAudioSources.ContainsKey(sourceName))
        {
            Debugger.LogError("change-colume: нет источника звука " + sourceName);
            return;
        }

        definedAudioSources[sourceName].ChangeVolume(newVolumeValue);
    }

    public void Stop(Queue<string> command)
    {
        string sourceName;
        command.Dequeue();
        try
        {
            sourceName = command.Dequeue();
        }
        catch
        {
            Debugger.LogError("stop: было передано недостаточно параметров");
            return;
        }

        if (!definedAudioSources.ContainsKey(sourceName))
        {
            Debugger.LogError("stop: нет источника звука " + sourceName);
            return;
        }

        definedAudioSources[sourceName].Stop();
    }
}
