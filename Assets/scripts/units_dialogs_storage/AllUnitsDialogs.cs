using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllUnitsDialogs
{
    private static Dictionary<UnitsIds, UnitsDialogs> unitsDialogsDictionary 
        = new Dictionary<UnitsIds, UnitsDialogs>()
    {
        { UnitsIds.pl, AllotoManDialogs.Dialogs }
    };
    
    public static UnitsDialogs GetById(UnitsIds id)
    {
        if(!unitsDialogsDictionary.ContainsKey(id))
            throw new System.InvalidOperationException("нет диалога с id" + id);

        return unitsDialogsDictionary[id];
    }
}
