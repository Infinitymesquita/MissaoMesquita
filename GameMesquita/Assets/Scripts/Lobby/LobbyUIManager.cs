using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class LobbyUIManager : MonoBehaviour
{
    [Header("FEEDBACK TEXTS")]
    [SerializeField] TextMeshProUGUI booksCountingText;
    [SerializeField] TextMeshProUGUI cardsCountingText;
    [SerializeField] TextMeshProUGUI localsCountingText;

    string  cardsData;
    string  booksData;
    string localsData;
    
    [SerializeField] bool displayBooksCounting;
    [SerializeField] bool displayCardsCounting;
    [SerializeField] bool displayLocalsCounting;

    public int totalCardsinGame;
    public int totalLocalsinGame;

    void Start()
    {
        cardsData = SaveGame.Instance.GetSaveData("cartas");
        booksData = SaveGame.Instance.GetSaveData("livros");
        localsData = SaveGame.Instance.GetSaveData("locais");

        string[] cartasArray = cardsData.Split(';').Where(s => !string.IsNullOrEmpty(s)).ToArray();
        string[] localsArray = localsData.Split(';').Where(s => !string.IsNullOrEmpty(s)).ToArray();

       
        int totalCards = cartasArray.Length;
        int totalLocals = localsArray.Length;
        int totalBooks = int.Parse(booksData);
        
        if (displayBooksCounting)
        {
            booksCountingText.text = totalBooks.ToString();
        }
        if (displayCardsCounting)
        {
            cardsCountingText.text = $"{totalCards.ToString()}/{totalCardsinGame}"; 
        }
        if (displayLocalsCounting)
        {
            localsCountingText.text = $"Locais:{totalLocals.ToString()}/{totalLocalsinGame}";
        }
        
    }

 
}
