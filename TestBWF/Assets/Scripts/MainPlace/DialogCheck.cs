using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCheck : MonoBehaviour {

    public GameObject DialogCloud;


	void Start () {
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            DialogCloud.SetActive(true);
            PlayerPrefs.SetInt("FirstTime", 1);
            StartCoroutine(Disactive()); 
        }

    }

    IEnumerator Disactive()
    {
        yield return new WaitForSeconds(3f);
        DialogCloud.SetActive(false);
    }
	
}
