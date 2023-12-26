using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BossHealth : MonoBehaviour
{
     public int health;
    public int Boss;
    public Slider healthBar;
    private Animator anim;
    private bool isDead = false;
    public AudioSource deadSound;
    public AudioSource BGM;

    public GameObject VictoryUI;
    private bool isGetHit = false;

     void Start()
    {
        anim = gameObject.GetComponent<Animator>();
     }

    // Update is called once per frame
    void Update()
    {
 
        healthBar.value = health;
        if (health <= 0 && isDead == false) {
            BGM.Stop();

            if (Boss == 1)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                gameObject.GetComponent<BossMove>().canDoAction = false;
                gameObject.GetComponent<BossMove>().predictSpikeLine.active = false;
                gameObject.GetComponent<BossMove>().predictLine.active = false;
                gameObject.GetComponent<BossMove>().Spike[0].active = false;
                gameObject.GetComponent<BossMove>().Spike[1].active = false;
                gameObject.GetComponent<BossMove>().Spike[2].active = false;
                gameObject.GetComponent<BossMove>().StopAllCoroutines();
            }
            else if(Boss == 2)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                gameObject.GetComponent<Boss2Move>().StopAllCoroutines();
            }
            else if (Boss == 3)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                gameObject.GetComponent<Boss3Move>().StopAllCoroutines();
            }
            else if (Boss == 4)
            {
                 gameObject.GetComponent<Boss4Move>().StopAllCoroutines();
            }
            isDead = true;
            StartCoroutine(dead());
        }
    }

    IEnumerator dead()
    {
        deadSound.Play();
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.5f);
        VictoryUI.active = true;
        yield return new WaitForSeconds(3);

        if (Boss == 1)
        {
            menuScreen.BossClear = 2;
        }
        else if (Boss == 2)
        {
            menuScreen.BossClear = 3;
        }
        else if (Boss == 3)
        {
            menuScreen.BossClear = 4;
        }
        else if (Boss == 4)
        {
            menuScreen.BossClear = 5;
        }
        if (Boss != 4)
        {
            SceneManager.LoadScene("MapChoose");
        }
        else
        {
            SceneManager.LoadScene("CutSceneEnd");
        }

  

    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(getHit(damage));
    }
    IEnumerator getHit(int damage)
    {
        if (isGetHit == false)
        {
            isGetHit = true;
            yield return new WaitForSeconds(0.1f);
            health -= damage;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.01f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            isGetHit = false;
        }

    }
}
