using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn {
    public GameObject enemy;
    public int chance;
}

[CreateAssetMenu(fileName = "Enemy spawn table")]
public class EnemySpawnTable : ScriptableObject
{
    public List<EnemySpawn> map;

    public GameObject Get() {
        var count = 0;
        foreach (var pair in map)
            count += pair.chance;
        var v = Random.Range(1, count + 1);
        foreach (var pair in map) {
            if (v > pair.chance) {
                v -= pair.chance;
                continue;
            }

            return pair.enemy;
        }

        // TODO shouldn't happen
        return null;
    }
}
