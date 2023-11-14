using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    //Variables
    private int MainMenu = 0;
    private int GameOver = 1;
    private int WinScreen = 2;

    //Functions

    //Go to main Game
    public void GoToGame()
    {
        Debug.Log("The gorilla of ape is yearning");
        SceneManager.LoadScene(MainMenu);
    }

    //Player loses and goes to game over screen
    public void PlayerLose()
    {
        Debug.Log("There is no crying in video games");
        SceneManager.LoadScene(GameOver);
    }

    //Player wins the game
    public void PlayerWins()
    {
        Debug.Log("Winner is You");
        SceneManager.LoadScene(WinScreen);
    }

    //Player Exits the game
    public void QuitGame()
    {
        Debug.Log("Player Quits Game");
        Application.Quit();
    }

   
}
