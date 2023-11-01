using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ReloadType {
    NONE,
    NORMAL,
    HOT,
    FAILED_HOT
}

public class ReloadMeterController : MonoBehaviour
{
    #region Serialized

    public Color BgColor;
    public Color FgColor;
    public Color FailedHotReloadFgColor;
    public Color HotReloadZoneColor;

    public Image mainBar;
    public Image progressBar;
    public Image hotBar;


    #endregion
    #region Events

    public UnityEvent Ejected;
    public UnityEvent<ReloadType> Reloaded;

    #endregion

    private Gun _current;
    private Gun Current {
        get => _current;
        set {
            _current = value;
            var mbHeight = progressBar.rectTransform.rect.height;

            hotBar.rectTransform.offsetMax = new (hotBar.rectTransform.offsetMax.x, -mbHeight * (_current.reloadTime - _current.hotReloadIntervalEnd) / _current.reloadTime);
            hotBar.rectTransform.offsetMin = new (hotBar.rectTransform.offsetMin.x, mbHeight * _current.hotReloadIntervalStart / _current.reloadTime);
        }
    }

    private ReloadType _rt = ReloadType.NONE;
    public ReloadType ReloadType {
        get => _rt;
        set {
            _rt = value;

            switch (_rt) {
            case ReloadType.NORMAL:
                hotBar.color = HotReloadZoneColor;
                hotBar.gameObject.SetActive(true);
                break;
            case ReloadType.FAILED_HOT:
                hotBar.gameObject.SetActive(false);
                progressBar.color = FailedHotReloadFgColor;
                break;
            case ReloadType.HOT:
                LeanTween.cancel(progressBar.gameObject);
                _endReload();
                break;
            }
        }
    }

    private LTDescr _reloadTween;

    private void _hide() {
        mainBar.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
        hotBar.gameObject.SetActive(false);
    }

    public void OnGunEquipped(Object gun) {
        Current = gun as Gun;
    }

    void Start() {
        mainBar.color = BgColor;
        progressBar.color = FgColor;
        
        _hide();
    }

    public void Eject() {
        mainBar.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        
        var height = progressBar.rectTransform.rect.height;
        progressBar.transform.localPosition = new(progressBar.transform.localPosition.x, 0, progressBar.transform.localPosition.z);
        // progressBar.transform.localPosition -= Vector3.down * height;
        
        LeanTween.moveLocalY(progressBar.gameObject,-height, Current.ejectTime).setOnComplete(() => {
            _hide();
            Ejected.Invoke();
        });
    }

    public void Reload() {
        if (ReloadType != ReloadType.NONE) {
            // player is attempting to hot reload
            if (ReloadType == ReloadType.FAILED_HOT) {
                return;
            }
            var pos = -progressBar.transform.localPosition.y;
            var bottom = -hotBar.rectTransform.offsetMax.y;
            var top = progressBar.rectTransform.rect.height - hotBar.rectTransform.offsetMin.y;
            ReloadType = (pos > top || pos < bottom) ? ReloadType.FAILED_HOT : ReloadType.HOT;

            return;
        }

        mainBar.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);

        var height = progressBar.rectTransform.rect.height;
        progressBar.transform.localPosition = new(progressBar.transform.localPosition.x, -height, progressBar.transform.localPosition.z);
        // progressBar.transform.localPosition -= Vector3.down * height;

        ReloadType = ReloadType.NORMAL;
        
        _reloadTween = LeanTween.moveLocalY(progressBar.gameObject, 0, Current.reloadTime).setOnComplete(() => {
            _endReload();
        });
    }

    private void _endReload() {
        _hide();
        Reloaded.Invoke(ReloadType);
        ReloadType = ReloadType.NONE;
        
        mainBar.color = BgColor;
        progressBar.color = FgColor;
    }
}
