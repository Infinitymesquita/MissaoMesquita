using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenPackageManager : MonoBehaviour
{
    [Header("CARTAS PARA SORTEAR")]
    public Card[] cards;
    [Space]
    
    [Header("CARTAS SORTEADAS")]
    [SerializeField] Image[] spriteFiguresPackage;  // Referência ao componente Image no Canvas
    [SerializeField] GameObject[] feedbackFigures;
    [SerializeField] TextMeshProUGUI[] textMeshProUGUIs;
    
    [Header("CARTAS SORTEADAS")]
    [SerializeField] int cardsRepeatedValue;
    [SerializeField] int cardsPerPack;
    [Space]

    [Header("MANAGERS")]
    [SerializeField] ShopManager shopManager;
    [SerializeField] UIShopManager uIShopManager;

    public void OpenPackage()
    {
        ResetFigures();

        // Obter a lista atualizada de cartas
        List<string> cartasObtidas = new List<string>(shopManager.cartasData.Split(';'));

        for (int i = 0; i < cardsPerPack; i++)
        {
            int index = Random.Range(0, cards.Length);
            Card sorteada = cards[index];

            // Atualizar a imagem da carta sorteada no UI
            spriteFiguresPackage[i].sprite = sorteada.image;

            // Verificar se a carta já foi obtida usando a lista
            if (cartasObtidas.Contains(sorteada.id.ToString()))
            {
                // Carta repetida
                textMeshProUGUIs[i].text = cardsRepeatedValue.ToString();
                feedbackFigures[i].SetActive(true);

                Debug.Log("Figurinha repetida: " + sorteada.id + " - Você ganhou 10 moedas!");
                uIShopManager.AddBooks(cardsRepeatedValue);
            }
            else
            {
                // Carta nova: Adicionar à lista e salvar o progresso
                cartasObtidas.Add(sorteada.id.ToString());
                SaveGame.Instance.AddToSaveData("cartas", sorteada.id.ToString());

                Debug.Log("Você obteve a carta " + sorteada.id);
            }
        }

        // Atualizar os dados no shopManager
        shopManager.cartasData = string.Join(";", cartasObtidas);
    }
    void ResetFigures()
    {
        for (int i = 0; i < feedbackFigures.Length; i++)
        {
            feedbackFigures[i].SetActive(false);
            textMeshProUGUIs[i].text = ""; // Limpar o texto para evitar inconsistências
        }
    }
}
