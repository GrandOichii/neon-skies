using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestructableWall : MonoBehaviour
{
    public void OnHealthReachedZero() {
        Destroy(gameObject);
    }
}
