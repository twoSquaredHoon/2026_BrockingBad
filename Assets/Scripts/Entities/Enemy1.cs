using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : Enemy
{
    protected override void Start()
    {
        base.Start();

        attackType = "Melee";
        hp = 60f;
        attackDamage = 20f;
        moveSpeed = 1f;
        attackSpeed = 3f;
        attackRange = 0.5f;
        direction = Vector3.left;
    }
}
