using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed; //movement speed

    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    //delete this update method when done testing
    
    private void FixedUpdate() { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(x * speed, y * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ankh")) {
            GameManager.instance.IncreaseMaxHealth(1);
            Destroy(collision.gameObject);
        }
    }
}
