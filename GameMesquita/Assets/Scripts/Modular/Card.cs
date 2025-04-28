using UnityEngine;
[System.Serializable]
public class Card
{
    public int id;
    public Sprite image;
    public int price;
    [Multiline] public string description;
}