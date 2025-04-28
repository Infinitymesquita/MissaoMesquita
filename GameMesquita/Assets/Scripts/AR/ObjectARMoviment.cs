using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectARMoviment : MonoBehaviour
{
    Vector2 positionFinger;
    public GameObject arObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if(touch.fingerId == 0)
                {
                    positionFinger = touch.position;
                }
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                if(touch.fingerId == 0)
                {
                    arObject.transform.Rotate(positionFinger - touch.position * Time.deltaTime);
                }
            }
        }
    }
}
