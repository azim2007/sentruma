using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog
{
    private List<Replic> dialogReplics;
    private Func<bool> canStart;

    public string BackgroundName { get; private set; }

    private Action onEnd;

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

    public bool IsCurrent { get { return canStart(); } }

    public Dialog(List<Replic> dialog, List<Tuple<string, Dialog>> answersAndNextDialogs, 
        Func<bool> canStart, Action onEnd, string backgroundName)
    {
        this.dialogReplics = dialog;
        this.canStart = canStart;
        this.onEnd = onEnd;
        this.BackgroundName = backgroundName;
        this.AnswersAndNextDialogs = answersAndNextDialogs;
    }

    public IEnumerable<Replic> GetReplic()
    {
        foreach(var rep in dialogReplics)
        {
            yield return rep;
        }

        onEnd.Invoke();
        
        Debugger.Log("GetReplic completed");
    }
}
