using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasScrpt : MonoBehaviour
{
    public static canvasScrpt instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
