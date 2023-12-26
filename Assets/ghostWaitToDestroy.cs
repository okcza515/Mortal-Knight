using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostWaitToDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
