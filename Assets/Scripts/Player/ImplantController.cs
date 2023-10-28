using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AbilityMapping {
    public string key;
    // TODO change
    public Ability ability;
}

[System.Serializable]
public class ImplantSlotContainer {
    public string name;
    public ImplantSlot slot;

    #nullable enable
    [SerializeField] Implant? _implant;
    public Implant? Implant {
        get => _implant;
        set {
            _implant = value;
        }
    }
    #nullable disable

}

public class ImplantController : MonoBehaviour
{
    #region Serialized

    public List<AbilityMapping> abilityMappings;
    public List<ImplantSlotContainer> implantSlots;

    #endregion
    #region Events

    public UnityEvent<string, Object> ImplantUneqiupped;
    public UnityEvent<string, Object> ImplantEquipped;

    #endregion

    private Dictionary<string, Ability> _abilityMap;

    private Dictionary<string, ImplantSlotContainer> _slotMap;

    void Start() {
        _abilityMap = new();
        foreach (var mapping in abilityMappings) {
            _abilityMap.Add(mapping.key, mapping.ability);
        }

        _slotMap = new();
        foreach (var slot in implantSlots) {
            _slotMap.Add(slot.name, slot);
            Install(slot.name, slot.Implant);
        }
    }

    public void Install(string name, Implant implant) {
        var slot = _slotMap[name];

        var prev = slot.Implant;

        // disable previous abilities
        foreach (var key in prev.enables) {
            var ability = _abilityMap[key];
            --ability.ActiveCounter;
        }
        ImplantUneqiupped.Invoke(name, prev);

        // set implant in slot
        slot.Implant = implant;

        // enable new abilities
        foreach (var key in prev.enables) {
            var ability = _abilityMap[key];
            ++ability.ActiveCounter;
        }
        ImplantEquipped.Invoke(name, implant);

        // gameObject.AddComponent()
    }
}
