using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Heal : ActivatedAbility
{
    #region Serialized

    public int amount = 1;
    public float healtAfter;

    #endregion

    public override void StartAbility() {
        base.StartAbility();
        StartCoroutine(_end());
    }


    IEnumerator _end() {
        yield return new WaitForSeconds(healtAfter);

        GetComponent<Health>().Value += amount;
        base.EndAbility();
    }
}
