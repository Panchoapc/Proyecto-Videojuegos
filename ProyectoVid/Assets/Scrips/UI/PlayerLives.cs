using UnityEngine;

public class PlayerLives : MonoBehaviour {
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    /// <summary>
    /// Muestra `livesCount` corazones que representan las vidas.
    /// </summary>
    public void DisplayLives(int livesCount) {
        switch (livesCount) {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            default:
                Debug.LogErrorFormat("[PlayerLives] Error: cannot display {0} lives", livesCount);
                break;
        }
    }
}
