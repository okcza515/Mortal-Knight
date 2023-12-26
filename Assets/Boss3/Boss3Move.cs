using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Move : MonoBehaviour
{
    public bool canDoAction;
    public GameObject bossLight;
    public GameObject ShockWave;
    public GameObject ShockWaveChild;
    public GameObject laser;
    public GameObject laserChild1;
    public GameObject laserChild2;
    public GameObject thunder;
    public GameObject thunderWarning;

    private Animator anim;
    private float pPos;
    public GameObject player;
    public AudioSource bossCharge;
     public AudioSource explodsion;
    public AudioSource beforeThunderSound;
    public AudioSource thunderSound;
    public AudioSource bossLift;

    private int rand;
    void Start()
    {
        StartCoroutine(light());
        anim = gameObject.GetComponent<Animator>();
        rand = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (canDoAction == true)
        {
            if (rand==0) {
                slam();
            }
            else if (rand==1)
            {
                StartCoroutine(shootingWall());
            }
            else if (rand==2)
            {
                StartCoroutine(thundering());
            }
        }
    }
    void slam()
    {
        canDoAction = false;
        StartCoroutine(slaming());
    }
    IEnumerator thundering()
    {
        canDoAction = false;
        anim.SetTrigger("startHold");
        beforeThunderSound.Play();
        yield return new WaitForSeconds(2f);
        //shoot1
        beforeThunderSound.Stop();
             pPos = player.transform.position.x;
        GameObject newThunderWarning = Instantiate(thunderWarning, new Vector2(pPos, thunder.transform.position.y), thunder.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        thunderSound.Play();
        Destroy(newThunderWarning);
        GameObject newThunder = Instantiate(thunder, new Vector2(pPos,thunder.transform.position.y),thunder.transform.rotation);
         yield return new WaitForSeconds(0.1f);
        Destroy(newThunder);
        //shoot2
            pPos = player.transform.position.x;
        newThunderWarning = Instantiate(thunderWarning, new Vector2(pPos, thunder.transform.position.y), thunder.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        thunderSound.Play();
        Destroy(newThunderWarning);
        newThunder = Instantiate(thunder, new Vector2(pPos, thunder.transform.position.y), thunder.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Destroy(newThunder);
        //shoot3
            pPos = player.transform.position.x;
        newThunderWarning = Instantiate(thunderWarning, new Vector2(pPos, thunder.transform.position.y), thunder.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        thunderSound.Play();
        Destroy(newThunderWarning);
        newThunder = Instantiate(thunder, new Vector2(pPos, thunder.transform.position.y), thunder.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Destroy(newThunder);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isHolding", true);
        yield return new WaitForSeconds(0.5f);
        laserChild1.GetComponent<DamageObject>().hasAttacked = false;
        laserChild2.GetComponent<DamageObject>().hasAttacked = false;
        anim.SetBool("isHolding", false);
        yield return new WaitForSeconds(0.1f);
        canDoAction = true;
        rand = Random.Range(0, 2);

    }
    IEnumerator shootingWall()
    {
        canDoAction = false;
        anim.SetTrigger("startHold");
        bossLift.Play();
        yield return new WaitForSeconds(2f);
        bossLift.Stop();
        bossCharge.Play();
        laser.active = true;
        yield return new WaitForSeconds(2f);
        bossCharge.Stop();
        anim.SetBool("isHolding", true);
        laser.active = false;
        yield return new WaitForSeconds(0.5f);
        laserChild1.GetComponent<DamageObject>().hasAttacked = false;
        laserChild2.GetComponent<DamageObject>().hasAttacked = false;
        anim.SetBool("isHolding", false);
        yield return new WaitForSeconds(0.1f);
         canDoAction = true;
        rand = Random.Range(0, 2);
        if (rand == 1)
        {
            rand = 2;
        }

    }
    IEnumerator slaming()
    {
        anim.SetTrigger("startSlam");
        bossLift.Play();
        yield return new WaitForSeconds(1f);
        bossLift.Stop();
        anim.SetBool("isSlaming", true);
        yield return new WaitForSeconds(0.5f);
        ShockWave.active = true;
        explodsion.Play();
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isSlaming", false);
        yield return new WaitForSeconds(0.1f);
        ShockWave.active = false;
        ShockWaveChild.GetComponent<DamageObject>().hasAttacked = false;
        canDoAction = true;
        rand = Random.Range(1, 3);

    }
    IEnumerator light()
    {
        yield return new WaitForSeconds(1);
        bossLight.GetComponent<Animator>().SetTrigger("parentIsGrounded");
    }
}
