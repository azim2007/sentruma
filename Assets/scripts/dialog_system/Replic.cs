using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Replic
{
    public static readonly string[] textDividers = { "<div>" };
    public static readonly string serviceSenderName = "$_service";
    public string Sender { get; private set; }
    private List<string> Text { get; set; }

    public bool IsService { 
        get
        {
            return Sender == serviceSenderName;
        } 
    }

    public static Replic ServiceReplic (string command)
    {
        return new Replic (serviceSenderName, command.Split(' ').ToList());
    }

    private Replic(string serviceSender, List<string> command)
    {
        Sender = serviceSender;
        Text = new List<string>();
        foreach (var e in command)
            Text.Add(e);
    }

    public Replic(List<string> text)
    {
        Sender = "";
        Text = new List<string>();
        foreach (var e in text)
            Text.Add(e);
    }

    public Replic(UnitsIds senderId, List<string> text)
    {
        Sender = SendersList.GetSender(senderId);
        Text = new List<string>();
        foreach (var e in text)
            Text.Add(e);
    }

    public Replic(string dividingText)
        : this(new List<string>(dividingText.Split(textDividers, StringSplitOptions.None)))
    { }

    public Replic(UnitsIds senderId, string dividingText) 
        : this(senderId, new List<string> (dividingText.Split(textDividers, StringSplitOptions.None)))
    { }

    public IEnumerable<string> GetText()
    {
        foreach(var e in Text)
        {
            yield return e;
        }
    }
}
