using System.Collections;
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
                new Replic(UnitsIds.pl, "Ну...<div> ладно"),
                new Replic("*Ага, буду я для него выполнять ерунду всякую*"),
            },

            null,
            () => true,
            () => { },
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
