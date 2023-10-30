using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImplantGrid : MonoBehaviour, IDropHandler
{
    #region Events

    public UnityEvent<string> UnequippedImplant;

    #endregion

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        ImplantDisplay display = dropped.GetComponent<ImplantDisplay>();
        display.DragParent = transform;

        var prevSlot = display.SlotName;
        if (prevSlot.Length == 0) {
            return;
        }
        display.SlotName = "";
        UnequippedImplant.Invoke(prevSlot);
    }

}
