using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
[Acuna, Joseph] [Hernandez, Max]
[12/06/23]
Controls the scenes of the game via buttons
*/

public class ButtonFunctions : MonoBehaviour
{
    /// <summary>
    /// Starts the game
    /// </summary>
   public void StartGame()
    {
        //Loads the game scene
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Goes to control Screen
    /// </summary>
    public void controls()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void CloseGame()
    {
        Debug.Log("Close Game");
        //Quits the application
        Application.Quit();
    }


}
