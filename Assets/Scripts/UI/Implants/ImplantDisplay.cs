using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class ImplantDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Serialized

    public Image imageDisplay;
    public TMP_Text nameDisplay;
    public Implant initialImplant;

    #endregion

    private Implant _implant;
    public Implant Implant {
        get => _implant;
        set {
            _implant = value;

            // TODO add
            // imageDisplay.sprite = _implant.image;
            nameDisplay.text = _implant.name;
        }
    }

    public string SlotName { get; set; } = "";

    #region Drag 

    public Transform DragParent { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        GetComponent<Image>().raycastTarget = false;
        imageDisplay.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DragParent);
        imageDisplay.raycastTarget = true;
        GetComponent<Image>().raycastTarget = true;
    }

    #endregion

    void Start() {
        Implant = initialImplant;
    }
}
