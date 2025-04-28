using UnityEngine;
using TMPro;

public class ARSceneManager : MonoBehaviour
{
    public TMP_Text DescriptionText; // Arraste um TextMeshPro UI aqui
    public Transform ARAnchor; // Ponto onde o objeto será instanciado
    public GameObject[] Prefabs; // Lista de prefabs para instanciar

    void Start()
    {
        // Configurar o texto da descrição
        DescriptionText.text = SelectLocalData.Description;

        // Encontrar e instanciar o objeto
        foreach (GameObject prefab in Prefabs)
        {
            if (prefab.name == SelectLocalData.AssociatedObjectName)
            {
                Instantiate(prefab, ARAnchor.position, ARAnchor.rotation, ARAnchor);
                break;
            }
        }
    }
}