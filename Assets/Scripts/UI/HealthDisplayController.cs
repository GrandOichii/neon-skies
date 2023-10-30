using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayController : MonoBehaviour
{
    #region Serialized

    public GameObject sectorTemplate;
    
    public int amount;

    #endregion

    private List<GameObject> _sectors = new();

    public void OnHealthChanged(int v) {
        print("NEW HEALTH: " + v);
        if (_sectors is null) return;
        for (int i = 0; i < _sectors.Count; i++) {
            _sectors[i].SetActive(i < v);
        }
    }

    public void OnMaxHealthChanged(int v) {
        print("NEW MAX HEALTH: " + v);
        foreach (var s in _sectors) {
            Destroy(s);
        }

        _sectors = new();
        for (int i = 0; i < v; i++) {
            var child = Instantiate(sectorTemplate, transform);
            _sectors.Add(child);
        }

    }
}
