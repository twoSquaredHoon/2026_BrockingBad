using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    Animator animator;
    [SerializeField] protected Projectile proj;
    protected float checkTimer;
    protected float checkSpeed;
    [SerializeField] protected bool bossTargetted = false;
    // Entity Manage
    [SerializeField] public Brook brook;
    [SerializeField] protected float distance;
    [SerializeField] protected Entity target;
    [SerializeField] protected bool canMove;
    [SerializeField] protected bool frozen;
    [SerializeField] protected float frozenTimer;
    [SerializeField] protected float attackTimer;
    protected bool isAttacking;
    [SerializeField] protected bool currentlyMatched;
    [SerializeField] protected Vector3 direction;

    // Animations
    protected Coroutine freezeCoroutine = null;
    protected Color originalColor;

    // Stats
    [SerializeField] protected float hp;
    protected float moveSpeed;
    protected float attackDamage;
    protected float attackSpeed;
    protected float attackRange;
    protected String attackType;
    protected float score;

    [Obsolete]
    protected virtual void Start()
    {
        EntityManager.Register(this);
        brook = EntityManager.getBrook();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        checkTimer = 0f;
        checkSpeed = 0.1f;

        canMove = true;
        currentlyMatched = false;
        frozen = false;
        isAttacking = false;
        attackTimer = attackSpeed - 0.05f;  
    }

    protected virtual void Update()
    {
        if (brook == null || brook is not Brook)
        {
            brook = EntityManager.getBrook();
        }

        if (hp <= 0)
        {
            animateAndDestroy();
        }

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkSpeed)
        {
            updateEnemy();
            checkTimer = 0f;
        }

        if (this is Enemy && !currentlyMatched)
        {
            float distance_brook = Math.Abs(transform.position.x - brook.transform.position.x);
            if (distance_brook <= attackRange * 1.75f)
            {
                bossTargetted = true;
                isAttacking = true;
            }  else
            {
                bossTargetted = false;
                isAttacking = false;
            } 
        }
        
        if (!frozen && !isAttacking)
        {
            moveEntity();
        } 
        else if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attack();
                attackTimer = 0f;
            }
            if (target == null)
            {
                currentlyMatched = false;
                isAttacking = false;
            }
        }
    }

    protected virtual void updateEnemy()
    {
        if (!currentlyMatched)
        {
            target = EntityManager.getTarget(this);
            if (target != null)
            {
                distance = Math.Abs(transform.position.x - target.transform.position.x);
                if (distance <= attackRange * 1.75f)
                {
                    matched(this, target);
                } 
            } 
            else
            {
                canMove = true;
            }
        } else
        {
            distance = Math.Abs(transform.position.x - target.transform.position.x);
            if (target.getTarget() != null && !target.getTarget().Equals(this))
            {
                setCurrentlyMatched(false);
            }
            else if (distance <= attackRange)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
                canMove = !frozen;
            } 
        }
    }

    protected virtual void moveEntity()
    {
        if (target != null)
        {
            if (target.transform.position.x <= transform.position.x)
            {
                direction = Vector3.left;
            } 
            else
            {
                direction = Vector3.right;
            }
            
        } else
        {
            if (this is Enemy)
            {
                direction = getBrockDirection(this);
            } else if (this is Team)
            {
                direction = Vector3.right;
            }
        }
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    protected virtual Vector3 getBrockDirection(Entity e)
    {
        if (e.transform.position.x <= brook.transform.position.x)
        {
            return Vector3.right;
        } else
        {
            return Vector3.left;
        }
    }

    protected virtual void matched(Entity teammate, Entity opponent)
    {
        teammate.setCurrentlyMatched(true);
        opponent.setCurrentlyMatched(true);
        
        teammate.target = opponent;
        opponent.target = teammate;
    }

    protected virtual void setCurrentlyMatched(bool val)
    {
        currentlyMatched = val;
    }

    public virtual bool getCurrentlyMatched()
    {
        return currentlyMatched;
    }

    public virtual Entity getTarget()
    {
        return target;
    }

    public virtual void setTarget(Entity target)
    {
        this.target = target;
    }

    protected virtual void attack()
    {
        if (bossTargetted)
        {
            if (this.attackType.Equals("Melee"))
            {
                if (brook != null)
                {
                    brook.getDamage(attackDamage);
                }
                if (brook == null)
                {
                    canMove = true;
                }
            } else if (this.attackType.Equals("Ranged"))
            {
                Projectile projShot = Instantiate(proj, transform.position, Quaternion.identity);
                projShot.Init(attackDamage, moveSpeed * 1.2f, brook, direction);
            } else
            {
                Debug.Log("Wrong Attack Type");
            }
        } else
        {
            if (this.attackType.Equals("Melee"))
            {
                if (target != null)
                {
                    target.getDamage(attackDamage);
                }
                if (target == null)
                {
                    canMove = true;
                }
            } else if (this.attackType.Equals("Ranged"))
            {
                Projectile projShot = Instantiate(proj, transform.position, Quaternion.identity);
                projShot.Init(attackDamage, moveSpeed * 1.2f, target, direction);
            } else
            {
                Debug.Log("Wrong Attack Type");
            }
        }
        
    }

    public virtual void getDamage(float dmg)
    {
        hp -= dmg;
    }

    protected virtual void animateAndDestroy()
    {
        if (target != null) 
        {
            target.setTarget(null);
            target.setCurrentlyMatched(false);
            EntityManager.Register(target);
        }
        EntityManager.Unregister(this);
        EntityManager.addDeadListEnemy(this);
        Destroy(gameObject);
    }

    public virtual void freeze(float num)
    {
        if (originalColor == default(Color))
            originalColor = spriteRenderer.color;

        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            UnfreezeState();
        }

        freezeCoroutine = StartCoroutine(freezeHelp(num));
    }

    private IEnumerator freezeHelp(float num)
    {
        ApplyFreezeState();

        yield return new WaitForSeconds(num);

        UnfreezeState();

        freezeCoroutine = null;
    }

    private void ApplyFreezeState()
    {
        frozen = true;

        if (animator != null)
            animator.speed = 0f;

        spriteRenderer.color = new Color(0f, 0.2f, 0.7f, 1f);
    }

    private void UnfreezeState()
    {
        frozen = false;

        if (animator != null)
            animator.speed = 1f;

        spriteRenderer.color = originalColor;
    }

    public virtual void knockback(float knockbackDist, float freezeTime)
    {
        StopCoroutine("KnockbackCoroutine");  // avoid duplicate knockbacks
        StartCoroutine(KnockbackCoroutine(knockbackDist, freezeTime));
    }

    private IEnumerator KnockbackCoroutine(float knockbackDist, float freezeTime)
    {
        bool knockLeft = (this is Team);
        float knocked = 0f;
        float knockSpeed = 5f;

        canMove = false;

        while (knocked < knockbackDist)
        {
            knockSpeed += 0.3f;
            float move = knockSpeed * Time.deltaTime;
            if (knockLeft)
            {
                transform.position += Vector3.left * move;
            } 
            else
            {
                transform.position += Vector3.right * move;
            }
            
            knocked += move;

            yield return null; // wait for next frame
        }

        canMove = true;
        freeze(freezeTime);
    }
}
