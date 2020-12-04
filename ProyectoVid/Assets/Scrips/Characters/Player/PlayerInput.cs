using System.Collections.Generic;
using UnityEngine;

public static class PlayerInput { // AKA InputController
    public static void Process(Player p) {
        PlayerPhysics.Move(p, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayerState.Attack(p);
        }
    }
}
