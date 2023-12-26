using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoss : MonoBehaviour
{
    public GameObject Boss;
    private Animator anim;
    private int start = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isParentGrounded = Boss.GetComponent<BossMove>().isGrounded;
        if (isParentGrounded == true&&start==0)
        {
            anim.SetTrigger("parentIsGrounded");
            start = 1;
        }

    }
}
