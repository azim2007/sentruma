using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class IFightUnit
{
    private float speed;
    private List<Attack> attacks;
    public IFightUnit(float speed, List<Attack> attacks)
    {
        this.attacks = attacks;
        this.speed = speed;
        this.attacks.OrderByDescending(a => a.Damage);
    }

    void StartFight(GameObject thisUnit, GameObject other)
    {

    }
}
