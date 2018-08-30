using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialog : MonoBehaviour
{
    public GameObject DialogCloud;
    public GameObject MysteryBoxGM;
    private Animator anim;


    void Start()
    {
        anim = MysteryBoxGM.transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(Dialog());
    }

    IEnumerator Dialog()
    {
        yield return new WaitForSeconds(3f);

        DialogCloud.SetActive(false);
        StartCoroutine(MysteryBox());
    }

    IEnumerator MysteryBox()
    {
        yield return new WaitForSeconds(5f);

        MysteryBoxGM.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MysteryBox")
        {
            anim.enabled = true;
        }

        if (collision.gameObject.tag == "3D")
        {
            SceneManager.LoadScene("MainPlace");
        }
    }
}
