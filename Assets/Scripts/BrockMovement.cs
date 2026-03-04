using System.Reflection;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0) {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
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
}
