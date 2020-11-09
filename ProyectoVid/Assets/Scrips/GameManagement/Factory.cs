using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    [SerializeField] private GameObject rayGunShotPrefab; // tipo `LightRay`, rayo láser del arma de rayos
    [SerializeField] private GameObject xboxRayShotPrefab; // rayo láser de la Xbox

    /// <summary>
    /// Instancia un disparo de la pistola de rayos.
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
}
