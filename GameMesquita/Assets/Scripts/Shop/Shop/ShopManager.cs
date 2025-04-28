using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ShopManager : MonoBehaviour
{
    [Header("MANAGERS")]
    [SerializeField] CardNavigator cardNavigator; // Referência ao CardNavigator
    [SerializeField] UIShopManager uIShopManager;
    [SerializeField] OpenPackageManager openPackageManager;
    
    [Space]
    [Header("CARTAS ADQUIRIDAS")]
    public string cartasData;

    [Header("PACK INFOS")]
    public int valueForPack;

    [SerializeField] GameObject panelOpenCards;
    [SerializeField] TextMeshProUGUI feedbackText;
    private void Start()
    {
        cartasData = SaveGame.Instance.GetSaveData("cartas");
        CheckCardsWasPurchased();
    }
    public void BuyCard()
    {
        // Transformar cartasData em uma lista para verificar IDs corretamente
        List<string> cartasObtidas = new List<string>(cartasData.Split(';'));

        Card currentCard = cardNavigator.GetCurrentCard();

        // Verificar se o jogador possui moedas suficientes e se a carta não foi comprada
        if (uIShopManager.GetBooksNumber() >= currentCard.price && !cartasObtidas.Contains(currentCard.id.ToString()))
        {
            uIShopManager.RemoveBooks(currentCard.price);

            // Adicionar a carta comprada ao progresso
            SaveGame.Instance.AddToSaveData("cartas", currentCard.id.ToString());
            cartasObtidas.Add(currentCard.id.ToString());

            // Atualizar cartasData com a nova lista
            cartasData = string.Join(";", cartasObtidas);

            uIShopManager.feedbackText.text = "Carta comprada com sucesso!";
            Debug.Log("Carta comprada com sucesso!");
        }
        else if (cartasObtidas.Contains(currentCard.id.ToString()))
        {
            uIShopManager.feedbackText.text = "Carta já comprada!";
            Debug.Log("Carta já comprada!");
        }
        else
        {
            uIShopManager.feedbackText.text = "Moedas insuficientes!";
            Debug.Log("Moedas insuficientes!");
        }

        CheckCardsWasPurchased();
    }
    public void BuyPack()
    {
        if (uIShopManager.GetBooksNumber() >= valueForPack)
        {
            panelOpenCards.SetActive(true);
            uIShopManager.RemoveBooks(valueForPack);
            openPackageManager.OpenPackage();
        }
        else
        {
            feedbackText.text = "Moedas insuficientes!";
        }
    }
    public void CheckCardsWasPurchased()
    {
        // Transformar cartasData em uma lista para verificar IDs corretamente
        List<string> cartasObtidas = new List<string>(cartasData.Split(';'));

        Card currentCard = cardNavigator.GetCurrentCard();

        // Verificar se o ID da carta atual está na lista
        if (cartasObtidas.Contains(currentCard.id.ToString()))
        {
            cardNavigator.buyButton.gameObject.SetActive(false);
            uIShopManager.feedbackText.text = "Carta já comprada!";
        }
        else
        {
            cardNavigator.buyButton.gameObject.SetActive(true);
            uIShopManager.feedbackText.text = ""; // Limpar o texto, se necessário
        }

    }
}
