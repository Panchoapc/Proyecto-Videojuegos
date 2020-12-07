using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager {
    /// <summary>
    /// GAME OVER.
    /// </summary>
    public static void LooseGame() {
        SceneManager.LoadScene("LooseGameMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// El jugador gana.
    /// </summary>
    public static void WinGame() {
        SceneManager.LoadScene("WinGameMenu", LoadSceneMode.Single);
    }

    public static void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
