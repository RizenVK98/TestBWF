using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speedMove; //скорость передвижения 
    public float jumpPower; //силы прыжка 

    private float gravityForce; // гравитация персонажа 
    private Vector3 moveVector; // направление движения персонажа

    private CharacterController ch_controller;
    private Animator ch_animator;

    private MobileController mContr;
    public bool UIjump;

    // Use this for initialization
    void Start()
    { 
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
        if (other.tag == "Portal")
        {
            PlayerPrefs.SetInt("PlayerLifes", 3);
            SceneManager.LoadScene("main_game");
        }
    }
}
