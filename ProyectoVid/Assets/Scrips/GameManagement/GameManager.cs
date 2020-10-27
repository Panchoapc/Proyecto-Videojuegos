using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void FixedUpdate()
    {
        lifeCkeck();
    }

    public void WinGame()
    {
        Debug.Log("YOU WON!");
        End();
    }

    void lifeCkeck()
    {
        int currentLife = FindObjectOfType<Player>().getCurrentLives();
        if (currentLife <= 0)
        {
            Debug.Log("GAME OVER!");
            SceneManager.LoadScene("EndGame Menu", LoadSceneMode.Single);
        }
    }

    void End()
    {
        SceneManager.LoadScene("WinGame Menu", LoadSceneMode.Single); ;
    }
}
