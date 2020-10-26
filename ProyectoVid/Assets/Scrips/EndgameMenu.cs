using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // vamos a la escena anterior que corresponde a la del juego
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit(); // este comando no funciona en el editor de UNITY
    }
}
