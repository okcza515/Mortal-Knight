using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Move : MonoBehaviour
{
    public bool canDoAction;
    public GameObject heart;
    public GameObject bossLight;
    public GameObject player;
    public GameObject bossDamageHitBox;
    public GameObject firstLocation;
    public GameObject scythe;
    public float returnTime;
    private float directionToPlayer;
    private Animator anim;

    public AudioSource inviSound;
    public AudioSource liftHandSound;
    public AudioSource pullSound;
    public AudioSource throwSound;
    public AudioSource outinviSound;
    public AudioSource attackSound;

    private float rand;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(light());
        rand = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {

        if (canDoAction == true)
        {
            if (rand==0)
            {
                canDoAction = false;
                StartCoroutine(inviAttack());
            }
            else if (rand ==1)
            {
                canDoAction = false;
                StartCoroutine(throwing());
            }
            else if (rand ==2)
            {
                canDoAction = false;
                StartCoroutine(draining());
            }
        }
    }
    IEnumerator draining()
    {
        directionToPlayer = transform.position.x - player.transform.position.x;
        anim.SetTrigger("startDrain");
        if (directionToPlayer < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else if (directionToPlayer > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        yield return new WaitForSeconds(0.3f);
        liftHandSound.Play();
        anim.SetBool("draining", true);
        yield return new WaitForSeconds(1.5f);
        pullSound.Play();
        liftHandSound.Stop();
        GameObject newHeart = Instantiate(heart,new Vector2(player.transform.position.x,player.transform.position.y),Quaternion.identity);
        newHeart.active = true;
        player.GetComponent<Health>().numOfHeart -= 1;
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("draining", false);
        rand = Random.Range(0, 2);
        yield return new WaitForSeconds(1.5f);
        canDoAction = true;


    }
    IEnumerator throwing()
    {
        directionToPlayer = transform.position.x - player.transform.position.x;
        anim.SetTrigger("startThrowing");
        if (directionToPlayer < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else if (directionToPlayer > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        yield return new WaitForSeconds(0.15f);
        liftHandSound.Play();
        yield return new WaitForSeconds(1.5f);
        liftHandSound.Stop();
        throwSound.Play();
        directionToPlayer = transform.position.x - player.transform.position.x;
        if (directionToPlayer < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else if (directionToPlayer > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        anim.SetBool("throwing", true);
        GameObject newScythe = Instantiate(scythe,transform.position, transform.rotation);
        newScythe.active = true;
        yield return new WaitForSeconds(returnTime);
        anim.SetBool("throwing", false);
        transform.eulerAngles = new Vector3(0, 0, 0);
         rand = Random.Range(1, 3);
        if (rand == 1) { rand = 0; }
        yield return new WaitForSeconds(1.5f);
        canDoAction = true;
    }
    IEnumerator inviAttack()
    {
        anim.SetTrigger("invisible");
        inviSound.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        inviSound.Stop();
        outinviSound.Play();
        anim.SetTrigger("outInvi");
        transform.position = new Vector2(player.transform.position.x,player.transform.position.y);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        attackSound.Play();
        anim.SetBool("inviAttack", true);
        yield return new WaitForSeconds(0.1f);
        bossDamageHitBox.active = true;
        yield return new WaitForSeconds(0.2f);
        bossDamageHitBox.active = false;
        yield return new WaitForSeconds(0.8f);
        bossDamageHitBox.GetComponent<DamageObject>().hasAttacked = false;
        anim.SetBool("inviAttack", false);
        /////////////attack1/////////////////
        anim.SetTrigger("invisible");
        inviSound.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        inviSound.Stop();
        outinviSound.Play();
        anim.SetTrigger("outInvi");
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        attackSound.Play();
        anim.SetBool("inviAttack", true);
        yield return new WaitForSeconds(0.1f);
        bossDamageHitBox.active = true;
        yield return new WaitForSeconds(0.2f);
        bossDamageHitBox.active = false;
        yield return new WaitForSeconds(0.8f);
        bossDamageHitBox.GetComponent<DamageObject>().hasAttacked = false;
        anim.SetBool("inviAttack", false);
        ////////////attack2//////////
        anim.SetTrigger("invisible");
        inviSound.Play();
        yield return new WaitForSeconds(0.5f);
        inviSound.Stop();
        anim.SetTrigger("outInvi");
        transform.position = new Vector2(firstLocation.transform.position.x, firstLocation.transform.position.y);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("forceIdle");
         rand = Random.Range(1, 3);
        yield return new WaitForSeconds(1.5f);
        canDoAction = true;
    }
    IEnumerator light()
    {
        yield return new WaitForSeconds(1);
        bossLight.GetComponent<Animator>().SetTrigger("parentIsGrounded");
    }
}
