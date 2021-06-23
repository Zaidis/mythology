using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{

    public Transform location; //the spawnpoint where the player will go

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GameObject obj = collision.gameObject;

            obj.transform.position = location.position; //move player

        }
    }

}
