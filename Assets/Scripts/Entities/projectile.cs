using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    protected float damage;
    protected float moveSpeed;
    protected float terminatePosition;
    protected float projectileRange;
    protected Entity target;

    public void Init(int dmg, float speed, Entity target) 
    {
        this.damage = dmg;
        this.moveSpeed = speed;
        this.projectileRange = 4f;
        this.terminatePosition = transform.position.x + projectileRange;
        this.target = target;
    }
    void Update()
    {
        if (transform.position.x > terminatePosition)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        bool isOpponent = other.CompareTag("Enemy");
        if (isOpponent)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.getDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
