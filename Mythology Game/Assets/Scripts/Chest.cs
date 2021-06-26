using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool locked;
    [Header("The item that will be spawned")]
    public GameObject item;
    [Header("Sprites for Chest")]
    public Sprite openSprite;
    public Sprite closedSprite;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.CompareTag("Player")) {
            //checks to see if you have a key IF ITS LOCKED
            if (locked) {
                if(GameManager.instance.keyCount <= 0) {
                    //you have dont have a key
                    return;
                } else {
                    //you do have a key
                    GameManager.instance.keyCount--;
                    GameManager.instance.ChangeHealthBarUI();
                }
            }
            //open chest
            this.GetComponent<SpriteRenderer>().sprite = openSprite;
            SpawnItem();
            Destroy(this);
        }
    }

    private void SpawnItem() {
        GameObject item = ItemDatabase.instance.GrabItem();
        GameObject obj = Instantiate(item, this.gameObject.transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * Random.Range(300, 400));
    }
}
