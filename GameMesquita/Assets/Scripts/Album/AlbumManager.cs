using System.Collections;
using System.Collections.Generic;
using System.Linq; // Para ordenar
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    public Image[] cardSlots; // Os 6 slots para exibir as cartas
    public Button leftButton, rightButton; // Botões de navegação
    public Card[] allCards; // Todas as cartas disponíveis no jogo
    private List<Card> userCards = new List<Card>(); // Cartas que o usuário possui
    private int currentPage = 0; // Página atual
    private int cardsPerPage = 6; // Número de slots por página
    public string cardsData; // Dados das cartas no formato "0;11;24;12;5;20"

    public SelectCard selectCard; // Referência ao script SelectCard

    void Start()
    {
        // Carregar os IDs das cartas que o usuário possui
        cardsData = SaveGame.Instance.GetSaveData("cartas");
        LoadUserCards();

        // Inicializar o dicionário no SelectCard
        selectCard.InitializeCards(allCards);

        // Configurar os botões
        leftButton.onClick.AddListener(() => ChangePage(-1));
        rightButton.onClick.AddListener(() => ChangePage(1));

        // Atualizar a exibição inicial
        UpdateCardDisplay();
    }

    void ChangePage(int direction)
    {
        // Calcula o número total de páginas
        int totalPages = Mathf.CeilToInt((float)allCards.Length / cardsPerPage);

        // Atualiza o índice da página atual, com limites
        currentPage = Mathf.Clamp(currentPage + direction, 0, totalPages - 1);

        // Atualiza a exibição das cartas
        UpdateCardDisplay();
    }

    void UpdateCardDisplay()
    {
        // Atualiza os slots de cartas
        for (int i = 0; i < cardSlots.Length; i++)
        {
            int cardIndex = currentPage * cardsPerPage + i;

            if (cardIndex < allCards.Length)
            {
                Card card = allCards[cardIndex];

                // Verifica se o usuário possui a carta
                if (userCards.Exists(c => c.id == card.id))
                {
                    // Mostra a carta se o usuário possui
                    cardSlots[i].sprite = card.image;
                    cardSlots[i].gameObject.SetActive(true);

                    // Adiciona o evento de clique para exibir detalhes
                    int cardId = card.id;
                    Button cardButton = cardSlots[i].GetComponent<Button>();
                    cardButton.onClick.RemoveAllListeners(); // Remove eventos anteriores
                    cardButton.onClick.AddListener(() => selectCard.ShowCardDetails(cardId));
                }
                else
                {
                    // Mostra um slot vazio se o usuário não possui a carta
                    cardSlots[i].sprite = null; // Opcional: coloque uma imagem "vazia" aqui
                    cardSlots[i].gameObject.SetActive(false);
                }
            }
            else
            {
                // Desativa o slot se não houver carta para esse índice
                cardSlots[i].gameObject.SetActive(false);
            }
        }

        // Atualizar estado dos botões de navegação
        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        // Total de páginas disponíveis
        int totalPages = Mathf.CeilToInt((float)allCards.Length / cardsPerPage);

        // Desativa o botão esquerdo se estiver na primeira página
        leftButton.interactable = currentPage > 0;

        // Desativa o botão direito se estiver na última página
        rightButton.interactable = currentPage < totalPages - 1;
    }

    void LoadUserCards()
    {
        // Limpa a lista de cartas do usuário
        userCards.Clear();

        // Carrega os IDs das cartas do usuário
        string[] idStrings = cardsData.Split(';');

        // Adiciona as cartas que correspondem aos IDs carregados
        foreach (string idStr in idStrings)
        {
            if (int.TryParse(idStr, out int cardId))
            {
                Card card = System.Array.Find(allCards, c => c.id == cardId);
                if (card != null)
                {
                    userCards.Add(card);
                }
            }
        }
    }
}