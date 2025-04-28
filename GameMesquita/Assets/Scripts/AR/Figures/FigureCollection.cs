using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCollection : MonoBehaviour
{
    public List<Figure> allFigures;
    [SerializeField] Sprite[] images;
    void Start()
    {
        for (int i = 0; i < images.Length; i++)
        {
            allFigures.Add(new Figure(i, $"Figure{i}", images[i]));
        }
    }
}
