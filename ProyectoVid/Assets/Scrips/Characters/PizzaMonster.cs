using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMonster : Enemy {
    private void Start() {
        this.moveSpeed = 4;
        this.touchAttack = 10;
        this.health = 200;
    }
}
