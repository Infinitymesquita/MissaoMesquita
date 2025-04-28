using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RotateSprite : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    [SerializeField] float speed;

    private void Start()
    {
    
    }
    void Update()
    {
        transform.Rotate(axis, speed);
    }
}
