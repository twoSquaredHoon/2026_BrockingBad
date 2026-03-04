using System;
using System.Collections;
using System.Data;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EntityManager : MonoBehaviour
{
    public static List<Enemy> enemies = new List<Enemy>();
    public static List<Team> teams = new List<Team>();
    public static List<string> deadEnemyList = new List<string>();
    public static List<string> deadTeamList = new List<string>();


    [SerializeField] private List<Enemy> debugEnemies;
    [SerializeField] private List<Team> debugTeams;
    [SerializeField] private List<string> debugDeadEnemies;
    [SerializeField] private List<string> debugDeadTeams;

    private void Update()
    {
        debugEnemies = new List<Enemy>(enemies);
        debugTeams = new List<Team>(teams);
        debugDeadEnemies = new List<string>(deadEnemyList);
        debugDeadTeams = new List<string>(deadTeamList);

        enemies.RemoveAll(e => e == null);
        teams.RemoveAll(t => t == null);
    }

    public static void Register(Entity e)
    {
        if (e is Enemy)
        {
            enemies.Add(e as Enemy);
        } else if (e is Team)
        {
            teams.Add(e as Team);
        }
        
    }
    public static void Register(Enemy e)
    {
        enemies.Add(e);
    }
    public static void Register(Team t)
    {
        teams.Add(t);
    }

    public static void Unregister(Entity e)
    {
        if (e is Enemy)
        {
            enemies.Remove(e as Enemy);
        } else if (e is Team)
        {
            teams.Remove(e as Team);
        }
        
    }
    public static void Unregister(Enemy e)
    {
        enemies.Remove(e);
    }
    public static void Unregister(Team t)
    {
        teams.Remove(t);
    }

    public static void addDeadListEnemy(Entity e)
    {
        if (e is Enemy)
        {
            deadEnemyList.Add(e.name);
        } 
        else if (e is Team)
        {
            deadTeamList.Add(e.name);
        }
        
    }

    public static List<Enemy> getEnemyList()
    {
        List<Enemy> copy = new List<Enemy>(enemies);
        return copy;
    }

    public static List<Team> getTeamList()
    {
        List<Team> copy = new List<Team>(teams);
        return copy;
    }

    public static List<string> getDeadEnemyList()
    {
        List<string> copy = new List<string>(deadEnemyList);
        return copy;
    }

    public static List<string> getDeadTeamList()
    {
        List<string> copy = new List<string>(deadTeamList);
        return copy;
    }

    public static Entity getTarget(Entity e)
    {
        if (e is Enemy)
        {
            return getTarget(e as Enemy);
        } else if (e is Team)
        {
            return getTarget(e as Team);
        }
        return null;
    }

    public static Entity getTarget(Enemy e)
    {
        if (teams.Count == 0 || e == null) return null;

        Team closest = null;
        float minDistance = float.MaxValue;
        Vector3 currentPos = e.transform.position;

        foreach (Team t in teams)
        {
            if (t == null || t.getCurrentlyMatched()) continue;

            float distance = Math.Abs(currentPos.x - t.transform.position.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = t;
            }
        }

        return closest as Entity; 
    }

    public static Entity getTarget(Team t)
    {
        if (enemies.Count == 0 || t == null) return null;

        Enemy closest = null;
        float minDistance = float.MaxValue;
        Vector3 currentPos = t.transform.position;

        foreach (Enemy e in enemies)
        {
            if (e == null || e.getCurrentlyMatched()) continue;

            float distance = Math.Abs(currentPos.x - e.transform.position.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = e;
            }
        }

        return closest as Entity;
    }

}