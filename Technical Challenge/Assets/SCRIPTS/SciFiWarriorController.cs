using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiWarriorController : MonoBehaviour {

    static readonly float speed = 4;
    static readonly float rotSpeed = 80;
    static readonly float gravity = 8;
    float rot = 0f;
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
		AllOff ();
    }

    private void Update()
    {
        Move();
    }

    void Move() {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.O))
            {
                Run();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                WalkFwd();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                WalkBackwd();
            }
            else if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O))
            {
                ShootAuto();
            }
            else if (Input.GetKey(KeyCode.P))
            {
                ShootSingle();
            }
            else
            {
                Idle();
            }
        }
        Rotate();
    }

    void AllOff() {
        animator.SetBool("isRunning", false);
        animator.SetBool("isShootingSingle", false);
        animator.SetBool("isWalkingFwd", false);
        animator.SetBool("isShootingAuto", false);
        animator.SetBool("isWalkingBackwd", false);
    }

    void Run()
    {
        AllOff();
        animator.SetBool("isRunning", true);
        moveDir = new Vector3(0, 0, 1);
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);
    }

    void WalkFwd()
    {
        AllOff();
        animator.SetBool("isWalkingFwd", true);
        moveDir = new Vector3(0, 0, 1);
        moveDir *= speed / 2;
        moveDir = transform.TransformDirection(moveDir);
    }

    void WalkBackwd()
    {
        AllOff();
        animator.SetBool("isWalkingBackwd", true);
        moveDir = new Vector3(0, 0, -1);
        moveDir *= speed / 2;
        moveDir = transform.TransformDirection(moveDir);
    }

    void ShootAuto()
    {
        Idle();
        animator.SetBool("isShootingAuto", true);
    }

    

    void ShootSingle() {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine() {
        Idle();
        animator.SetBool("isShootingSingle", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("isShootingSingle", false);
    }

    void Idle()
    {
        AllOff();
        moveDir = Vector3.zero;
    }

    void Rotate()
    {
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
