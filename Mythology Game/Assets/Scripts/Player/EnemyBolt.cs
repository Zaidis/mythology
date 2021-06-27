using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    private Vector3 location;
    private Rigidbody2D rb;
    public float speed;
    public float boltSize;
    public float damage;
    private void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
        
    }
    private void Start() {
        var player = FindObjectOfType<PlayerManager>().gameObject;
        location = player.transform.position;
        this.transform.localScale = new Vector2(boltSize, boltSize);
    }
    private void FixedUpdate() {
        //this.transform.position += location * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Wall")) {
            Destroy(this.gameObject);
        } else if (collision.CompareTag("Player")) {
            GameManager.instance.DamagePlayer((int)damage);
            Destroy(this.gameObject);
        }
    }
}
