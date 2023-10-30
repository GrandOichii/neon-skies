using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class EquippedImplantDisplayMapping {
    public string slotName;
    public GameObject display;
}

public class ImplantManager : MonoBehaviour
{
    #region Serialized

    public ImplantController implantController;
    public List<EquippedImplantDisplayMapping> displayMappings;
    public GameObject implantGrid;
    public GameObject implantDisplayTemplate;

    #endregion

    #region Events



    #endregion

    private Dictionary<string, ImplantDisplaySlot> _equippedDisplayMapping;


    void Awake() {
        implantController.SlotMapInitiated.AddListener(() => _onSlotMapInitiated());
    }

    private void _onSlotMapInitiated() {
        _equippedDisplayMapping = new();
        foreach (var pair in displayMappings) {
            var ds = pair.display.GetComponent<ImplantDisplaySlot>();
            
            // add listeners
            ds.UnequippedImplant.AddListener(OnImplantUnequipped);
            ds.EquippedImplant.AddListener(OnImplantEquipped);
            
            _equippedDisplayMapping.Add(pair.slotName, ds);
            ds.SlotName = pair.slotName;
        }
        foreach (var v in implantController.SlotMap.Values) {
            if (!_equippedDisplayMapping.ContainsKey(v.name)) continue;
            _equippedDisplayMapping[v.name].Accepts = v.slot;
            
            // create equipped implants
            if (v.Implant == null) continue;

            var child = Instantiate(implantDisplayTemplate);
            var id = child.GetComponent<ImplantDisplay>();
            id.Implant = v.Implant;
            id.SlotName = v.name;
            child.transform.SetParent(_equippedDisplayMapping[v.name].displayHolder.transform);
        }
    
        foreach (var implant in implantController.implants) {
            var child = Instantiate(implantDisplayTemplate);
            child.GetComponent<ImplantDisplay>().Implant = implant;
            child.transform.SetParent(implantGrid.transform);
        }

        // create implants
        // implantController.implants
    }

    public void OnImplantUnequipped(string slotName) {
        print("UNEQUIPPED " + slotName);
        implantController.Uninstall(slotName);
    }

    public void OnImplantEquipped(string slotName, Object o){
        var implant = o as Implant;

        implantController.Install(slotName, implant);
    } 
}
