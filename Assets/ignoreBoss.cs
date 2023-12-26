using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
        }


    }
}
