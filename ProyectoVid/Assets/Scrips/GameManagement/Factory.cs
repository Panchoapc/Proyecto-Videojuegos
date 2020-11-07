using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    [SerializeField] private GameObject rayGunShotPrefab; // tipo `LightRay`, rayo láser del arma de rayos

    /// <summary>
    /// Instancia un disparo de la pistola de rayos.
    /// </summary>
    public void SpawnRayGunShot() {
        Instantiate(this.rayGunShotPrefab);
    }
}
