using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private bool canMove = true;
    private Animator anim;
    private Vector3 vector;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetFloat("DirY", 0);
            anim.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetFloat("DirX", 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetFloat("DirX", -1);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Running", true);
            }
            else
            {
                anim.SetBool("Running", false);
            }
            

            yield return new WaitForSeconds(0.1f);
        }
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if(Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
