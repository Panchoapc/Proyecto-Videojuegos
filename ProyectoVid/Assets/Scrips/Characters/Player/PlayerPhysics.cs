using UnityEngine;

public static class PlayerPhysics { // AKA PhysicsController
    public static void Move(Player p, float xInput, float yInput) {
        p.FlipOnMovementX(xInput);
        p.transform.position += new Vector3(xInput, yInput, 0) * p.moveSpeed * Time.deltaTime;
    }

    public static void HandleCollision(Player p, Collision2D collision) {
        GameObject obj = collision.gameObject;
        //Debug.LogFormat("[Player] Collided with {0} (tagged \"{1}\")", obj.name, obj.tag);
        switch (obj.tag) {
            case "Enemy":
                p.TakeDamage(collision.gameObject.GetComponent<Enemy>().touchAttack);
                break;
            case "XboxRayShot":
                p.TakeDamage(Xbox360.RAY_ATTACK);
                break;
            case "Weapon":
                p.PickUpWeapon(obj, true);
                break;
            case "door":
                GameManager.WinGame();
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
