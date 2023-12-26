using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntBossCheck : MonoBehaviour
{
     public int numberOfBossClear;
    public GameObject ReplaceLockedButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuScreen.BossClear >= numberOfBossClear) {
            ReplaceLockedButton.active = true;  
            gameObject.active = false; }
    }
}
