using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    #region Serialized

    public float speed;
    public int damage = 1;
    public Rigidbody2D body;
    public float dissapearAfter;

    // public 

    #endregion
    // Start is called before the first frame update

    void Start()
    {
        Invoke(nameof(DestroySelf), dissapearAfter);
    }

    public void SetOriginator(GameObject originator) {
        Collider2D[] playerColliders = originator.GetComponents<Collider2D>();
        var bulletCollider = GetComponent<Collider2D>();

        // Make the bullet ignore all of the player's colliders
        foreach (Collider2D c in playerColliders) {
            Physics2D.IgnoreCollision(bulletCollider, c, true);
        }
    }

    void DestroySelf() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.MovePosition(transform.position + speed * Time.fixedDeltaTime * transform.up);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // if (collider.gameObject.CompareTag("wall")) {
        //     Destroy(gameObject);
        //     return;
        // }

        print("HIT " + collider.gameObject.name);
        if (collider.TryGetComponent(out Health health)) {
            health.Value -= damage;
        }

        Destroy(gameObject);
    }
}
