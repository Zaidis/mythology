using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RoomTransition : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public Transform location; //the spawnpoint where the player will go
    public Transform camLocation;
    public dtype type = dtype.up;
    private void Awake() {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GameObject obj = collision.gameObject;

            obj.transform.position = location.position; //move player
            cam.Follow = camLocation;
            //cam.LookAt = camLocation;
            if(type == dtype.up) {
                //move camera up

            } else if (type == dtype.down) {
                //move camera down

            } else if (type == dtype.left) {
                //move camera left

            } else {
                //move camera right
            }
        }
    }

}
