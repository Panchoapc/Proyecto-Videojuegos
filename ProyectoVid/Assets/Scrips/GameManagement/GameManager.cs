using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager {

    public static bool isPaused { get; private set; } = false;

    public static void PauseGame() {
        isPaused = true;
    }

    public static void ResumeGame() {
        isPaused = false;
    }

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
        GameManager.ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
