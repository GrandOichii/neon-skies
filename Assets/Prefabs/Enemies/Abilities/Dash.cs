using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Dash : Ability
{
    #region Serialized
    
    public float duration;
    public float speedMod;

    #endregion
    
    public override void StartAbility(UnityEngine.Object o) {
        base.StartAbility(o);
        var m = o.GetComponent<Movement>();
        var baseSpeed = m.moveSpeed;
        m.moveSpeed += speedMod;
        StartCoroutine(_end(m, baseSpeed));
    }


    IEnumerator _end(Movement m, float returnTo) {
        yield return new WaitForSeconds(duration);

        m.moveSpeed = returnTo;
        base.EndAbility();
    }
}
