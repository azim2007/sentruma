﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    private List<Replic> DialogReplics { get; set; }
    private Func<bool> CanStart { get; set; }
    private Action onEnd;
    private Action OnEnd { 
        get { return onEnd; } 

        set 
        { 
            onEnd = () => 
            { 
                value(); 
                IsShowed = true; 
            };
        } 
    }

    private bool IsShowed { get; set; }
    public bool IsCurrent { get { return !IsShowed && CanStart(); } }

    public Dialog(List<Replic> dialog, Func<bool> canStart, Action onEnd, bool isShowed)
    {
        this.DialogReplics = dialog;
        this.CanStart = canStart;
        this.OnEnd = onEnd;
        this.IsShowed = isShowed;
    }

    public Dialog(List<Replic> dialog, Func<bool> canStart, Action onEnd) : this(dialog, canStart, onEnd, false) { }

    public IEnumerable<Tuple<string, string>> GetReplic()
    {
        foreach(var rep in DialogReplics)
        {
            foreach(var text in rep.GetText())
            {
                yield return new Tuple<string, string> (rep.Sender, text);
            }
        }
        OnEnd();
        Debug.Log("GetReplic completed");
    }
}