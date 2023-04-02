using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControll : MonoBehaviour
{
    public void playAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void exitGame()
    {
        Application.Quit();
    }
} 
