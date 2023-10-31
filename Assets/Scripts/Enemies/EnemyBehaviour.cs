using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyBehaviour : MonoBehaviour
{
    protected EnemyController _controller;
    protected bool _active;

    void Awake() {
        _controller = GetComponent<EnemyController>();
    }

    public virtual void EBStart() {
        _active = true;
    }
    

    public virtual void EBUpdate() {

    }

    public virtual void EBEnd() {
        _active = false;
    }
}
