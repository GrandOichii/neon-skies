using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement))]
public class Dash : ActivatedAbility
{
    #region Serialized
    
    public float duration;
    public float speedMod;

    #endregion
    
    public override void StartAbility() {
        base.StartAbility();
        var m = GetComponent<Movement>();
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
