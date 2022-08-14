using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    private List<Replic> DialogReplics { get; set; }
    private Func<bool> CanStart { get; set; }

    private Func<List<Tuple<string, Dialog>>> onEnd;
    private Func<List<Tuple<string, Dialog>>> GetOnEnd {
        get { return onEnd; }
    }

    private Action SetOnEnd
    {
        set
        {
            onEnd = () =>
            {
                value.Invoke();
                IsShowed = true;
                return answersAndNextDialogs;
            };
        }
    }

    private List<Tuple<string, Dialog>> answersAndNextDialogs;
    private bool IsShowed { get; set; }
    public bool IsCurrent { get { return !IsShowed && CanStart(); } }

    public Dialog(List<Replic> dialog, Func<bool> canStart, Action onEnd, bool isShowed)
    {
        this.DialogReplics = dialog;
        this.CanStart = canStart;
        this.SetOnEnd = onEnd;
        this.IsShowed = isShowed;
    }

    public Dialog(List<Replic> dialog, Func<bool> canStart, Action onEnd) : this(dialog, canStart, onEnd, false) { }

    public IEnumerable<Tuple<string, string>> GetReplic(List<Tuple<string, Dialog>> answersAndNextDialogs)
    {
        foreach(var rep in DialogReplics)
        {
            foreach(var text in rep.GetText())
            {
                yield return new Tuple<string, string> (rep.Sender, text);
            }
        }

        foreach(var e in GetOnEnd.Invoke())
        {
            answersAndNextDialogs.Add(e);
        }
        
        Debugger.Log("GetReplic completed");
    }
}
