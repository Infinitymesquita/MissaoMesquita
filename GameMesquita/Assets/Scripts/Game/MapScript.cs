using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private GameManagerScript gameManagerScript;
    [SerializeField] private GameObject ColisorEstrada;
    public float speed = 4;
    private void Awake()
    {
        gameManagerScript = FindObjectOfType<GameManagerScript>();
    }
    void FixedUpdate()
    {
        speed = gameManagerScript.speed;
        // se move em direçao a camera e depois  que fica atras dela se destroy
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

        if (transform.position.z >= 80)
        {
            gameManagerScript.DisableMap(this.gameObject);
        }
    }
    
}
