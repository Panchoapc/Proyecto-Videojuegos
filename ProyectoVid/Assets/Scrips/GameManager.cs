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

    void lifeCkeck()
    {
        int currentLife = FindObjectOfType<Player>().getCurrentLives();
        if (currentLife <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
