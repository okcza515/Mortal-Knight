using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float textSpeed;

    public GameObject button;
    public GameObject[] picture;
    private int pictureCount;

    private void Start()
    {
        StartCoroutine(Type());
    }
    private void Update()
    {
        if (menuScreen.BossClear == 1)
        {
            if (textDisplay.text == sentences[index])
            {
                button.active = true;
            }
            if (pictureCount == 1)
            {
                picture[0].active = false;
                picture[1].active = true;
            }
            else if (pictureCount == 3)
            {
                picture[1].active = false;
                picture[2].active = true;
            }
            else if (pictureCount == 4)
            {
                picture[2].active = false;
                picture[3].active = true;
            }
            else if (pictureCount == 5)
            {
                picture[3].active = false;
                picture[4].active = true;
            }
            else if (pictureCount == 7)
            {
                picture[4].active = false;
                picture[5].active = true;
            }
            else if (pictureCount == 11)
            {
                picture[5].active = false;
                picture[6].active = true;
            }
            else if (pictureCount == 15)
            {
                SceneManager.LoadScene("Map0");
            }
        }
        else if(menuScreen.BossClear == 5)
        {
            if (textDisplay.text == sentences[index])
            {
                button.active = true;
            }
            if (pictureCount == 1)
            {
                picture[0].active = false;
                picture[1].active = true;
            }
            else if (pictureCount == 3)
            {
                picture[1].active = false;
                picture[2].active = true;
            }
            else if (pictureCount == 4)
            {
                picture[2].active = false;
                picture[3].active = true;
            }
            else if (pictureCount == 5)
            {
                picture[3].active = false;
                picture[4].active = true;
            }
            else if (pictureCount == 6)
            {
                picture[4].active = false;
                picture[5].active = true;
            }
            else if (pictureCount == 7)
            {
                picture[5].active = false;
                picture[6].active = true;
            }
            else if (pictureCount == 9)
            {
                SceneManager.LoadScene("MapChoose");
            }
        }
    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextSentence()
    {
        pictureCount++;
        button.active = false;
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            button.active = false;
        }
    }
}
