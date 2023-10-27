using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    #region Serialized

    public float speed;
    public Rigidbody2D body;
    // public 

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
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
            collider.GetComponent<Health>().Value--;
            return;
        }
    }
}
