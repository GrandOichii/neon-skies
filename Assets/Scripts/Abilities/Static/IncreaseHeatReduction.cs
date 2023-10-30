using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeatManager))]
public class IncreaseHeatReduction : StaticAbility
{
    #region Serialized

    public float increaseBy;

    #endregion

    public override void Activate()
    {
        base.Activate();
        GetComponent<HeatManager>().reduceHeatBy += increaseBy;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        GetComponent<HeatManager>().reduceHeatBy -= increaseBy;
    }
}
