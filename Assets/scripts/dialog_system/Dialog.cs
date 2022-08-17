using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog
{
    private List<Replic> DialogReplics { get; set; }

    private Func<bool> CanStart { get; set; }

    public string BackgroundName { get; private set; }

    private Action onEnd;

    private Action OnEnd
    {
        get { return onEnd; }

        set
        {
            onEnd = () =>
            {
                value.Invoke();
                IsShowed = true;
            };
        }
    }

    public bool HasAnswers
    {
        get
        {
            if(AnswersAndNextDialogs != null)
                return AnswersAndNextDialogs.Count > 0;

            return false;
        }
    }

    public List<Tuple<string, Dialog>> AnswersAndNextDialogs { get; private set; }

    private bool IsShowed { get; set; }

    public bool IsCurrent { get { return !IsShowed && CanStart(); } }

    public Dialog(List<Replic> dialog, List<Tuple<string, Dialog>> answersAndNextDialogs, Func<bool> canStart, Action onEnd, string backgroundName, bool isShowed)
    {
        this.DialogReplics = dialog;
        this.CanStart = canStart;
        this.OnEnd = onEnd;
        this.BackgroundName = backgroundName;
        this.IsShowed = isShowed;
        this.AnswersAndNextDialogs = answersAndNextDialogs;
    }

    public Dialog(List<Replic> dialog, List<Tuple<string, Dialog>> answersAndNextDialogs, Func<bool> canStart, Action onEnd, string backgroundName)
        : this(dialog: dialog, canStart: canStart, onEnd: onEnd, backgroundName: backgroundName, isShowed: false, answersAndNextDialogs: answersAndNextDialogs) { }

    public IEnumerable<Replic> GetReplic()
    {
        foreach(var rep in DialogReplics)
        {
            yield return rep;
        }

        OnEnd.Invoke();
        
        Debugger.Log("GetReplic completed");
    }
}
