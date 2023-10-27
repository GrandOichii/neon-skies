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

    #endregion

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {

        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Fire();
        }
    }

    void Fire() {
        Instantiate(bulletTemplate, muzzlePoint.transform.position, sprite.transform.rotation);
    }
}
