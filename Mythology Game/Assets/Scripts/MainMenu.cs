using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public List<GameObject> symbols = new List<GameObject>();
    private void Start() {
        int rand = Random.Range(0, symbols.Count);
        symbols[rand].SetActive(true);
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void PlayGame() {
        SceneManager.LoadScene("Adam Scene");
    }
}
