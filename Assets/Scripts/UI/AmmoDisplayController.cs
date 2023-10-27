using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplayController : MonoBehaviour
{
    #region Serialized

    public TMP_Text magazineDisplay;
    public TMP_Text totalDisplay;

    #endregion

    

    public void OnTotalAmmoUpdated(int total) {
        totalDisplay.text = total.ToString();
    }

    public void OnMagazineAmmoUpdated(int magazine) {
        magazineDisplay.text = magazine.ToString();
    }
}
