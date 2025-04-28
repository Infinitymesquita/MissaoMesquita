using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    public Image cardImage; // Imagem expandida da carta
    public TextMeshProUGUI cardDescription; // Texto da descrição da carta
    public GameObject cardDetailsPanel; // Painel para exibir os detalhes

    private Dictionary<int, Card> cardDictionary; // Dicionário para mapear IDs às cartas

    public void InitializeCards(Card[] allCards)
    {
        if (cardDictionary == null)
        {
            cardDictionary = new Dictionary<int, Card>(); // Inicializa o dicionário
        }

        foreach (Card card in allCards)
        {
            if (!cardDictionary.ContainsKey(card.id))
            {
                cardDictionary[card.id] = card; // Popula o dicionário com os IDs e cartas
            }
        }
    }

    public void ShowCardDetails(int cardId)
    {
        if (cardDictionary.TryGetValue(cardId, out Card card))
        {
            cardImage.sprite = card.image; // Exibe a imagem
            cardDescription.text = card.description; // Exibe a descrição
            cardDetailsPanel.SetActive(true); // Mostra o painel
        }
        else
        {
            Debug.LogError($"Carta com ID {cardId} não encontrada!");
        }
    }

    public void CloseCardDetails()
    {
        cardDetailsPanel.SetActive(false); // Esconde o painel
    }
}