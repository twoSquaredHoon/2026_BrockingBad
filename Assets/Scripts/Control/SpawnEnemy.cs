using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;


public class SpawnEnemy : MonoBehaviour
{
    protected Entity entity;
    public Enemy1 enemy1;
    // public Enemy_2 enemy2;
    // public Enemy_3 enemy3;
    [SerializeField] protected float stage;
    [SerializeField] protected float stageAcc;
    [SerializeField] protected float spawnrate1;
    [SerializeField] protected float spawnrate2;
    [SerializeField] protected float spawnrate3;

    [SerializeField] protected float timer1;
    [SerializeField] protected float timer2;
    [SerializeField] protected float timer3;

    [SerializeField] protected float actualTime;
    private int spawnCount = 0;
    void Start()
    {
        transform.position = new Vector3(9.5f, -0.7f, -0.13f);
        stage = 0;
        stageAcc = 0.2f;
        spawnrate1 = 5f;
        spawnrate2 = 9f;
        spawnrate3 = 15f;
        timer1 = 0;
        timer2 = 0;
        timer3 = 0;
        actualTime = 0;

        transform.position = new Vector3(9.5f, -0.7f, -0.13f);
    }

    void Update()
    {

        if (timer1 > spawnrate1)
        {
            timer1 = 0;
            spawn(enemy1);

        }
        else
        {
            timer1 = timer1 + Time.deltaTime * stage;
        }

        // if (timer2 > spawnrate2)
        // {
        //     timer2 = 0;
        //     Instantiate(enemy2);
        // }
        // else
        // {
        //     timer2 = timer2 + Time.deltaTime * stage;
        // }

        // if (timer3 > spawnrate3)
        // {
        //     timer3 = 0;
        //     Instantiate(enemy3);
        // }
        // else
        // {
        //     timer3 = timer3 + Time.deltaTime * stage;
        // }

        stage = 1 + stageAcc * Mathf.Floor(actualTime / 30f); //30초마다 stageAcc 만큼 빨라짐
        actualTime = actualTime + Time.deltaTime;
    }

    public void spawn(Enemy enemy)
    {   
        float y_random_position = -0.7f * (Random.value * 1 - 0.5f);
        Vector3 spawnPosition = new Vector3(transform.position.x, y_random_position, transform.position.z);
        Instantiate(enemy, spawnPosition, Quaternion.identity).name += "_"+(++spawnCount);
    }
}
