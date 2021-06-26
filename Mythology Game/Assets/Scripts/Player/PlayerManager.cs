using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator an;
    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        an = this.gameObject.GetComponent<Animator>();
    }
    //delete this update method when done testing
    
    private void FixedUpdate() { 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float speed = GameManager.instance.speed; //3 is default speed
        if(x != 0 || y != 0) {
            an.SetBool("moving", true);
            an.SetFloat("X", x);
            an.SetFloat("Y", y);
        } else {
            an.SetBool("moving", false);
        }
        
        rb.velocity = new Vector2(x * speed, y * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ankh")) {
            //INCEASE MAX HEALTH
            GameManager.instance.IncreaseMaxHealth(1);
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Djed")) {
            //INCREASE HEALTH? MAYBE
            GameManager.instance.HealPlayer(2);
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Was")) {
            //INCREASE DAMAGE
            GameManager.instance.damage += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Jar")) {
            GameManager.instance.IncreaseMaxHealth(1);
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Black Lotus")) {
            GameManager.instance.attackSpeed += 1;
            GameManager.instance.boltColor = new Color32(0, 0, 0, 255);
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Crook")) {
            GameManager.instance.damage += 1;
            GameManager.instance.speed -= 1;
            if (ItemDatabase.instance.CheckIfUsed("Flail")) {
                //you have the flail as well
                GameManager.instance.boltColor = new Color32(255, 0, 0, 255);
                GameManager.instance.IncreaseMaxHealth(1);
            }
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Flail")) {
            GameManager.instance.damage += 1;
            GameManager.instance.speed -= 1;
            if (ItemDatabase.instance.CheckIfUsed("Crook")) {
                //you have the flail as well
                GameManager.instance.boltColor = new Color32(255, 0, 0, 255);
                GameManager.instance.IncreaseMaxHealth(1);
            }
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Papyrus")) {
            GameManager.instance.boltSize += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Mummy Hand")) {
            GameManager.instance.attackSpeed += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Blue Lotus")) {
            GameManager.instance.boltSize += 1;
            GameManager.instance.boltColor = new Color32(0, 150, 200, 255);
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Feather")) {
            GameManager.instance.speed += 1;
            if (ItemDatabase.instance.CheckIfUsed("Heart")) {
                GameManager.instance.health = GameManager.instance.maxHealth;
            }
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Heart")) {
            GameManager.instance.IncreaseMaxHealth(1);
            if (ItemDatabase.instance.CheckIfUsed("Feather")) {
                GameManager.instance.health = GameManager.instance.maxHealth;
            }
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Scribe")) {
            GameManager.instance.boltSize += 1;
            GameManager.instance.attackSpeed -= 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Eye")) {
            GameManager.instance.damage += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Cat Fangs")) {

        } else if (collision.gameObject.CompareTag("Sun Disk")) {
            GameManager.instance.damage += 1;
            GameManager.instance.maxHealth -= 1;
            GameManager.instance.ChangeHealthBarUI();
            if(GameManager.instance.health > GameManager.instance.maxHealth) {
                GameManager.instance.health = GameManager.instance.maxHealth;
            }
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Book")) {
            GameManager.instance.attackSpeed += 1;
            GameManager.instance.maxHealth -= 1;
            GameManager.instance.ChangeHealthBarUI();
            if (GameManager.instance.health > GameManager.instance.maxHealth) {
                GameManager.instance.health = GameManager.instance.maxHealth;
            }
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Lapis")) {
            GameManager.instance.speed += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Turquoise")) {
            GameManager.instance.boltSize += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Mallet")) {
            GameManager.instance.damage += 1;
            GameManager.instance.attackSpeed -= 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Cow Horns")) {
            GameManager.instance.damage -= 1;
            GameManager.instance.attackSpeed += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Beer")) {
            GameManager.instance.speed += 1;
            GameManager.instance.AddItemToUI(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        }
        GameManager.instance.CheckStats();
        GameManager.instance.ChangeHealthBarUI();
        GameManager.instance.SettingsStats();
       // GameManager.instance.PrintStats();
    }
}
