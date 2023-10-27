using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void OnHealthChanged(int v) {
    }

    public void OnDeath() {
        Destroy(gameObject);
    }
}
