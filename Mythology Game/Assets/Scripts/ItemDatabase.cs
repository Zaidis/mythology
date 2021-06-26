using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> usedItems = new List<GameObject>();
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Called when opening a chest or when defeating a room on the
    /// chance that an item will spawn.
    /// </summary>
    public GameObject GrabItem() {
        int rand = Random.Range(0, items.Count);
        GameObject item = items[rand];
        usedItems.Add(item);
        items.RemoveAt(rand);
        return item;
    }
    public bool CheckIfUsed(GameObject obj) {
        foreach(GameObject o in usedItems) {
            if(obj == o) {
                return true;
            }
        }
        return false;
    }
}
