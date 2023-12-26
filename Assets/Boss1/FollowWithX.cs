using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithX : MonoBehaviour
{
    public GameObject whichOneToFollow;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(whichOneToFollow.transform.position.x, transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( whichOneToFollow.transform.position.x,transform.position.y,transform.position.z);
    }
}
