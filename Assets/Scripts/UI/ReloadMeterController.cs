using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReloadMeterController : MonoBehaviour
{
    #region Serialized

    public Color BgColor;
    public Color FgColor;
    public Color FailedHotReloadFgColor;
    public Color HotReloadZoneColor;

    public Image mainBar;
    public Image progressBar;


    #endregion
    #region Events

    public UnityEvent Ejected;

    #endregion

    private Gun _current;

    private void _hide() {
        mainBar.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
    }

    public void OnGunEquipped(Object gun) {
        _current = gun as Gun;
    }

    public void Start() {
        // set colors

        mainBar.color = BgColor;
        progressBar.color = FgColor;

        _hide();
    }

    public void Eject() {
        mainBar.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        
        var height = progressBar.rectTransform.rect.height;
        progressBar.transform.localPosition -= Vector3.down * height;
        
        LeanTween.moveLocalY(progressBar.gameObject, 0, _current.reloadTime).setOnComplete(() => {
            _hide();
            Ejected.Invoke();
        });
    }
}
