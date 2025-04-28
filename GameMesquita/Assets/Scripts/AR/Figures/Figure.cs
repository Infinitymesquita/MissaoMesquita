using UnityEngine;

public class Figure : MonoBehaviour
{
    public int id;
    public new string name;
    public Sprite image;  // Imagem da figurinha

    public Figure(int id, string nome, Sprite imagem)
    {
        this.id = id;
        this.name = nome;
        this.image = imagem;
    }
}
