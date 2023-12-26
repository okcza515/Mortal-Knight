using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartMove : MonoBehaviour
{
    public GameObject hand;
    public GameObject player;
    public float speed;
    private bool AbleToCollide=false;
    private Vector3 difference;
    private float rotZ;
    private float x;
    private float y;

    public AudioSource healSound;
      void Start()
    {
              StartCoroutine(waitBool());
             difference = hand.transform.TransformPoint(0,0,0) - transform.position;
             rotZ = Mathf.Atan2(difference.y, difference.x);
       // Debug.Log(rotZ);    
        x = Mathf.Cos(rotZ);
        y = Mathf.Sin(rotZ);
       }

    // Update is called once per frame  
    void Update()
    {
        transform.Translate(new Vector3(x*speed * Time.deltaTime,y*speed*Time.deltaTime,0));

    }
 
    IEnumerator waitBool()
    {
        yield return new WaitForSeconds(0.5f);
        AbleToCollide = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
 
        if (AbleToCollide == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                AbleToCollide = false;
                player.GetComponent<Health>().numOfHeart += 1;
                player.GetComponent<Health>().checkHeart = player.GetComponent<Health>().numOfHeart;
                Destroy(gameObject);
                healSound.Play();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            AbleToCollide = false;
            Destroy(gameObject);
        }
    }
}
