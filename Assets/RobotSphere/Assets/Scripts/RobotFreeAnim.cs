using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotFreeAnim : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;
	Animator anim;

	// Use this for initialization
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}

	// Update is called once per frame
	void Update()
	{
		CheckKey();
		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
        // Walk
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
			anim.SetBool("Walk_Anim", true);
		}
		else if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Keyboard.current.aKey.isPressed)
        {
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Keyboard.current.dKey.isPressed)
        {
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

        // Close
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}

}
