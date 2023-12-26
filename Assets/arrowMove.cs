using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMove : MonoBehaviour
{
    public GameObject player; 
    public float speed;
    public float lifeTime;
    public bool isDropingDown;
    void Start()
    {
        Invoke("destroy", lifeTime);
        if (isDropingDown == false)
        {
            Vector3 difference = player.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            Debug.Log(rotZ);
        }
    }
        
    // Update is called once per frame  
    void Update()
    {
             transform.Translate(Vector2.right * speed * Time.deltaTime);
  
    }
    void destroy()
    {
        Destroy(gameObject);
    }
}
