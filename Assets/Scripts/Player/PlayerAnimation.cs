using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    PlayerMovement playerMovementInstance;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        playerMovementInstance = GetComponent<PlayerMovement>();
	}

    // Update is called once per frame
    void Update()
    {
        Runnning();
	}

    void Runnning()
    {
        anim.SetInteger("Speed", Mathf.RoundToInt(playerMovementInstance.horizontal));
    }
}
