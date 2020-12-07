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
            case "door":
                //ACA PAUSAR 1.5 SEGUNDOS
                GameManager.NextScene();
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
