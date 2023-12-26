using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3WaitCutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public GameObject Slider;
    public GameObject bossCutScene;

    public float timeToWait;
    public float timeToWait2;

    void Start()
    {
        player.GetComponent<MOVE>().enabled = false;
        boss.GetComponent<Boss3Move>().canDoAction = false;
        bossCutScene.active = false;
        Slider.active = false;
        StartCoroutine(waitTimeForCutScene());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator waitTimeForCutScene()
    {
        yield return new WaitForSeconds(timeToWait);
        bossCutScene.active = true;
        yield return new WaitForSeconds(timeToWait2);
        bossCutScene.active = false;
        player.GetComponent<MOVE>().enabled = true;
        boss.GetComponent<Boss3Move>().canDoAction = true;
        Slider.active = true;

    }
}
