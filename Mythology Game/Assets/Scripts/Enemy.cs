using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health { get; set; }
    public int damage { get; set; }

    private void Start() {
        health = 4;
        damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("BOLT")) {
            health -= GameManager.instance.damage;
            Destroy(collision.gameObject);
            if(health <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
