using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class Replic
{
    public static readonly string[] textDividers = { "<div>" };
    public string Sender { get; private set; }
    private List<string> Text { get; set; }
    public Replic(string senderId, List<string> text)
    {
        Sender = SendersList.GetSender(senderId);
        Text = new List<string>();
        foreach(var e in text) 
            Text.Add(e);
    }

    public Replic(string senderId, string dividingText)
    {
        Sender = SendersList.GetSender(senderId);
        Text = new List<string>();
        var texts = dividingText.Split(textDividers, System.StringSplitOptions.None);
        foreach (var e in texts)
            Text.Add(e);
    }

    public Replic(List<string> text)
    {
        Sender = "";
        Text = new List<string>();
        foreach (var e in text)
            Text.Add(e);
    }

    public IEnumerable<string> GetText()
    {
        StringBuilder current = new StringBuilder();
        foreach(var e in Text)
        {
            current.Append(e + " ");
            yield return Sender != "" ? current.ToString() : $"*{current}*";
        }
    }
}
