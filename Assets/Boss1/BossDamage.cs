using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public float KnockBackForce;

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.GetComponent<BossMove>().isGrounded == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.localPosition = new Vector3(other.transform.localPosition.x + KnockBackForce, other.transform.localPosition.y, other.transform.localPosition.z);
                other.gameObject.GetComponent<Health>().numOfHeart--;
            }
        }
        if (gameObject.GetComponent<BossMove>().isDashing == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.localPosition = new Vector3(other.transform.localPosition.x + KnockBackForce, other.transform.localPosition.y, other.transform.localPosition.z);
                other.gameObject.GetComponent<Health>().numOfHeart--;
            }
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
            Physics2D.IgnoreCollision(other.collider,gameObject.GetComponent<Collider2D>());
             }


    }
}
