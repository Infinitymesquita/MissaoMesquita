using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class ImageGallery : MonoBehaviour
{

    public Local[] Locals;
    public Image DisplayImage;
    public int currentIndex = 0;

    public string localsData;

    void Start()
    {
        localsData = SaveGame.Instance.GetSaveData("locais");
        UpdateDisplay();
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % Locals.Length;
        UpdateDisplay();
    }

    public void PreviousImage()
    {
        currentIndex = (currentIndex - 1 + Locals.Length) % Locals.Length;
        UpdateDisplay();
    }

    public void ShowDetails()
    {
        Local selectedLocal = Locals[currentIndex];
        
        SelectLocalData.ID = selectedLocal.id;
        SelectLocalData.Description = selectedLocal.description;
        SelectLocalData.AssociatedObjectName = selectedLocal.objectToInstance.name;
    }

    private void UpdateDisplay()
    {
        if (localsData.Contains(currentIndex.ToString()))
        {
            DisplayImage.gameObject.SetActive(true);
            DisplayImage.sprite = Locals[currentIndex].image;

        }
        else
        {
            DisplayImage.gameObject.SetActive(false);
        }
        ShowDetails();
    }

    [System.Serializable]
    public class LocalData
    {
        public int ID;
        public string Description;
        public string AssociatedObject; // Nome do GameObject para instanciar posteriormente
    }
}