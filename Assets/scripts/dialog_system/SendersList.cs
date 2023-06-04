using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SendersList
{
    private static Dictionary<UnitsIds, string> senders = new Dictionary<UnitsIds, string>() 
    {
        {UnitsIds.pl, GetColorfulName("222831ff", "Игрок")},
        {UnitsIds.al_man, GetColorfulName("Алоттянец")},
        {UnitsIds.al_woman, GetColorfulName("Алоттянка")},
        {UnitsIds.al_kid, GetColorfulName("Алоттенок")},
        {UnitsIds.al_jug, GetColorfulName("11f3e5ff" ,"Кувшин")},
    };

    private static Dictionary<UnitsIds, string> sendersInfo = new Dictionary<UnitsIds, string>() 
    {
        { UnitsIds.pl, "это типо игрок. За него вы играете"},
        { UnitsIds.al_man, "это есть аллото житель всеведущий, относитесь с уважением"},
        { UnitsIds.al_woman, "это есть самка аллото, можно пнуть и харкнуть"},
        { UnitsIds.al_kid, "это есть детеныш аллото, угостите его"},
        { UnitsIds.al_jug, "это кувшин, суда можно вещи сложить и взять их. Только не наглей, праведник!"},
    };

    private static string GetColorfulName(string color, string name)
    {
        return "<color=#" + color + ">" + name + "</color>";
    }

    private static string GetColorfulName(string name)
    {
        return GetColorfulName("4f6367ff", name);
    }

    public static string GetSender(UnitsIds id)
    {
        if (senders.ContainsKey(id))
        {
            return senders[id];
        }

        throw new System.InvalidOperationException("нет сендера с айди: " + id);
    }

    public static string GetSenderInfo(UnitsIds id)
    {
        if (sendersInfo.ContainsKey(id))
        {
            return sendersInfo[id];
        }

        throw new System.InvalidOperationException("нет сендера с айди: " + id);
    }
}
