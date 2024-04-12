using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : PlayerManager
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    

    void Update()
    {
        if (jumpChargeFlag)
            anim.SetBool("Charging", true);
        else
            anim.SetBool("Charging", false);

        if (jumpFlag)
            anim.SetBool("Jumping", true);
        else
            anim.SetBool("Jumping", false);

        if (directionFlag == "RIGHT")
        {
            anim.SetBool("Walking", true);
            anim.SetFloat("DirX", 1f);
        }
        else if (directionFlag == "LEFT")
        {
            anim.SetBool("Walking", true);
            anim.SetFloat("DirX", -1f);
        }
        else
            anim.SetBool("Walking", false);
    }
}
