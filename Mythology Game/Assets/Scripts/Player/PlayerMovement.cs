using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x * speed, y * speed);
    }

}
