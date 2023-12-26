using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public float KnockBackForce;
    public bool hasAttacked = false;

     void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Player"))
        {
            other.transform.localPosition = new Vector3(other.transform.localPosition.x + KnockBackForce, other.transform.localPosition.y, other.transform.localPosition.z);
            if (hasAttacked == false) {
                hasAttacked = true;
                other.gameObject.GetComponent<Health>().numOfHeart--;
                }
        }
    }
}
