using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : BehaviorAgent
{
    private int ammoAmount = 20;
    public const int damageAmount = 20;

    public override void Attack(BehaviorAgent target)
    {
        base.Attack(target);

        if (ammoAmount > 0)
        {
            target.TakeDamage(damageAmount);
            ammoAmount--;
        }
        else
            Debug.Log("Not enough ammo!");
    }
}
