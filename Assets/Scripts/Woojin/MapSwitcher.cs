using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class MapSwitcher : MonoBehaviour
{
    [SerializeField] private SceneAsset _gotoScene;
    private BoxCollider2D thisCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(_gotoScene.name, LoadSceneMode.Single);
    }
}
