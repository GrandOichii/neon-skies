using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum FireMode {
    Auto,
    FullAuto,
    PumpAction
}

public class PlayerFire : MonoBehaviour
{
    #region Serialized

    public Gun startingGun;
    public int startingAmmoCount;
    public GameObject muzzlePoint;
    public GameObject sprite;
    public ReloadMeterController reloadMeter;

    #endregion

    #region Events

    public UnityEvent<Object> GunEquipped;
    public UnityEvent<int> TotalAmmoCountUpdated;
    public UnityEvent<int> MagazineAmmoCountUpdated;
    public UnityEvent GunFired;
    #endregion

    private int _totalAmmoCount;
    public int TotalAmmoCount {
        get => _totalAmmoCount;
        set {
            _totalAmmoCount = value;
            TotalAmmoCountUpdated.Invoke(_totalAmmoCount);
        }
    }

    private int _magazineAmmoCount;
    public int MagazineAmmoCount {
        get => _magazineAmmoCount;
        set {
            _magazineAmmoCount = value;
            MagazineAmmoCountUpdated.Invoke(_magazineAmmoCount);
        }
    }

    private float _deviation;
    public float Deviation {
        get => _deviation;
        set {
            _deviation = value;
            if (_deviation < 0) _deviation = 0;
            if (_deviation > Gun.maxDeviation) _deviation = Gun.maxDeviation;
        }
    }

    private Gun _gun;
    public Gun Gun 
    {
        get => _gun;
        set {
            _gun = value;
            GunEquipped.Invoke(_gun);
        }
    }

    private bool _canFire = true;

    void Start() {
        Gun = startingGun;
        
        MagazineAmmoCount = Gun.magazineSize;
        TotalAmmoCount = startingAmmoCount;

        StartCoroutine(_decreaseDeviation());
    }

    IEnumerator _decreaseDeviation() {
        // TODO bad?
        while (true) {
            --Deviation;
            yield return new WaitForSeconds(Gun.deviationDecreaseInterval);
        }
    }

    void Update()
    {
        // LeanTween.shake.
        switch (Gun.fireMode) {
        case FireMode.FullAuto:
            if (Input.GetKey(KeyCode.Mouse0)) {
                Fire();
            }
            break;
        case FireMode.Auto:
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Fire();
            }
            break;
        case FireMode.PumpAction:
            // TODO
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Fire();
            }
            break;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            _advanceReload();
        }
    }

    void Fire() {
        if (!_canFire) return;

        if (MagazineAmmoCount == 0) {
            return;
        }

        _canFire = false;
        var bullet = Instantiate(Gun.bulletTemplate, muzzlePoint.transform.position, sprite.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, Random.Range(-Deviation, Deviation)));
        Deviation += Gun.deviationPerBullet;
        --MagazineAmmoCount;

        GunFired?.Invoke();

        StartCoroutine(_enableFire());
    }

    IEnumerator _enableFire() {
        yield return new WaitForSeconds(Gun.fireInterval);
        _canFire = true;
    }

    private bool _loaded = true;
    private bool _canAdvanceReload = true;
    void _advanceReload() {
        if (!_canAdvanceReload) return;

        if (_loaded) {
            _canAdvanceReload = false;
            MagazineAmmoCount = 0;
            reloadMeter.Eject();
            return;
        }

        if (TotalAmmoCount <= 0) {
            return;
        }

        // loaded
        reloadMeter.Reload();
    }

    public void OnEjected() {
        _canAdvanceReload = true;
        _loaded = false;
    }

    public void OnReloaded(ReloadType rt) {
        _loaded = true;
        MagazineAmmoCount = System.Math.Min(Gun.magazineSize, TotalAmmoCount);
        TotalAmmoCount -= MagazineAmmoCount;
    }

}
