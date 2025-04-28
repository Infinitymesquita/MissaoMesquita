using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardNavigator : MonoBehaviour
{
    public Image cardDisplay; // Área para mostrar a carta
    public TextMeshProUGUI priceText; // Texto para exibir o preço
    public Card[] cards; // Lista de cartas
    public int currentIndex = 0;

    public Button buyButton;
    [SerializeField] ShopManager shopManager;
    [SerializeField] UIShopManager uIShopManager;
    private void Start()
    {
        UpdateCardDisplay();
    }
    public void ShowNextCard()
    {
        if (currentIndex < cards.Length - 1)
        {
            currentIndex++;
            UpdateCardDisplay();
        }
    }

    public void ShowPreviousCard()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateCardDisplay();
        }
    }

    private void UpdateCardDisplay()
    {
        cardDisplay.sprite = cards[currentIndex].image;
        priceText.text = cards[currentIndex].price.ToString();
        uIShopManager.feedbackText.text = "";
        shopManager.CheckCardsWasPurchased();
    }
    public Card GetCurrentCard()
    {
        return cards[currentIndex];
    }
}