using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap : MonoBehaviour
{
    private PlayerManager thePlayer;
    private CameraMoving theCamera;
    private BoxCollider2D targetBound;
    public string map1_name;
    public string map2_name;
    public BoxCollider2D map1_Bound;
    public BoxCollider2D map2_Bound;
    void Start()
    {
        theCamera = FindObjectOfType<CameraMoving>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(thePlayer.currentMapName == map1_name)
            {
                thePlayer.currentMapName = map2_name;
                targetBound = map2_Bound;
            }
            else
            {
                thePlayer.currentMapName = map1_name;
                targetBound = map1_Bound;
            }
            theCamera.SetBound(targetBound);
        }
    }
}
