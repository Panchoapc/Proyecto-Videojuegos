using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "Sistema de combate" con arma de larga distancia (RayGun) del jugador.
/// </summary>
public class PlayerGunCombat : MonoBehaviour {
    public static readonly int RAYGUN_SHOT_ATTACK = 30; // daño que hace cada disparo de la pistola de rayos
    public static readonly int RAYGUN_SHOT_COOLDOWN = 1; // tiempo mínimo entre ataques (segundos)

    private bool isCoolingDown = false;
    private Factory factory;

    private void Start() {
        this.factory = FindObjectOfType<Factory>();
    }

    public bool RayGunAttack() {
        if (this.isCoolingDown) return false;

        this.factory.SpawnRayGunShot();
        this.isCoolingDown = true;
        Invoke(nameof(this.ReactivateAttack), RAYGUN_SHOT_COOLDOWN);
        return true;
    }

    private void ReactivateAttack() {
        this.isCoolingDown = false;
    }
}
