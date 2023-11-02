using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun")]
public class Gun : ScriptableObject
{
    #region Serialized

    public new string name;
    [TextArea]
    public string description;
    public int magazineSize;
    public GameObject bulletTemplate;
    public float maxDeviation;
    public float deviationDecreaseInterval;
    public float deviationPerBullet;
    public FireMode fireMode;
    public float fireInterval;

    [Header("Reloading")]
    public float ejectTime;
    public float reloadTime;
    public float hotReloadIntervalStart;
    public float hotReloadIntervalEnd;

    [Header("Pump-action")]
    public int minBulletsPerShot = 1;
    public int maxBulletsPerShot = 1;
    public float perShotDeviation = 0f;

    #endregion
}
