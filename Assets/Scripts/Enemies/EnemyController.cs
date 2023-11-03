using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateBehaviourMapping {
    public string parameter;
    public EnemyBehaviour behaviour;
}

public class EnemyController : MonoBehaviour
{
    #region Serialized

    public string initial;
    public List<StateBehaviourMapping> behaviourMappings;
    public LayerMask raycastIgnore;

    #endregion

    #region Events

    public UnityEvent<Vector2> lookedAt;

    #endregion

    private Dictionary<string, EnemyBehaviour> _behaviourMap;
    private string _current;
    public string Current {
        get => _current;
        set {
            _behaviourMap[Current].EBEnd();
            _current = value;
            _behaviourMap[Current].EBStart();
        }
    }

    private Dictionary<string, object> _data = new();

    public T Get<T>(string name) where T : class {
        return _data[name] as T;
    }

    public void Set<T>(string name, T value) {
        if (!_data.ContainsKey(name)) {
            _data[name] = value;
            return;
        }

        _data[name] = value;
    }

    public bool Contains(string name) => _data.ContainsKey(name);

    void Start() {
        _behaviourMap = new();
        foreach (var mapping in behaviourMappings) {
            _behaviourMap.Add(mapping.parameter, mapping.behaviour);
        }

        _current = initial;
        _behaviourMap[Current].EBStart();
    }

    void Update() {
        _behaviourMap[Current].EBUpdate();
    }

    public void LookAt(Vector3 point) {
        transform.up = point - transform.position;
    }

    public RaycastHit2D FireRay(float range) {
        var hit = Physics2D.Raycast(transform.position, transform.up, range, ~raycastIgnore);
        // Debug.DrawLine(transform.position, hit.point);
        var point = hit.point;
        if (hit.collider == null)
            point = range * transform.up;
        lookedAt.Invoke(point);
        return hit;
    }

    public void OnHealthChanged(int v) {
        // TODO display
    }

    public void OnDeath() {
        Destroy(gameObject);
    }
}
