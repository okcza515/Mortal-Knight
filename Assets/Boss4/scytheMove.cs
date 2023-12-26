using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scytheMove : MonoBehaviour
{
     public float speed;
    public float returnTime;
    public bool isDropingDown;
    private int negative = 1;
    private float directionToPlayer;
    public GameObject player;
    void Start()
    {
        Invoke("returnHold", returnTime);
        directionToPlayer = transform.position.x - player.transform.position.x;
         if (directionToPlayer < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        else if (directionToPlayer > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);

        }
    }

    // Update is called once per frame  
    void Update()
    {
        transform.Translate(negative * Vector2.right * speed * Time.deltaTime);

    }
    void returnHold()
    {
        GetComponent<DamageObject>().hasAttacked = false;
        negative = -1;
        Invoke("destroy", returnTime);
    }
    void destroy()
    {
        Destroy(gameObject);

    }
}
