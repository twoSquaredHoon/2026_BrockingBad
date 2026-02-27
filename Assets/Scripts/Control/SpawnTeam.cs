using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;

public class SpawnTeam : MonoBehaviour
{
    [SerializeField] private float energy;
    private float energyRate;
    private float maxEnergy;
    private float timer;
    private int spawnCount = 0;
    

    void Start()
    {
        transform.position = new Vector3(-9.5f, -0.7f, -0.13f);
        energy = 0f;
        energyRate = 0.1f; //0.25초마다 energy가 1씩 추가
        timer = 0f;
        maxEnergy = 100f;
    }

    void Update()
    {
        if(energy < maxEnergy)
        {
            if (timer >= 1f)
            {
                energy += 1f;
                timer = 0f;
            }
            timer += Time.deltaTime * (1 / energyRate);
        }
    }

    public void spawn(Team team)
    {
        float y_random_position = -0.7f * (Random.value * 1 - 0.5f);
        Vector3 spawnPosition = new Vector3(transform.position.x, y_random_position, transform.position.z);
        Instantiate(team, spawnPosition, Quaternion.identity).name += "_"+(++spawnCount);
    }

    public float getEnergy()
    {
        return energy;
    }

    public float getMaxEnergy()
    {
        return maxEnergy;
    }

    public void reduceEnergy(float num)
    {
        energy -= num;
    }

}
