using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsDialogs
{
    private List<Dialog> unitsDialogsList;
    private Dialog defaultDialog;

    public UnitsDialogs(List<Dialog> unitsDialogsList, Dialog defaultDialog)
    {
        this.unitsDialogsList = unitsDialogsList;
        this.defaultDialog = defaultDialog;
    }

    public Dialog GetCurrent()
    {
        if (unitsDialogsList != null)
        {
            for (int i = unitsDialogsList.Count - 1; i >= 0; i--)
            {
                if (unitsDialogsList[i].IsCurrent)
                    return unitsDialogsList[i];
            }
        }
        
        return null;
    }

    public Dialog GetCurrentOrDefault()
    {
        return GetCurrent() ?? defaultDialog;
    }
}
