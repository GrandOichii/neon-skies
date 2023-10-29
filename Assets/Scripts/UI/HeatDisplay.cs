using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeatDisplay : MonoBehaviour
{
    #region Serialized

    public TMP_Text currentDisplay;
    public TMP_Text threshDisplay;

    public RectTransform mainBar;
    public Image progressBar;
    public RectTransform limitBar;

    #endregion

    private float _thresh;

    public void OnHeatChanged(float heat) {
        currentDisplay.text = heat.ToString();

        var limitW = limitBar.rect.width;
        var mainW = mainBar.rect.width;
        var targetX = heat * mainW / _thresh;
        targetX = limitW - targetX;
        progressBar.rectTransform.offsetMax = new (-targetX, progressBar.rectTransform.offsetMax.y);
        
        // targetX = System.Math.Min(targetX, limitW);
        // progressBar.transform.position = new(targetX, progressBar.transform.position.y, progressBar.transform.position.y);

        // max - main.width
        // current - ?
    }

    public void OnThreshChanged(float thresh) {
        threshDisplay.text = thresh.ToString();
        _thresh = thresh;
    }
}
