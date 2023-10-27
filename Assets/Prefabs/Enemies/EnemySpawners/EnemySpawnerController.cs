using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    #region Serialized

    public EnemySpawnTable spawnTable;

    #endregion

    void Start()
    {
        var template = spawnTable.Get();
        Instantiate(template, transform);
    }

}
