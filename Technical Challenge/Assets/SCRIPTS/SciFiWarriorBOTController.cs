using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiWarriorBOTController : MonoBehaviour {

    static readonly float speed = 4;
    static readonly float rotSpeed = 80;
    static readonly float gravity = 8;
    float rot = 0f;
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator animator;
	int turn = 0;
	float oldRot = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			///create a hit state to move player
			///back & play the hit animation after coroutine
			animator.SetBool ("recentCollision", true);
			moveDir = transform.position = Vector3.RotateTowards(moveDir, other.gameObject.transform.position, 4, 4);
			transform.TransformDirection (moveDir);
			animator.SetBool ("recentCollision", false);
		}
	}

    void Move() {
		if (!animator.GetBool ("recentCollision")) {
			turn++;
			WalkFwd ();
			if (turn <= 190) {
				if (turn >= 100) {
					rot += 1f;
				}
			} else {
				turn = 0;
			}
		}
        Rotate();
    }

    void AllOff() {
		animator.SetBool("recentCollision", false);
    }
		

    void WalkFwd()
	{
   
        moveDir = new Vector3(0, 0, 1);
        moveDir *= speed / 2;
        moveDir = transform.TransformDirection(moveDir);
    }

    void Rotate()
    {	
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
