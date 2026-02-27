using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonE1 : EnemyButton
{
    public Enemy1 enemy1;
    protected override void Start()
    {
        base.Start();
        enemy = enemy1;
    }

}
