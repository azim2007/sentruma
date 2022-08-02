using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SendersList
{
    private static Dictionary<string, string> senders = new Dictionary<string, string>() 
    {
        {"pl", GetColorfulName("222831ff", "Игрок")},
        {"al_man", GetColorfulName("Алоттянец")},
        {"al_woman", GetColorfulName("Алоттянка")},
        {"al_kid", GetColorfulName("Алоттенок")},
    };

    private static string GetColorfulName(string color, string name)
    {
        return "<color=#" + color + ">" + name + "</color>";
    }

    private static string GetColorfulName(string name)
    {
        return GetColorfulName("4f6367ff", name);
    }

    public static string GetSender(string id)
    {
        if (senders.ContainsKey(id))
        {
            return senders[id];
        }

        throw new System.InvalidOperationException("нет сендера с айди: " + id);
    }
}
