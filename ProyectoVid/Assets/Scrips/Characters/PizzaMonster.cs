using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMonster : Enemy {
    void Start() {
        this.moveSpeed = 3;
        this.touchAttack = 40;
        this.health = 200;
    }

    public int getAttack()
    {
        return touchAttack;
    }
}
