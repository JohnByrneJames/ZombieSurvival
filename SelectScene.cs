using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Dice a Unity quale scena loadare

public class SelectScene : MonoBehaviour {

    

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
