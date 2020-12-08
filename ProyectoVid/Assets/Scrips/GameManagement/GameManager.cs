using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager {
    /**
     * Se tiene un sistema de pausa. Cuando `isPaused` es cierto, todo movimiento se detiene
     * (no ocurren los `Update` de los personajes y ataques).
     */
    public static bool isGamePaused { get; private set; } = false;
    public static void PauseGame() => GameManager.isGamePaused = true;
    public static void ResumeGame() => GameManager.isGamePaused = false;

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
