using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int numOfHeart;
    public int checkHeart;
    public Image[] heart;


    public bool Stunned = false;
    private Animator anim;

    private bool isAlive = true;
    public GameObject Boss;
    public GameObject GameOver;

    public AudioSource audioSource;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        checkHeart = numOfHeart;
    }
    void Update()
    {
        for(int i = 0; i < heart.Length; i++)
        {
            if (i < numOfHeart)
            {
                heart[i].enabled = true;
            }
            else
            {
                heart[i].enabled = false;
            }
        }
        if (checkHeart > numOfHeart &&isAlive==true)
        {
            checkHeart = numOfHeart;
            StartCoroutine(Stunning());
        }
        if (numOfHeart <= 0&&isAlive==true)
        {
            Stunned = true;
            isAlive = false;
            StartCoroutine(Dying());
        }
    }
    IEnumerator Stunning()
    {
        audioSource.Play();
        anim.SetTrigger("Stunned");
        Stunned = true;
        yield return new WaitForSeconds(0.25f);
        if (isAlive == true)
        {
            Stunned = false;
        }
    }
    IEnumerator Dying()
    {
        anim.SetBool("Die", true);
        anim.SetTrigger("DieTrigger");
        GameOver.active = true;
        if (menuScreen.BossClear == 1)
        {
            Boss.GetComponent<BossMove>().canDoAction = false;
        }
        else if(menuScreen.BossClear == 2)
        {
            Boss.GetComponent<Boss2Move>().canDoAction = false;
        }
        else if(menuScreen.BossClear == 3)
        {
            Boss.GetComponent<Boss3Move>().canDoAction = false;
        }
        else if(menuScreen.BossClear == 4)
        {
          //  Boss.GetComponent<Boss4Move>().canDoAction = false;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
     }
}
