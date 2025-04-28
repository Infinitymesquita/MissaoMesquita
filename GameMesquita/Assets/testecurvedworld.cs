using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testecurvedworld : MonoBehaviour
{
    public GameManagerScript gameManagerScript;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.transform.position.z >= 20)
        {
            this.transform.position = new Vector3(-2.38f, 0.6f, -480f);
        }
        speed = gameManagerScript.speed;
        this.transform.localPosition += Vector3.forward * speed * Time.deltaTime;
    }
}
