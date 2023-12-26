using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClearSet : MonoBehaviour
{
    public int map;
    // Start is called before the first frame update
    void Start()
    {
        menuScreen.BossClear = map;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
