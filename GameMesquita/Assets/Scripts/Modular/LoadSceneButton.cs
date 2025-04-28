using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] string sceneToOpen;
    Button button;
    
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(LoadNewScene);
    }
    void LoadNewScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToOpen);
    }
}
