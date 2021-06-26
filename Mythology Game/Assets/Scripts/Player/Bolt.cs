using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private PlayerManager player;
    private Rigidbody2D rb;
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;

    float x;
    float y;
    private void Awake() {
        player = GameManager.instance.player;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.GetComponent<SpriteRenderer>().color = GameManager.instance.boltColor;
        this.transform.localScale = new Vector3(GameManager.instance.boltSize, GameManager.instance.boltSize, 0);
    }

    private void Start() {
        if (player.GetComponent<Combat>().shootUp) {
            moveUp = true;
        } else if (player.GetComponent<Combat>().shootDown) {
            moveDown = true;
        } else if (player.GetComponent<Combat>().shootLeft) {
            moveLeft = true;
        } else if (player.GetComponent<Combat>().shootRight) {
            moveRight = true;
        }
        if (moveUp) {
            this.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        x = player.rb.velocity.x;
        y = player.rb.velocity.y;
    }
    private void FixedUpdate() {
        
        if (moveUp) {
            //this.transform.position += Vector3.up * 3 * Time.deltaTime;
            rb.velocity = new Vector2((x / 2), 7);
        } else if (moveDown) {
            rb.velocity = new Vector2((x / 2), -7);
        } else if (moveLeft) {
            rb.velocity = new Vector2(-7, (y / 2));
        } else if (moveRight) {
            rb.velocity = new Vector2(7, (y / 2));
        }

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Wall")) {
            Destroy(this.gameObject);
        }
    }
}
