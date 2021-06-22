using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    //delete this update method when done testing
    
    private void FixedUpdate() { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float speed = 3 + GameManager.instance.speed; //3 is default speed
        rb.velocity = new Vector2(x * speed, y * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ankh")) {
            //INCEASE MAX HEALTH
            GameManager.instance.IncreaseMaxHealth(1);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Djed")) {
            //INCREASE HEALTH? MAYBE
            GameManager.instance.HealPlayer(2);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Was")) {
            //INCREASE DAMAGE
            GameManager.instance.damage += 1;
            Destroy(collision.gameObject);
        }
        GameManager.instance.ChangeHealthBarUI();
        GameManager.instance.PrintStats();
    }
}
