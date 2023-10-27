using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityKeyBinding {
    public KeyCode key;
    public Ability ability;
} 

public class PlayerAbilityController : MonoBehaviour
{
    #region Serialized

    public List<AbilityKeyBinding> abilityKeyBindings;
    
    #endregion

    void Update()
    {
        foreach (var pair in abilityKeyBindings) {
            if (Input.GetKeyDown(pair.key)) {
                pair.ability.Do(this);
                return;
            }
        }
    }
}
