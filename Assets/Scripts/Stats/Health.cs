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
    public bool immortal = false;

    #endregion

    #region Events

    public UnityEvent<int> Changed;
    public UnityEvent<int> MaxChanged;
    public UnityEvent ReachedZero;

    #endregion

    private int _value;
    public int Value {
        get => _value;
        set {
            _value = value;
            if (_value > MaxValue) _value = MaxValue;
            if (_value <= 0 && !immortal) {
                _value = 0;
                ReachedZero?.Invoke();
            }
            Changed?.Invoke(_value);
        }
    }

    private int _maxValue;
    public int MaxValue {
        get => _maxValue;
        set {
            var old = _maxValue;
            var diff = value - _maxValue;
            _maxValue = value;
            if (_value > _maxValue) _value = _maxValue;
            if (diff > 0) {
                Value += diff;
            }

            MaxChanged?.Invoke(_maxValue);
        }
    }

    void Awake() {
        MaxValue = maxValue;
        Value = initialValue;
    }
}
