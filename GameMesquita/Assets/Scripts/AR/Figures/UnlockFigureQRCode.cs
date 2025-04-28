using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockFigureQRCode : MonoBehaviour
{
    [SerializeField] int idCardToUnlock;
    [SerializeField] int idLocalToUnlock;
    [SerializeField] int idSkinToUnlock;
    
    public string cartasData;
    public string localsData;
    public string skinData;
    // Start is called before the first frame update
    void Start()
    {
        cartasData = SaveGame.Instance.GetSaveData("cartas");
        localsData = SaveGame.Instance.GetSaveData("locais");
        skinData = SaveGame.Instance.GetSaveData("skins");

        if (!cartasData.Contains(idCardToUnlock.ToString()))
        {
            SaveGame.Instance.AddToSaveData("cartas", idCardToUnlock.ToString());
            //feedbackText.text = $"Carta Obtida {idCardToUnlock}";
        }
        if (!localsData.Contains(idLocalToUnlock.ToString()))
        {
            SaveGame.Instance.AddToSaveData("locais", idLocalToUnlock.ToString());
            //feedbackText.text = $"Carta Obtida {idCardToUnlock}";
        }
        if (!skinData.Contains(idSkinToUnlock.ToString()))
        {
            SaveGame.Instance.AddToSaveData("skins", idSkinToUnlock.ToString());
            //feedbackText.text = $"Carta Obtida {idCardToUnlock}";
        }
    }
}
