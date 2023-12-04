using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text playerHP;
    public TMP_Text playerScore;

    //Need this variable made
    //public TMP_Text level;

    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        playerHP.text = "HP: " + playerMovement.lives.ToString();
        playerScore.text = "Score: " + playerMovement.score.ToString();

    }
}
