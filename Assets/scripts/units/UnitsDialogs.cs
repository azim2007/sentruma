using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsDialogs
{
    private List<Dialog> UnitsDialogsList { get; set; }

    private Dialog DefaultDialog { get; set; }

    public UnitsDialogs(List<Dialog> unitsDialogsList, Dialog defaultDialog)
    {
        UnitsDialogsList = unitsDialogsList;
        DefaultDialog = defaultDialog;
    }

    public Dialog GetCurrent()
    {
        if(UnitsDialogsList != null)
        {
            foreach (var e in UnitsDialogsList)
            {
                if (e.IsCurrent)
                    return e;
            }
        }
        
        return null;
    }

    public Dialog GetCurrentOrDefault()
    {
        return GetCurrent() ?? DefaultDialog;
    }
}
