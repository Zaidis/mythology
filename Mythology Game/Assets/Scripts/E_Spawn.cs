using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawn : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();

    public void SpawnEnemies() {
        foreach(GameObject enemy in enemies) {
            enemy.SetActive(true);
        }
    }
}
