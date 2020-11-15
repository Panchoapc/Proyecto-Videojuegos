using UnityEngine;

public static class PlayerState { // AKA StateController
    public static void Attack(Player p) {
        if (p.weapon == null) {
            Debug.LogFormat("[Player] Cannot attack without a weapon!");
            return;
        }
        Debug.LogFormat("[Player] Player attacked with weapon {0}", p.weapon);

        switch (p.weapon) {
            case "RayGun":
                p.raygunSound.Play();
                GameObject.FindObjectOfType<Factory>().SpawnRayGunShot();
                break;
            case "ShockSword":
                p.swordSound.Play();
                p.swordCombatHandler.SwordAttack();
                p.spriteRenderer.sprite = p.spriteSwordAttack;
                p.Invoke(nameof(p.SwordRestoreSprite), p.swordAttackSpriteDuration);
                break;
        }
    }
}
