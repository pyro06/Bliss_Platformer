using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnim;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void DeathAnimation()
    {
        playerAnim.SetBool("Death",true);
    }

    public void DeathAnimationReset()
    {
        playerAnim.SetBool("Death", false);
    }

  
}
