using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    public float speedMove; //скорость передвижения 
    public float jumpPower; //силы прыжка 

    private float gravityForce; // гравитация персонажа 
    private Vector3 moveVector; // направление движения персонажа

    private CharacterController ch_controller;
    private Animator ch_animator;

    private MobileController mContr;
    public bool UIjump;

    public GameObject bomb;
    public GameObject bomb1;

    private EnemyMovement em;
    private EnemyMovement em1;

    private ParticleSystem ps;
    private ParticleSystem ps1;

    public GameObject Key;

    public bool a;
    public bool b;


    // Use this for initialization
    void Start()
    {
        b = true;
        a = true;
        em = bomb.GetComponent<EnemyMovement>();
        em1 = bomb1.GetComponent<EnemyMovement>();
        ps = bomb.transform.GetChild(0).GetComponent<ParticleSystem>();
        ps1 = bomb1.transform.GetChild(0).GetComponent<ParticleSystem>();
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();

        UIjump = false;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        GamingGravity();
        LoseInFalling();
    }

    //метод перемещения персонажа
    private void CharacterMove()
    {
        //перемещение по поверхности
        moveVector = Vector3.zero;
        moveVector.z = /*Input.GetAxis("Vertical")*/mContr.Vertical() * speedMove;
        moveVector.x =/*Input.GetAxis("Horizontal")*/mContr.Horizontal() * speedMove;

        //анимация передвижения персонажа
        if ((moveVector.x != 0 || moveVector.z != 0) && ch_controller.isGrounded)
        {
            ch_animator.SetBool("run", true);
        }
        else
        {
            ch_animator.SetBool("run", false);
        }


        //поворот в сторону движения
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime); //метод передвижения по направлению
    }

    //метод гравитации
    public void GamingGravity()
    {

        if (!ch_controller.isGrounded)
        {
            gravityForce -= 7f * Time.deltaTime;
        }
        else
        {
            gravityForce = -1f;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || UIjump) && ch_controller.isGrounded)
        {
            gravityForce = jumpPower;
            ch_animator.SetBool("jump", true);
            StartCoroutine(StopJump());
        }
    }

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(1f);
        ch_animator.SetBool("jump", false);
        UIjump = false;

    }

    public void MobileJump()
    {
        UIjump = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (a)
            {
                em.enabled = false;
                em1.enabled = false;
                PlayerPrefs.SetInt("PlayerLifes", PlayerPrefs.GetInt("PlayerLifes") - 1);

                if (other.gameObject.name == "Bob-omb")
                {
                    ps.Play();
                }
                else
                {
                    ps1.Play();
                }

                StartCoroutine(Restart());
                a = false;
            }
        }

        if (other.tag == "Key")
        {
            Destroy(Key);
        }

        if (other.tag == "Portal1")
        {
            SceneManager.LoadScene("MainPlace");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.75f);

        if (PlayerPrefs.GetInt("PlayerLifes") != 0)
        {
            SceneManager.LoadScene("main_game");
        }
        else
        {
            SceneManager.LoadScene("MainPlace");
        }
    }

    public void LoseInFalling()
    {
        if (gameObject.transform.position.y <= 120f && b)
        {
            PlayerPrefs.SetInt("PlayerLifes", PlayerPrefs.GetInt("PlayerLifes") - 1);

            if (PlayerPrefs.GetInt("PlayerLifes") != 0)
            {
                SceneManager.LoadScene("main_game");
            }
            else
            {
                SceneManager.LoadScene("MainPlace");
            }

            b = false;
        }
    }
}
