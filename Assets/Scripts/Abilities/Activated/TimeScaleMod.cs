using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleMod : ActivatedAbility
{
    #region Serialized

    public float duration;
    public float decreaseBy;

    #endregion

    public override void StartAbility() {
        base.StartAbility();

        Time.timeScale -= decreaseBy;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

        StartCoroutine(_end());
    }


    IEnumerator _end() {
        yield return new WaitForSeconds(duration);

        Time.timeScale += decreaseBy;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

        base.EndAbility();
    }
}
