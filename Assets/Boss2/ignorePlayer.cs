using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
        }


    }
}
