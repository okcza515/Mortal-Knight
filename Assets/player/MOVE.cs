using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float wallSlidingSpeed;
    private Animator anim;

    //check สถานะ
    private bool isAttacking = false;
    private bool isAttacking2 = false;

    //เช็คพื้น
    public Transform feetPos;
    public LayerMask whatIsGround;
    public float feetRadius;

    //เช็คกำแพง
    public Transform frontPos;
    public float frontRadius;
    public LayerMask whatIsWall;
    public bool hasSlided = false;

    //ดาเมจ
    public int damage;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;

    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool Stunned = gameObject.GetComponent<Health>().Stunned;
        bool isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, whatIsGround);
        bool isSliding =  Physics2D.OverlapCircle(frontPos.position, frontRadius, whatIsWall);
        if (isGrounded == false) {
            anim.SetBool("isGrounded", false);
        }
        else
        {
            anim.SetBool("wallJump", false);
             anim.SetBool("isGrounded", true);
            anim.ResetTrigger("Sliding");
            if (hasSlided == true)
            {
                hasSlided = false;
            }
            if (isAttacking2 == true)
            {
                isAttacking2 = false;
            }
        }

        if (isSliding == true && isGrounded == false && Stunned == false && hasSlided == false)
        {
            anim.SetTrigger("Sliding");
            anim.SetBool("isGrounded", false);
            anim.SetBool("isSliding", true);
            hasSlided = true;
            anim.SetBool("wallJump", false);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else if (Input.GetKey(KeyCode.W) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && isGrounded == false && isAttacking == false && Stunned == false && isSliding == true)
        {
            audioSource.Play();
            if (Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            anim.SetBool("wallJump", true);
            anim.SetBool("isGrounded", false);
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 1 * jumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.W) && isGrounded == true && isAttacking == false && Stunned == false)
        {
            audioSource.Play();
            anim.SetBool("isGrounded", false);
            anim.SetTrigger("Jump");
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (Input.GetKeyDown(KeyCode.J) && isAttacking2 == false && Stunned == false && isGrounded == false && isSliding == false)
        {
            audioSource.Play();
            Collider2D[] enimiesToDamge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enimiesToDamge.Length; i++)
            {
                enimiesToDamge[i].GetComponent<BossHealth>().TakeDamage(damage);
            }
            anim.SetTrigger("jumpAttack");
            isAttacking2 = true;
        }
        else if (Input.GetKey(KeyCode.A) && isAttacking == false && Stunned == false)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

            if (isGrounded == true || (isGrounded == false && isSliding == false))
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);

            }
            if (isGrounded == true)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

        }
        else if (Input.GetKey(KeyCode.D) && isAttacking == false && Stunned == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

            if (isGrounded == true || (isGrounded == false && isSliding == false))
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            }
            if (isGrounded == true)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

        }
        else if (Input.GetKeyDown(KeyCode.J) && isAttacking == false && Stunned == false&&isGrounded==true)
        {
            audioSource.Play();
            rb.velocity = new Vector2(0 * speed, rb.velocity.y);
            anim.SetBool("isRunning", false);
            anim.SetTrigger("attack");
            Collider2D[] enimiesToDamge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for(int i = 0; i < enimiesToDamge.Length; i++)
            {
                enimiesToDamge[i].GetComponent<BossHealth>().TakeDamage(damage);
            }
            StartCoroutine(Attacking());

        }

        else
        {
            rb.velocity = new Vector2(0 * speed, rb.velocity.y);
            anim.SetBool("isRunning", false);
        }
         if (isSliding == false && isGrounded == false && hasSlided == true)
        {
            anim.SetBool("isGrounded", false);
            hasSlided = false;
            anim.SetBool("isSliding", false);
        }

    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    } 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, feetRadius);
        Gizmos.DrawWireSphere(frontPos.position, frontRadius);
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

    }

}


