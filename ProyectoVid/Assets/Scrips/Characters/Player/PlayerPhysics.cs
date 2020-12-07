using UnityEngine;

public static class PlayerPhysics { // AKA PhysicsController
    public static void Move(Player p, float xInput, float yInput) {
        p.FlipOnMovementX(xInput);
        p.transform.position += new Vector3(xInput, yInput, 0) * p.moveSpeed * Time.deltaTime;
    }

    public static void HandleCollision(Player p, Collision2D collision) {
        GameObject obj = collision.gameObject;
        switch (obj.tag) {
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                p.TakeDamage(enemy.touchAttack);
                enemy.OnPostTouchAttack(); // ejecutando acción del enemigo inmediatamente posterior a ataque por colisión
                break;
            case "XboxRayShot":
                p.TakeDamage(Xbox360.RAY_ATTACK);
                break;
            case "Weapon":
                p.PickUpWeapon(obj, true);
                break;
            case "door": // esperar 1.5 segundos para pasar al siguiente nivel para que se escuche completo el sonido de fin de nivel 1
                p.winSound.Play();
                GameManager.PauseGame();
                p.Invoke(nameof(p.LoadNextLevel), 1.5f);
                break;
        }
    }

    public static void HandleTrigger(Player p, Collider2D collision) {
        if (collision.gameObject.name.Contains("XboxRayShot")) {
            Debug.LogFormat("[Player] Hit by XboxRayShot!");
            p.TakeDamage(Xbox360.RAY_ATTACK);
        }
    }
}
