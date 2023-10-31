using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AbilityMapping {
    public string key;
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

    public List<ImplantSlotContainer> implantSlots;
    public List<Implant> implants;

    #endregion
    #region Events

    // TODO fix
    public UnityEvent<string, Object> ImplantUneqiupped;
    public UnityEvent<string, Object> ImplantEquipped;
    public UnityEvent SlotMapInitiated;

    #endregion

    private Dictionary<Implant, List<Ability>> _activationMap;
    public Dictionary<string, ImplantSlotContainer> SlotMap { get; private set; }

    void Awake() {
        _activationMap = new();
        foreach (var a in GetComponents<Ability>()) {
            foreach (var i in a.enabledBy) {
                if (!_activationMap.ContainsKey(i)) _activationMap.Add(i, new());
                _activationMap[i].Add(a);
            }
        }

        SlotMap = new();
        foreach (var slot in implantSlots) {
            SlotMap.Add(slot.name, slot);
            if (slot.Implant == null) continue;
            Install(slot.name, slot.Implant);
        }

        SlotMapInitiated?.Invoke();
    }

    public void Uninstall(string name) {

        var slot = SlotMap[name];

        var prev = slot.Implant;
        if (prev == null) {
            return;
        }

        // getcomponents

        // disable previous abilities
        foreach (var a in _activationMap[prev]) {
            --a.ActiveCounter;
        }

        slot.Implant = null;
        ImplantUneqiupped.Invoke(name, prev);
    }

    public void Install(string name, Implant implant) {
        Uninstall(name);
        var slot = SlotMap[name];

        // set implant in slot
        slot.Implant = implant;

        // enable new abilities
        foreach (var a in _activationMap[implant]) {
            ++a.ActiveCounter;
        }

        ImplantEquipped.Invoke(name, implant);

        // gameObject.AddComponent()
    }
}
