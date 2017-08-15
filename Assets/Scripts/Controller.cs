﻿using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour {

	Rigidbody rBody;

	float forwardInput, turnInput;

	Quaternion targetRotation;
	public Quaternion TargetRotation {
		get { return targetRotation; }
	}


	public float inputDelay = 0.1f;
	public float forwardVel = 20f;
	public float rotateVel = 100f;



	void Start () {
		// locate player
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody> ()) {
			rBody = GetComponent<Rigidbody> ();
		} else {
			Debug.LogError ("No rigidbody");
		}
		forwardInput = turnInput = 0;
	}

	void GetInput() {
		forwardInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}

		

	void Update () {
		GetInput ();
		Turn ();

	}

	void FixedUpdate() {

		LockYAxis ();
		Move ();
	}



	void Move() {
		if (Mathf.Abs (forwardInput) > inputDelay) {

			//move
			rBody.velocity = transform.forward * forwardInput * forwardVel;
		} else {
			//stop
			rBody.velocity = Vector3.zero;

		}
	}

    void Turn() {
        if (rBody.velocity != Vector3.zero)
        { 
            if (Mathf.Abs(turnInput) > inputDelay) {
                targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
            }
        transform.rotation = targetRotation;
        }
	}

	void LockYAxis () // Make sure the karts don't fall out of rotation
	{
		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;
		Quaternion rot = transform.rotation;
		rot.y = 0;
		transform.rotation = rot;
	}

}

