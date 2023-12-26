using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public float WaitTime;
    private bool isAble;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z-10);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(WaitTime);
        anim.enabled = false;
    }
}
