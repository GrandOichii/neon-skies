using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayController : MonoBehaviour
{
    #region Serialized

    public GameObject sectorTemplate;
    public int amount;

    #endregion

    private List<GameObject> _sectors;

    void Awake()
    {
        _sectors = new();
        for (int i = 0; i < amount; i++) {
            var child = Instantiate(sectorTemplate, transform);
            _sectors.Add(child);
        }
    }

    public void OnHealthChanged(int v) {
        if (_sectors is null) return;
        for (int i = 0; i < _sectors.Count; i++) {
            _sectors[i].SetActive(i < v);
        }
    }
}
