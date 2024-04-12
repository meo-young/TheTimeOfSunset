using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{
    public string ob_name;
    private Animator anim;
    private bool chestFlag = false;
    private bool stoneFlag = false;
    GameObject chest;
    GameObject stone;
    GameObject obstacle;

    void Start()
    {
        anim = GetComponent<Animator>();
        chest = GameObject.Find("Chest");
        stone = GameObject.Find("Stone_Active");
        obstacle = GameObject.Find("Obstacle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ob_name == "chest" && chestFlag == false)
        {
            anim.SetBool("Trigger", true);
            chestFlag = true;
        }
        else if(ob_name == "stone" && stoneFlag == false)
        {
            stoneFlag = true;
            transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
            obstacle.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
