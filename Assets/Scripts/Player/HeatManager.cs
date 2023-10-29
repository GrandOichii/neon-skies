using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ControlledAbility {
    public ActivatedAbility ability;
    public float heat;
}

public class HeatManager : MonoBehaviour
{
    #region Serialized

    public float initialHeatThresh;
    public float reduceHeatInterval;
    public float reduceHeatBy;    
    public int damageOnOverheat;
    public List<ControlledAbility> controlledAbilities;

    public Health health;

    #endregion

    #region Events

    public UnityEvent<float> heatChanged;
    public UnityEvent<float> heatThreshChanged;
    public UnityEvent<int> damagedByHeat;

    #endregion

    private float _heat;
    public float Heat {
        get => _heat;
        set {
            if (value < 0) {
                value = 0;
            }

            _heat = value;
            heatChanged.Invoke(_heat);
        }
    }

    private float _heatThresh;
    public float HeatThresh {
        get => _heatThresh;
        set {
            _heatThresh = value;
            heatThreshChanged.Invoke(_heatThresh);
        }
    }

    void Awake() {
        HeatThresh = initialHeatThresh;
        foreach (var controlled in controlledAbilities) {
            var ca = controlled;
            var ability = controlled.ability;
            ability.AbilityStarted.AddListener(() => AddHeat(ca.heat));
        }
        StartCoroutine(_decreaseHeat());
    }   

    public void AddHeat(float heat) {
        if (Heat > HeatThresh) {
            var damage = damageOnOverheat;
            health.Value -= damage;
            damagedByHeat.Invoke(damage);
        }
        Heat += heat;
    } 

    IEnumerator _decreaseHeat() {
        while (true) {
            Heat -= reduceHeatBy;
            yield return new WaitForSeconds(reduceHeatInterval);
        }
    }
}
