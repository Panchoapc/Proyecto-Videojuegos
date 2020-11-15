using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    /// <summary>
    /// GAME OVER.
    /// </summary>
    public void LooseGame() {
        SceneManager.LoadScene("LooseGameMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// El jugador gana.
    /// </summary>
    public void WinGame() {
        SceneManager.LoadScene("WinGameMenu", LoadSceneMode.Single);
    }
}
