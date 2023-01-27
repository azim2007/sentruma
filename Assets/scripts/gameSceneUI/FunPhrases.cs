using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// класс-хранилище анекдотов для вывода во время игры
/// </summary>
public static class FunPhrases
{
    private static List<string> phrases = new List<string>()
    {
        "Может майн и фашизм для него одно и тоже, ведь у руля всё равно стоит мужик " +
        "с растительностью на лице.",

        "Встречается парень с девушкой: \n" +
        "— Это не вы вчера вечером танцевали на столе в одном нижнем белье? \n" +
        "— Вы, видимо, рано ушли.)))",

        "Смейтесь просто так.",
    };

    public static string GetRandom()
    {
        return phrases[Random.Range(0, phrases.Count)];
    }
}
