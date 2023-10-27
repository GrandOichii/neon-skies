using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    #region Serialized
    
    public Rigidbody2D body;
    public GameObject sprite;

    #endregion

    private Vector2 _moveDir;


    void Start()
    {
        
    }

    void Update() {
        ProcessInputs(); 
    }

    void FixedUpdate()
    {
        Move();

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        sprite.transform.up = direction;    
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDir = new Vector2(moveX, moveY).normalized;

        
    }

    void Move() {
        body.velocity = _moveDir * new Vector2(moveSpeed, moveSpeed);
    }
}
