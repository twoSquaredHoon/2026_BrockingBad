using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float damage;
    protected float moveSpeed;
    protected float terminatePosition;
    protected float projectileRange;
    [SerializeField] protected Entity target;
    protected Vector3 direction;
    protected float distance;
    protected bool withinDistance;
    protected bool innerWithinDistance;

    public virtual void Init(float dmg, float speed, Entity target, Vector3 direction) 
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        this.damage = dmg;
        this.moveSpeed = speed;
        this.projectileRange = 4f;
        this.direction = direction;
        if (this.direction.Equals(Vector3.left))
        {
            this.terminatePosition = transform.position.x - projectileRange;
        } else
        {
            this.terminatePosition = transform.position.x + projectileRange;
        }
        this.target = target;
    }
    protected virtual void Update()
    {
        withinDistance = transform.position.x < terminatePosition;
        if ((direction.Equals(Vector3.left) && withinDistance) || (direction.Equals(Vector3.right) && !withinDistance))
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += direction * (moveSpeed * 3f) * Time.deltaTime;
            if (target != null)
            {
                innerWithinDistance = transform.position.x < target.transform.position.x;
                if ((direction.Equals(Vector3.left) && innerWithinDistance) || (direction.Equals(Vector3.right) && !innerWithinDistance))
                {
                    target.getDamage(damage);
                    Destroy(gameObject);
                }
            } 
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
