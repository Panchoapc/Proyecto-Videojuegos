using UnityEngine;

public static class PlayerState { // AKA StateController
    public static void Attack(Player p) {
        if (p.weapon == null) {
            Debug.LogFormat("[PlayerState] Cannot attack without a weapon!");
            return;
        }
        Debug.LogFormat("[PlayerState] Player attacked with weapon {0}", p.weapon);

        switch (p.weapon) {
            case "RayGun":
                PlayerState.RayGunAttack(p);
                break;
            case "ShockSword":
                PlayerState.SwordAttack(p);
                break;
        }
    }

    private static void RayGunAttack(Player p) {
        if (p.gunCombatHandler.RayGunAttack()) {
            p.raygunSound.Play();
            GameObject.FindObjectOfType<Factory>().SpawnRayGunShot();
        } else {
            Debug.LogFormat("[PlayerState] Cannot attack due cooldown.");
        }
    }

    private static void SwordAttack(Player p) {
        if (p.swordCombatHandler.SwordAttack()) {
            p.swordSound.Play();
            p.animator.SetBool("SwordHit", true);
            p.spriteRenderer.sprite = p.spriteSwordAttack; // TODO: reemplazar por animación
            p.Invoke(nameof(p.SwordRestoreSprite), p.swordAttackSpriteDuration);
        } else {
            Debug.LogFormat("[PlayerState] Cannot attack due cooldown.");
        }
    }
}
