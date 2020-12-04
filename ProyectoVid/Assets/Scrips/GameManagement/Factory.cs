using UnityEngine;

public class Factory : MonoBehaviour {
    [SerializeField] private GameObject rayGunShotPrefab = null; // rayo láser del arma de rayos
    [SerializeField] private GameObject xboxRayShotPrefab = null; // rayo láser de la Xbox
    [SerializeField] private GameObject avocadoPrefab = null;

    /// <summary>
    /// Instancia un disparo de la pistola de rayos del jugador.
    /// </summary>
    public void SpawnRayGunShot() {
        Instantiate(this.rayGunShotPrefab);
    }

    /// <summary>
    /// Instancia un disparo del ataque de rayos de las luces rojas de la muerte de la Xbox 360.
    /// </summary>
    public void SpawnXboxRayShot() {
        Instantiate(this.xboxRayShotPrefab);
    }

    /// <summary>
    /// Instancia la palta en `position`.
    /// </summary>
    public void SpawnAvocado(Vector2 position) {
        Instantiate(this.avocadoPrefab, position, Quaternion.identity);
    }
}
