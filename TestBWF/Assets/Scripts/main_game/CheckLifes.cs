using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLifes : MonoBehaviour {
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    void Start () {
        CheckLf();
    }


    public void CheckLf()
    {
        if (PlayerPrefs.GetInt("PlayerLifes") == 1)
        {
            life1.SetActive(true);
        }

        if (PlayerPrefs.GetInt("PlayerLifes") == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
        }

        if (PlayerPrefs.GetInt("PlayerLifes") == 3)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
    }
}
