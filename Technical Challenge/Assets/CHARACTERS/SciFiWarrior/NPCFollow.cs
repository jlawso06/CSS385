using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour {

    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedDistance = 5;
    public GameObject NPC;
    public float followSpeed;
    public RaycastHit Shot;
    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(ThePlayer.transform);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            if(TargetDistance >= AllowedDistance)
            {
                anim.SetInteger("condition", 1);
                followSpeed = 0.02f;
                transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, followSpeed);
            }
            else
            {
                anim.SetInteger("condition", 0);
                followSpeed = 0;
            }
        }
            
	}
}
