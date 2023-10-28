using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    public void OnHealthReachedZero() {
        Destroy(gameObject);
    }
}
