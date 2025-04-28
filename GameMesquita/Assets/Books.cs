using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Books : MonoBehaviour
{
    public Transform[] books;

    // Start is called before the first frame update
    void Start()
    {
        float random = Random.Range(0, books.Length);
        this.transform.Rotate(0, Random.Range(0, 360), 0);
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i].gameObject.name != random.ToString())
            {
                books[i].gameObject.SetActive(false);
            }
        }
    }
}
