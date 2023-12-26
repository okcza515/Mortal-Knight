using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Move : MonoBehaviour
{
    public bool canDoAction = true;
    public GameObject bossAppearLine;
    public GameObject bossInvisibleGround;
    public GameObject arrow;
    public GameObject dropingArrow;
    public GameObject sky;
    public GameObject player;
    private float directionToPlayer;
    private Animator anim;
    private Rigidbody2D rb;

    private bool isPratrolling = false;
    private bool ableToRandom =true;
    private int randomX;
    private int randomY;
    private int randomAttack;
    private bool appearJump = false;

    public AudioSource animSound;
    public AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(waitAppear());
        randomAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        directionToPlayer = transform.position.x - player.transform.position.x;
        appearJumping();
        if (canDoAction == true)
        {
            if (randomAttack == 0)
            {
                patroling();
            }
            else if(randomAttack == 1)
            {
                attack1();
            }
            else if(randomAttack == 2)
            {
                attackUp();
            }
        }

    }
    void appearJumping()
    {
        if (appearJump == true)
        {
            rb.velocity = new Vector2(0, 10);
        }
    }
    void patroling()
    {
        rb.velocity = new Vector2(8 * randomX, 5 * randomY);
        if(isPratrolling == false) {
            isPratrolling = true;
            StartCoroutine(waitThePatrolling());
            }
        if (ableToRandom == true)
        {
            StartCoroutine(waitToRandom());
        }
    }
    void attack1()
    {
        anim.SetTrigger("attack1");
        rb.velocity = new Vector2(0, 0);
        canDoAction = false;
        if (directionToPlayer < 0)
        {
             transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else if (directionToPlayer > 0  )
        {
             transform.eulerAngles = new Vector3(0, 0, 0);
        }
        StartCoroutine(shooting1());
    }
    void attackUp()
    {
        rb.velocity = new Vector2(0, 0);
        canDoAction = false;
        StartCoroutine(shootingUp());
    }

    IEnumerator shootingUp()
    {
        animSound.Play();
        anim.SetTrigger("attackUp");
        yield return new WaitForSeconds(1);
        animSound.Stop();
        shootSound.Play();
        anim.SetBool("shootUp", true);
        GameObject newDropArrow = Instantiate(dropingArrow,new Vector2(player.transform.position.x,sky.transform.position.y),dropingArrow.transform.rotation);
        newDropArrow.active = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("shootUp", false);
        yield return new WaitForSeconds(0.1f);
        animSound.Play();
        anim.SetTrigger("attackUp");
        yield return new WaitForSeconds(1);
        animSound.Stop();
        shootSound.Play();
        anim.SetBool("shootUp", true);
        newDropArrow = Instantiate(dropingArrow, new Vector2(player.transform.position.x, sky.transform.position.y), dropingArrow.transform.rotation);
        newDropArrow.active = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("shootUp", false);
        canDoAction = true;
        randomAttack = Random.Range(0, 3);
    }
    IEnumerator shooting1()
    {
        animSound.Play();
        yield return new WaitForSeconds(1);
        animSound.Stop();
        shootSound.Play();
        anim.SetBool("startFinishedLoopShoot",true);
        GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation);
        newArrow.active = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("startFinishedLoopShoot", false);
        canDoAction = true;
        randomAttack = Random.Range(0, 3);
    }

    IEnumerator waitToRandom()
    {
          ableToRandom = false;
            randomX = Random.Range(-2, 2);
            if (randomX == -2) { randomX = -1; }
            else if(randomX == 0) { randomX = 1; }
            randomY = Random.Range(-2, 2);
            if (randomY == -2) { randomY = -1; }
            else if (randomY == 0) { randomY = 1; }
        yield return new WaitForSeconds(1f);
        ableToRandom = true;
    }
    IEnumerator waitThePatrolling()
    {
         yield return new WaitForSeconds(3);
        randomAttack = Random.Range(1, 3);
        isPratrolling = false;
    }
    IEnumerator waitAppear()
    {
        yield return new WaitForSeconds(0.5f);
        bossAppearLine.GetComponent<Animator>().SetTrigger("parentIsGrounded");
        anim.SetTrigger("finishedAppear");
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("finishedJump");
        appearJump = true;
        yield return new WaitForSeconds(0.5f);
        appearJump = false;
        rb.velocity = new Vector2(0, 0);
        bossInvisibleGround.active = true;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
        }


    }
}
