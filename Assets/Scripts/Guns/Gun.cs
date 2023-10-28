using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun")]
public class Gun : ScriptableObject
{
    public new string name;
    public int magazineSize;
    public GameObject bulletTemplate;
    public float maxDeviation;
    public float deviationDecreaseInterval;
    public float deviationPerBullet;
    public FireMode fireMode;
    public float fireInterval;

    public float ejectTime;
    public float reloadTime;
    public float hotReloadIntervalStart;
    public float hotReloadIntervalEnd;
}
