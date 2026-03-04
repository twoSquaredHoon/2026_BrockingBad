using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team1 : Team
{
    protected override void Start()
    {
        base.Start();
        attackType = "Melee";
        hp = 100f;
        attackDamage = 20f;
        moveSpeed = 1f;
        attackSpeed = 3f;
        attackRange = 0.5f;
        score = 3f;
        direction = Vector3.right;
    }

    protected override void PlayAttackAnimation()
    {
        animator?.SetBool("isAttacking", true);
    }

    protected override void PlayMoveAnimation()
    {
        animator?.SetBool("isAttacking", false);
    }
}