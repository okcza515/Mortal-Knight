using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = gameObject.GetComponent<BossMove>().isGrounded;
        if (isGrounded == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*fallSpeed);
        }
        else
        {
            this.enabled = false;
        }

    }
}
