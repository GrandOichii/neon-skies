using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    #region Serialized

    public int initialValue;
    public int maxValue;

    #endregion

    #region Events

    public UnityEvent<int> Changed;
    public UnityEvent ReachedZero;

    #endregion

    private int _value;
    public int Value {
        get => _value;
        set {
            _value = value;
            if (_value > maxValue) _value = maxValue;
            if (_value <= 0) {
                _value = 0;
                ReachedZero?.Invoke();
            }
            Changed?.Invoke(_value);
        }
    }

    void Awake() {
        Value = initialValue;
    }
}
