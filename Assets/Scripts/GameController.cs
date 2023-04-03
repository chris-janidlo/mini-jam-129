using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void exitGame()
    {
        Application.Quit();
    }
} 
