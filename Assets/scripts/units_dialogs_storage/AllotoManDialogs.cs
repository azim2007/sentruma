﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AllotoManDialogs
{
    private static UnitsDialogs dialogs = new UnitsDialogs(
        new List<Dialog>(),

        new Dialog(
            new List<Replic>()
            {
                new Replic(UnitsIds.al_man, "Значит так, <b>Меченый</b><div>, я тебя спас, и в благородство играть не буду<div>, выполнишь для меня пару заданий и мы в расчете."),
                new Replic(UnitsIds.al_man, "Заодно и посмотрим как быстро у тебя голова после амнезии прояснится."),
                Replic.ServiceReplic("i love you"),
                Replic.ServiceReplic("i love you too"),
                new Replic(UnitsIds.pl, "Ну...<div> ладно"),
                Replic.ServiceReplic("yay"),
                new Replic("*Ага, буду я для него выполнять ерунду всякую*"),
            },

            new List<System.Tuple<string, Dialog>>()
            {
                new System.Tuple<string, Dialog>(
                    "повеситься",
                    new Dialog(
                        new List<Replic>()
                        {
                            new Replic(UnitsIds.pl, "Я ПОВЕШУСЬ"),
                            new Replic("*и повесился*")
                        },

                        null,
                        () => true,
                        () => {Debugger.Log("on end игрок повесился"); },
                        "bg"
                    )
                ),
                new System.Tuple<string, Dialog>("вскрыться", null),
                new System.Tuple<string, Dialog>("смириться и жить дальше", null)
            },

            () => true,
            () => { Debugger.Log("asdf"); },
            "bg"
        )
    );

    public static UnitsDialogs Dialogs { 
        get 
        {
            return dialogs; 
        } 
    }
}
