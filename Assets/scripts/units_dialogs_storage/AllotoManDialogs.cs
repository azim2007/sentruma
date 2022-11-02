using System.Collections.Generic;

public static class AllotoManDialogs
{
    private static Dialog povesitsa = new Dialog(
        new List<Replic>()
        {
            new Replic(UnitsIds.pl, "Я ПОВЕШУСЬ"),
            Replic.ServiceReplic("show char rage"),
            new Replic("*и повесился*")
        },

        null,
        () => true,
        () => { Debugger.Log("on end игрок повесился"); },
        "bg1"
    );

    private static Dialog vskrytsa = new Dialog(
        new List<Replic>()
        {
            new Replic(UnitsIds.pl, "Я вскроюсть"),
            Replic.ServiceReplic("show char1 rage"),
            new Replic("*и вскрылся*")
        },

        null,
        () => true,
        () => { Debugger.Log("on end игрок вскрылся"); },
        "bg1"
    );

    private static Dialog smiritsa = new Dialog(
        new List<Replic>()
        {
            new Replic("*ладно<div> видно мне придется просто смириться*"),
            Replic.ServiceReplic("bg bg"),
            new Replic("*и жить дальше*"),
            Replic.ServiceReplic("show char rage"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
            new Replic(UnitsIds.pl, "лады"),
        },

        null,
        () => true,
        () => { Debugger.Log("on end игрок смирился"); },
        "bg1"
    );

    private static UnitsDialogs dialogs = new UnitsDialogs(
        new List<Dialog>(),

        new Dialog(
            new List<Replic>()
            {
                Replic.ServiceReplic("define char microchel"),
                Replic.ServiceReplic("define char1 examp"),
                Replic.ServiceReplic("define char2 examp"),
                new Replic(UnitsIds.al_man, "Значит так, <b>Меченый</b><div>, я тебя спас, и в благородство играть не буду<div>, выполнишь для меня пару заданий и мы в расчете."),
                Replic.ServiceReplic("bg bg bg-color 0 255 0 fg-color 0 10 150 200"),
                new Replic(UnitsIds.al_man, "Заодно и посмотрим как быстро у тебя голова после амнезии прояснится."),
                Replic.ServiceReplic("show char norm atDistance front atHorizontal left layer 2 atVertical midbottom"),
                Replic.ServiceReplic("show char1 smile atHorizontal right layer 1 with fade 0,5"),
                Replic.ServiceReplic("show char2 rage atDistance back atHorizontal middle layer 1 with fade 2"),
                Replic.ServiceReplic("i love you too"),
                new Replic(UnitsIds.pl, "Ну...<div> ладно"),
                Replic.ServiceReplic("bg bg1"),
                Replic.ServiceReplic("hide char with fade 1,5"),
                Replic.ServiceReplic("destroy char2"),
                //Replic.ServiceReplic("hide char1"),
                Replic.ServiceReplic("bg bg bg-color 0 255 0"),
                new Replic("*Ага, буду я для него выполнять ерунду всякую*"),
                Replic.ServiceReplic("show char rage atDistance midback atHorizontal midright layer 1"),
            },

            new List<System.Tuple<string, Dialog>>()
            {
                new System.Tuple<string, Dialog>("повеситься", povesitsa),
                new System.Tuple<string, Dialog>("вскрыться", vskrytsa),
                new System.Tuple<string, Dialog>("смириться и жить дальше", smiritsa)
            },

            () => true,
            () => { Debugger.Log("asdf"); },
            "bg"
        )
    );

    public static UnitsDialogs Dialogs
    {
        get
        {
            return dialogs;
        }
    }
}
