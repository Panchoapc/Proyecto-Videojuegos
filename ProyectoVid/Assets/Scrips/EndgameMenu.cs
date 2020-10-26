using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("game", LoadSceneMode.Single); // vamos a la escena game
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit(); // este comando no funciona en el editor de UNITY
    }
}
