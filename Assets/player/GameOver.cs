using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
       public void restartButton()
    {
        if (menuScreen.BossClear == 1)
        {
            SceneManager.LoadScene("Map1");
        }
        if (menuScreen.BossClear == 2)
        {
            SceneManager.LoadScene("Map2");
        }
        if(menuScreen.BossClear == 3)
        {
            SceneManager.LoadScene("Map3");
        }
        if (menuScreen.BossClear == 4)
        {
            SceneManager.LoadScene("Map4");
        }
    }
    public void exitButton()
    {
        SceneManager.LoadScene("MapChoose");
    }
}
