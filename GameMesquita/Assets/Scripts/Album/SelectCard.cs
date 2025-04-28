using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    public Image cardImage; // Imagem expandida da carta
    public TextMeshProUGUI cardDescription; // Texto da descri��o da carta
    public GameObject cardDetailsPanel; // Painel para exibir os detalhes

    private Dictionary<int, Card> cardDictionary; // Dicion�rio para mapear IDs �s cartas

    public void InitializeCards(Card[] allCards)
    {
        if (cardDictionary == null)
        {
            cardDictionary = new Dictionary<int, Card>(); // Inicializa o dicion�rio
        }

        foreach (Card card in allCards)
        {
            if (!cardDictionary.ContainsKey(card.id))
            {
                cardDictionary[card.id] = card; // Popula o dicion�rio com os IDs e cartas
            }
        }
    }

    public void ShowCardDetails(int cardId)
    {
        if (cardDictionary.TryGetValue(cardId, out Card card))
        {
            cardImage.sprite = card.image; // Exibe a imagem
            cardDescription.text = card.description; // Exibe a descri��o
            cardDetailsPanel.SetActive(true); // Mostra o painel
        }
        else
        {
            Debug.LogError($"Carta com ID {cardId} n�o encontrada!");
        }
    }

    public void CloseCardDetails()
    {
        cardDetailsPanel.SetActive(false); // Esconde o painel
    }
}