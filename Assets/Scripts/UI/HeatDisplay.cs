using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeatDisplay : MonoBehaviour
{
    #region Serialized

    public TMP_Text currentDisplay;
    public TMP_Text threshDisplay;

    #endregion

    public void OnHeatChanged(float heat) {
        currentDisplay.text = heat.ToString();
    }

    public void OnThreshChanged(float thresh) {
        threshDisplay.text = thresh.ToString();
    }
}
