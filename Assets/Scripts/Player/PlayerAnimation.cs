using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    public void PlayAnimations(float horizontalSpeed, bool jumpTakeOff,bool jumpLanding, bool onWallStick)
    {
        anim.SetInteger("Speed",(int) horizontalSpeed);

        anim.SetBool("JumpTakeOff", jumpTakeOff);

        anim.SetBool("JumpLanding", jumpLanding);

        anim.SetBool("WallStick", onWallStick);

    }
    
}
