using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    void Update()
    {
        gameObject.transform.eulerAngles += new Vector3(0, rotateSpeed * Time.deltaTime, 0);    
    }
}
