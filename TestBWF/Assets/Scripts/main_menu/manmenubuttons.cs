using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manmenubuttons : MonoBehaviour {

    public GameObject ActiveOption;
    public GameObject ActiveEvolution;

    public void Play()
    {
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            SceneManager.LoadScene("2DGameScene");
        }
        else
        {
            SceneManager.LoadScene("MainPlace");
        }
    }

    public void Option()
    {
        ActiveOption.SetActive(true);
    }
    public void Evolution()
    {
        ActiveEvolution.SetActive(true);

    }

        public void Quite()
    {
        Application.Quit();
    }
}
