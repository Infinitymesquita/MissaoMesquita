using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnableOrDisableObjects : MonoBehaviour
{
    [SerializeField] bool enableObjects;
    [SerializeField] bool disableObjects;

    [Header("ENABLE OBJECTS")]
    [SerializeField] GameObject[] gameObjectsToEnable;
    
    [Header("DISABLE OBJECTS")]
    [SerializeField] GameObject[] gameObjectsToDisable;
    
    Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(EnableOrDisableAllObjects);
    }
    public void EnableOrDisableAllObjects()
    {
        if (enableObjects)
        {
            for (int i = 0; i < gameObjectsToEnable.Length; i++)
            {
                gameObjectsToEnable[i].SetActive(true);
            }
        }
        if (disableObjects)
        {
            for (int i = 0; i < gameObjectsToDisable.Length; i++)
            {
                gameObjectsToDisable[i].SetActive(false);
            }
        }
    }
}
