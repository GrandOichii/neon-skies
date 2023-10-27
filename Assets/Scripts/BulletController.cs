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

    void DestroySelf() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.MovePosition(transform.position + speed * Time.fixedDeltaTime * transform.up);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("wall")) {
            Destroy(gameObject);
            return;
        }
        if (collider.gameObject.CompareTag("damageable")) {
            Destroy(gameObject);
            collider.GetComponent<Health>().Value -= damage;
            return;
        }
    }
}
