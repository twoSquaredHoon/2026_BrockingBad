using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    protected override void Start()
    {
        base.Start();
        // float yRandomPosition = -0.7f * (Random.value * 1 - 0.5f);
        // transform.position = new Vector3(19f, yRandomPosition, -0.13f);
        gameObject.tag = "Enemy";
    }
}
