using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stairs : MonoBehaviour
{
    [SerializeField]
    bool playerInAreaOfEffect = false;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    //canvasTransition transitioner;
    bool trans = false;
    float timer = 2f;
    public GameObject tooltip;
    GameObject Astar;
    private void Start()
    {
        Astar = GameObject.Find("Astar");
        Astar.SetActive(true);
        //canvas = GameObject.FindGameObjectWithTag("Canvas");
       // transitioner = canvas.GetComponentInChildren<canvasTransition>();
        tooltip.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(playerInAreaOfEffect && !trans)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
               // transitioner.gameObject.SetActive(true);
               // transitioner.fadeOut();
                trans = true;
                
            }
        }
        if(trans)
        {
           // SoundTrack.soundTrack.StopSoundtrack();
            //FXplayer.fxplayer.PlayFX(fxOptions.stairs);
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                goToNextScene();
            }
        }
    }
    private void goToNextScene()
    {
        //GameManager.instance.increaseLevel();
      //  GameManager.instance.calculateDifficulty();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInAreaOfEffect = true;
            tooltip.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            playerInAreaOfEffect = false;
            tooltip.SetActive(false);
        }
    }
}
