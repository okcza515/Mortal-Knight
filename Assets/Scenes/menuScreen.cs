using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScreen : MonoBehaviour
{
    public static int BossClear = 0;

    public void toTheCutScene()
    {
        SceneManager.LoadScene("CutScene1");
     }
    public void toMap0()
    {
        SceneManager.LoadScene("Map0");
    }
    public void toChooseMap()
    {
        SceneManager.LoadScene("MapChoose");
    }
    public void toMap1()
    {
        SceneManager.LoadScene("Map1");
    }
    public void toMap2()
    {
        SceneManager.LoadScene("Map2");
    }
    public void toMap3()
    {
        SceneManager.LoadScene("Map3");
    }
    public void toMap4()
    {
        SceneManager.LoadScene("Map4");
    }
    public void toMainMeu1()
    {
        SceneManager.LoadScene("mainMenu");
    }
    public void IntBossPlus()
    {
        BossClear = 1;
    }

    public void Leave()
    {
        Application.Quit();
        Debug.Log("Game is exiting");

    }
}
