using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class SoundEmitterListenerController : MonoBehaviour
{
    #region Serialized


    #endregion

    #region Events

    public UnityEvent<Collider2D> soundHeard;

    #endregion

    private CircleCollider2D _collider;

    void Awake() {
        _collider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.CompareTag("sound")) {
            soundHeard.Invoke(collider);
        }
    }
}
