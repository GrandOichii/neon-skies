using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImplantManager : MonoBehaviour
{
    #region Serialized

    public ImplantController implantController;

    #endregion

    #region Events



    #endregion


    void Awake() {
        implantController.SlotMapInitiated.AddListener(() => _onSlotMapInitiated());
    }

    private void _onSlotMapInitiated() {

    }
}
