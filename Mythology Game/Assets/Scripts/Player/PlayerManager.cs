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
    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.T)) {
            Damage(1);
        } else if (Input.GetKeyDown(KeyCode.Y)) {
            Heal(1);
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x * speed, y * speed);
    }

    public int health { get; set; }
    public void Damage(int amount) {
        health -= amount;
    }
    public void Heal(int amount) {
        health += amount;
    }
}
