using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : ActingObjectsController
{
    private void Start()
    {
        base.UnitId = UnitsIds.al_door;
    }

    public override void OnPeacefulAction()
    {
        transform.Rotate(0, 0, (transform.rotation.z == 0f ? -90f : 90f));
    }
}
