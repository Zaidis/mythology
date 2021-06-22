using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed; //movement speed
    public int maxHealth = 10;

    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start() {
        health = maxHealth;
    }
    //delete this update method when done testing
    public void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            Damage(1);
        } else if (Input.GetKeyDown(KeyCode.Y)) {
            Heal(1);
        }
    }
    private void FixedUpdate() { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x * speed, y * speed);
    }

    public int health { get; set; }
    public void Damage(int amount) {
        health -= amount;
        if(health <= 0) {
            //you died
            print("You have died.");
        }
        GameManager.instance.ChangeHealthBarUI();
    }
    public void Heal(int amount) {
        health += amount;
        if(health > maxHealth) {
            health = maxHealth;
        }
        GameManager.instance.ChangeHealthBarUI();
    }
    public void IncreaseMaxHealth(int amount) {
        maxHealth += amount;
        GameManager.instance.ChangeHealthBarUI();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ankh")) {
            IncreaseMaxHealth(1);
            Destroy(collision.gameObject);
        }
    }
}
