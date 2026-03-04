using System.Reflection;
using UnityEngine;

public class Brook : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;

    public float hp = 400f;

    void Start()
    {
        EntityManager.Register(this);
    }

    void Update()
    {
        if (hp <= 0)
        {
            animateAndDestroy();
        }
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0) {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));

        rb.linearVelocity = new Vector2(horizontal, 0) * speed;
    }
    void Flip() {
        facingDirection *= -1;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void TriggerSpawnAnimation()
    {
        if (anim != null)
        {
            // "doSpawn"이라는 이름의 Trigger를 발동시킵니다.
            anim.SetTrigger("doSpawn");
        }
    }

    public void getDamage(float dmg)
    {
        this.hp -= dmg;
    }

    protected virtual void animateAndDestroy()
    {
        Destroy(gameObject);
    }
}
