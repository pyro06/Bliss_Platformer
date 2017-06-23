using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void ResetJump()
    {
        anim.ResetTrigger("Jump");
    }

    public void JumpInAir()
    {
        anim.SetBool("Landing", false);
    }

    public void JumpLand()
    {
        anim.SetBool("Landing", true);
    }
}
