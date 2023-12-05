using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int currentLevel = 1;

    public TMP_Text playerHP;
    public TMP_Text playerScore;
    public TMP_Text level;

    //Need this variable made
    //public TMP_Text level;

    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        playerHP.text = "HP: " + playerMovement.lives.ToString();
        playerScore.text = "Score: " + playerMovement.score.ToString();
        level.text = "Level: " + currentLevel.ToString();

    }
}
