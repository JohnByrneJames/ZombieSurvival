using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public Transform canvas;
    public Transform Player;


    // Update is called once per frame
    private void Start()
    {
       // Scene scene = SceneManager.GetActiveScene();
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }


    }

    public void Pause()
    {

            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                Player.GetComponent<Player>().enabled = false;
            }
            else 
            {
                

                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;

                Player.GetComponent<Player>().enabled = true;
            }

    }

}