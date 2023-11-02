using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImplantDisplaySlot : MonoBehaviour, IDropHandler
{
    #region Serialized

    public GameObject displayHolder;
    public TMP_Text slotNameDispay;

    #endregion

    #region Events

    public UnityEvent<string> UnequippedImplant;
    public UnityEvent<string, Object> EquippedImplant;


    #endregion
    
    #nullable enable
    public  ImplantDisplay? Current { get; set; }
    #nullable disable

    private string _slotName;
    public string SlotName {
        get => _slotName;
        set {
            _slotName = value;

            slotNameDispay.text = _slotName;
        }
    }

    public ImplantSlot Accepts { get; set; }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        ImplantDisplay display = dropped.GetComponent<ImplantDisplay>();
        if (display.Implant.slot != Accepts) return;

        if (Current != null) {
            // unequipping
            Current.transform.SetParent(display.DragParent);
            display.SlotName = "";
            UnequippedImplant.Invoke(SlotName);
            Current = null;
        }
        
        display.DragParent = displayHolder.transform;
        Current = display;
        
        display.SlotName = SlotName;
        EquippedImplant.Invoke(SlotName, display.Implant);
    }
}
