using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack
{
    private float damage, recharge;
    public float Damage { get { return damage; } }
    public virtual bool CanUseNow() { return true; }

    public virtual void AttackPlayer() { }
}
