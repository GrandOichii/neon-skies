using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class MaxHealthIncrease : StaticAbility
{
    #region Serialized

    public int increaseBy;

    #endregion

    public override void Activate()
    {
        base.Activate();
        GetComponent<Health>().MaxValue += increaseBy;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        GetComponent<Health>().MaxValue -= increaseBy;
    }
}
