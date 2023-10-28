using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImplantDisplay : MonoBehaviour
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

            // imageDisplay.sprite = _implant.image;
            nameDisplay.text = _implant.name;
        }
    }

    void Start() {
        if (initialImplant != null) {
            Implant = initialImplant;
        }
    }
}
