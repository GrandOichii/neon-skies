using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFire : MonoBehaviour
{
    #region Serialized

    public GameObject muzzlePoint;
    public GameObject bulletTemplate;
    public GameObject sprite;
    public float maxDeviation;
    public float deviationDecreaseInterval;
    public float deviationPerBullet;

    #endregion

    private float _deviation;
    public float Deviation {
        get => _deviation;
        set {
            _deviation = value;
            if (_deviation < 0) _deviation = 0;
            if (_deviation > maxDeviation) _deviation = maxDeviation;
        }
    }

    void Start() {
        StartCoroutine(_decreaseDeviation());
    }

    IEnumerator _decreaseDeviation() {
        // TODO bad?
        while (true) {
            --Deviation;
            yield return new WaitForSeconds(deviationDecreaseInterval);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            Fire();
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
        }
    }

    void Fire() {
        var bullet = Instantiate(bulletTemplate, muzzlePoint.transform.position, sprite.transform.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, Random.Range(-Deviation, Deviation)));
        Deviation += deviationPerBullet;
    }
}
