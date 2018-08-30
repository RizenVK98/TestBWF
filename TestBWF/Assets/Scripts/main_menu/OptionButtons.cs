using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButtons : MonoBehaviour {
    public GameObject ActiveOption;

    public void b2_menu()
    {
        ActiveOption.SetActive(false);
    }
}
