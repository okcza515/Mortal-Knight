using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public AudioSource dashSound;
    public AudioSource chargeSound;
    public AudioSource landingSound;
    public AudioSource startPullSound;

    private Rigidbody2D rb;
    public float speed;
    public float DashSpeed;
    public GameObject dashGhost;
    public float ghostDelay;
    private float ghostDelaySecond;


    public float jumpForce;
    public float attachRadius;
    private Animator anim;

    public Transform feetPos;
    public Transform frontPos;
    public float frontRadius;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsWall;


    public float feetRadius;
    public bool isGrounded;

    public float pullTime;
    public float chargeTime;
    public bool isDashing = false;
    public bool isSwing = false;
    private bool afterDashing = false;

    private bool isPulling = false;
    public float jumpTime;
    public float fallTime;
    public float gravityFall;
    private bool hasJump=false;

    private bool jumping = false;
    private bool isRunning = false;
    public GameObject player;
    public bool isAttachedToPlayer = false;
    public bool isAttachedToWall = false;
    public bool isFacingLeft = true;
    public GameObject sky;
    private float directionToPlayer;
 
    public GameObject[] Spike;
    public GameObject predictLine;
    public GameObject predictSpikeLine;
    public int numberOfSkill;

    public bool canDoAction = false;
    private int randomNum;
    private int randomNumForSwing;

    private bool spikeMoving = false;
    private int spikeTrigger = 0;
    public Vector3[] spikePos;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySecond = ghostDelay;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        for (int i = 0; i < Spike.Length; i++)
        {
            spikePos[i] = Spike[i].transform.position;
            Spike[i].active = false;
        }
        randomNum = Random.Range(0,numberOfSkill+1);
        randomNumForSwing = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        directionToPlayer = transform.position.x - player.transform.position.x;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, whatIsGround);
        isAttachedToPlayer = Physics2D.OverlapBox(transform.position,new Vector2 (attachRadius, 20),0f,whatIsPlayer);
        isAttachedToWall = Physics2D.OverlapCircle(frontPos.position, feetRadius, whatIsWall);

   

        if (isGrounded == true)
        {
            predictLine.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
             anim.SetBool("isGrounded", true);
            rb.gravityScale = 2;

            if (directionToPlayer < 0 && isPulling == false && isDashing == false&&isSwing==false)
            {
                isFacingLeft = false;
                transform.eulerAngles = new Vector3(0, 180, 0);

            }
            else if (directionToPlayer > 0 && isPulling == false && isDashing == false&&isSwing==false)
            {
                isFacingLeft = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (hasJump==true) {
                StartCoroutine(spikeCountDown()); //รอหนามจากพื้นหลังกระโดดให้หาย
                hasJump = false;
                randomNum = Random.Range(0,numberOfSkill+1);
                canDoAction = true;
                landingSound.Play();
            }
        }
        else
        {
             anim.SetBool("isGrounded", false);
        }

        if (canDoAction == true)
        {
            if (randomNum == 0)
            {
                canDoAction = false;
                isPulling = true;
                StartCoroutine(pullLoop());
            }
            else if (randomNum == 1)
            {
                canDoAction = false;
                anim.SetTrigger("jumpStart");
                StartCoroutine(jumpLoop());
            }
            else if (randomNum == 2)
            {// ดึงเข้าหาตัว
                canDoAction = false;
                isPulling = true;
                StartCoroutine(pullLoop2());
            }
            else if(randomNum == 3)
            {
                StartCoroutine(waitForRunning());
                randomNum = numberOfSkill + 1;
            }

            /////////////////////////////////////////////////////////////
            if(isRunning == true)
            {
                anim.SetBool("isRunning", true);
                if (isAttachedToPlayer == true)
                {
                    isRunning = false;
                    rb.velocity = new Vector2(0, 0);
                    anim.SetBool("isRunning", false);
                    if (randomNumForSwing == 1)
                    {
                        StartCoroutine(Dashing());
                    }
                    else if(randomNumForSwing == 2)
                    {
                        StartCoroutine(Swing());
                    }
                }
                else if (directionToPlayer < 0)
                {
                    rb.velocity = new Vector2(1 * speed, rb.velocity.y);
                }
                else if (directionToPlayer > 0)
                {
                    rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                }
            }
        }
        /////////////////////////////////////////////////////////
        if (isDashing == true && afterDashing == false)
        {   if(isAttachedToWall == true)
            {
                afterDashing = true;
                StartCoroutine(noDashing());
                rb.velocity = new Vector2(0, 0);
            }
            else if (isFacingLeft == false)
            {
                if (ghostDelaySecond > 0)
                {
                    ghostDelaySecond -= Time.deltaTime;
                }
                else
                {
                    GameObject currentGhost = Instantiate(dashGhost, transform.position,transform.rotation);
                    ghostDelaySecond = ghostDelay;
                }
                rb.velocity = new Vector2(1 * DashSpeed, rb.velocity.y);
            }
            else if (isFacingLeft == true)
            {
                if (ghostDelaySecond > 0)
                {
                    ghostDelaySecond -= Time.deltaTime;
                }
                else
                {
                    GameObject currentGhost = Instantiate(dashGhost, transform.position, transform.rotation);
                    ghostDelaySecond = ghostDelay;
                }
                rb.velocity = new Vector2(-1 * DashSpeed, rb.velocity.y);
            }
        }

        ///////////////////////////////////////////////////////
            if (jumping == true)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+jumpForce, gameObject.transform.position.z);
        }
        if (spikeMoving == true)
        {
            if (spikeTrigger == 1)
            {
                Spike[0].transform.position = new Vector2(Spike[0].transform.position.x + 0.4f, Spike[0].transform.position.y);
            }
            else if(spikeTrigger ==2)
            {
                Spike[2].transform.position = new Vector2(Spike[2].transform.position.x - 0.4f, Spike[0].transform.position.y);
            }
        }
    }

    IEnumerator Swing()
    {
        isSwing = true;
        canDoAction = false;
        startPullSound.Play();
        randomNum = numberOfSkill + 1;
        anim.SetTrigger("Charging");
        yield return new WaitForSeconds(chargeTime);
        landingSound.Play(); 
        anim.SetBool("BossSwing", true);
        yield return new WaitForSeconds(0.2f);
        Spike[3].active = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("BossSwing", false);
        Spike[3].active = false;
        isSwing = false;
        randomNum = Random.Range(0, numberOfSkill + 1);
        canDoAction = true;
        randomNumForSwing = Random.Range(1, 3);
    }

    IEnumerator noDashing()
    {
        yield return new WaitForSeconds(1);
        randomNum = Random.Range(0, numberOfSkill + 1);
        isDashing = false;
        afterDashing = false;
        anim.SetBool("BossDash", false);
        canDoAction = true;
    }
    IEnumerator Dashing()
    {
        startPullSound.Play();
        randomNum = numberOfSkill + 1;
        anim.SetTrigger("Charging");
        yield return new WaitForSeconds(chargeTime);
        dashSound.Play();
        anim.SetBool("BossDash", true);
        isDashing = true;
        canDoAction = false;
    }
    IEnumerator waitForRunning()
    {
        yield return new WaitForSeconds(1);
        isRunning = true;
        randomNumForSwing = Random.Range(1, 3);
    }

    IEnumerator pullLoop()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("pullStart");
        yield return new WaitForSeconds(0.1f);
        startPullSound.Play();
        predictSpikeLine.active = true;
        yield return new WaitForSeconds(pullTime);
        chargeSound.Play();
        yield return new WaitForSeconds(0.2f);
        predictSpikeLine.active = false;
        anim.SetTrigger("pullLoopEnd");
        if (directionToPlayer < 0)
        {
            spikeTrigger = 1;
            Spike[0].active = true;
            spikeMoving = true;
        }
        else
        {
            spikeTrigger = 2;
            spikeMoving = true;
            Spike[2].active = true;
        }
        yield return new WaitForSeconds(2f);
        Spike[0].transform.position = spikePos[0];
        Spike[2].transform.position = spikePos[2];
        spikeMoving = false;
        Spike[0].active = false;
        Spike[2].active = false;
        isPulling = false;
        yield return new WaitForSeconds(0.7f);//idle
        randomNum = Random.Range(0, numberOfSkill + 1);
        canDoAction = true;
    }
    IEnumerator pullLoop2()
    {
        randomNumForSwing = Random.Range(1, 3);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("pullStart");
        yield return new WaitForSeconds(0.1f);
        startPullSound.Play();
        yield return new WaitForSeconds(pullTime);
        chargeSound.Play();
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("pullLoopEnd");
        if (directionToPlayer < 0) {
            player.transform.position = new Vector2(transform.position.x +2, transform.position.y);
        }
        else
        {
            player.transform.position = new Vector2(transform.position.x -2, transform.position.y);
        }
        yield return new WaitForSeconds(1f);
        isPulling = false;
        yield return new WaitForSeconds(0.7f);//idle
        if (randomNumForSwing == 1) { StartCoroutine(Dashing()); }
        else if(randomNumForSwing == 2) { StartCoroutine(Swing()); }

    }
    IEnumerator jumpLoop()
    {
        yield return new WaitForSeconds(jumpTime);
        dashSound.Play();
        anim.SetTrigger("jumpUp");
        jumping = true;
        yield return new WaitForSeconds(fallTime);
        jumping = false;
        hasJump = true;
        gameObject.transform.position = new Vector3(player.transform.position.x, sky.transform.position.y, gameObject.transform.position.z);
        rb.velocity = new Vector3(0, 0, 0);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.01f);
        predictLine.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = gravityFall;
    }
    IEnumerator spikeCountDown() {
        Spike[1].active = true;
        yield return new WaitForSeconds(0.4f);
        Spike[1].active = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, feetRadius);
        // Gizmos.DrawCube(transform.position, new Vector2(attachRadius, 20));
        Gizmos.DrawWireSphere(frontPos.position, frontRadius);
    }
}
