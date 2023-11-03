using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmittersController : MonoBehaviour
{
    #region Serialized

    public CircleCollider2D gunFiredSoundEmitter;
    public float stayFor;

    #endregion


    void Awake() {
        gunFiredSoundEmitter.gameObject.SetActive(false);
    }

    public void OnGunFired() {
        gunFiredSoundEmitter.gameObject.SetActive(true);
        StartCoroutine(_endGunSound());
    }

    IEnumerator _endGunSound() {
        yield return new WaitForSeconds(stayFor);
        gunFiredSoundEmitter.gameObject.SetActive(false);
    }
}
