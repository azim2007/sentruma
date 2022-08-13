using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Replic
{
    public static readonly string[] textDividers = { "<div>" };
    public string Sender { get; private set; }
    private List<string> Text { get; set; }
    private Action OnEnd { get; set; }

    public Replic(List<string> text)
    {
        Sender = "";
        Text = new List<string>();
        foreach (var e in text)
            Text.Add(e);
    }

    public Replic(UnitsIds senderId, List<string> text) : this(text)
    {
        Sender = SendersList.GetSender(senderId);
    }

    public Replic(List<string> text, Action onEnd) : this(text)
    {
        OnEnd = onEnd;
    }

    public Replic(UnitsIds senderId, List<string> text, Action onEnd) : this(text)
    {
        OnEnd = onEnd;
        Sender = SendersList.GetSender(senderId);
    }

    public Replic(string dividingText)
        : this(new List<string>(dividingText.Split(textDividers, StringSplitOptions.None)))
    { }

    public Replic(UnitsIds senderId, string dividingText) 
        : this(senderId, new List<string> (dividingText.Split(textDividers, StringSplitOptions.None)))
    { }

    public Replic(string dividingText, Action onEnd)
        : this(new List<string>(dividingText.Split(textDividers, StringSplitOptions.None)), onEnd)
    { }

    public Replic(UnitsIds senderId, string dividingText, Action onEnd)
        : this(senderId, new List<string>(dividingText.Split(textDividers, StringSplitOptions.None)), onEnd)
    { }

    public IEnumerable<string> GetText()
    {
        StringBuilder current = new StringBuilder();
        foreach(var e in Text)
        {
            current.Append(e + " ");
            yield return Sender != "" ? current.ToString() : $"*{current}*";
        }

        OnEnd.Invoke();
    }
}
