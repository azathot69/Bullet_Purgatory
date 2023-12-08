using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
   public void StartGame()
    {
        //Loads the game scene
        SceneManager.LoadScene(1);
    }

    public void controls()
    {
        SceneManager.LoadScene(2);
    }

    public void CloseGame()
    {
        Debug.Log("Close Game");
        //Quits the application
        Application.Quit();
    }


}
