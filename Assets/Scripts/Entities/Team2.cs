using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team2 : Team
{
    protected override void Start()
    {
        base.Start();

        attackType = "Ranged";
        hp = 40f;
        attackDamage = 20f;
        moveSpeed = 1f;
        attackSpeed = 2f;
        attackRange = 5f;
        score = 3f;
        direction = Vector3.right;
    }
}
