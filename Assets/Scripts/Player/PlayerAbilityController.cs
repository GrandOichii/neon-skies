using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActivatedAbilityKeyBinding {
    public KeyCode key;
    public ActivatedAbility ability;
} 

public class PlayerAbilityController : MonoBehaviour
{
    #region Serialized

    public List<ActivatedAbilityKeyBinding> abilityKeyBindings;
    
    #endregion

    void Update()
    {
        foreach (var pair in abilityKeyBindings) {
            if (Input.GetKeyDown(pair.key)) {
                pair.ability.Do();
                return;
            }
        }
    }

    public void OnDashStarted() {
    }

    public void OnDashEnded() {
    }
}
