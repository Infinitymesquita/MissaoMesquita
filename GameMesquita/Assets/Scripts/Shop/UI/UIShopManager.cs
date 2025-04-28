using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIShopManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI booksCountText;
    public  TextMeshProUGUI feedbackText;
    public int booksCounts;
    
    public string livrosData;
    private void Start()
    {
        livrosData = SaveGame.Instance.GetSaveData("livros");
        booksCounts = int.Parse(livrosData);
        booksCountText.text = booksCounts.ToString();
    }
    void UpdateBooksCounterUI()
    {
        booksCountText.text = booksCounts.ToString();
        SaveGame.Instance.UpdateSaveData("livros", booksCountText.text);
    }
    public void AddBooks(int booksNumber)
    {
        booksCounts += booksNumber; // Acumula as moedas
        UpdateBooksCounterUI();
    }
    public void RemoveBooks(int booksNumber)
    {
        booksCounts -= booksNumber;
        UpdateBooksCounterUI();
    }
    public int GetBooksNumber()
    {
        return booksCounts;
    }
}