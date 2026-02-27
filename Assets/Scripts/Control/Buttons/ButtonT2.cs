using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonT2 : TeamButton
{
    public Team2 type2;
    protected override void Start()
    {
        base.Start();
        k = KeyCode.Alpha2;
        energyRequired = 30f;
    }

    protected override void callSpawn()
    {
        if (!canClick) return;

        if (spawner != null)
        {
            spawner.spawn(type2);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

}
