using System.Collections;
using System.Collections.Generic;
using System.Linq; // Para ordenar
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    public Image[] cardSlots; // Os 6 slots para exibir as cartas
    public Button leftButton, rightButton; // Bot�es de navega��o
    public Card[] allCards; // Todas as cartas dispon�veis no jogo
    private List<Card> userCards = new List<Card>(); // Cartas que o usu�rio possui
    private int currentPage = 0; // P�gina atual
    private int cardsPerPage = 6; // N�mero de slots por p�gina
    public string cardsData; // Dados das cartas no formato "0;11;24;12;5;20"

    public SelectCard selectCard; // Refer�ncia ao script SelectCard

    void Start()
    {
        // Carregar os IDs das cartas que o usu�rio possui
        cardsData = SaveGame.Instance.GetSaveData("cartas");
        LoadUserCards();

        // Inicializar o dicion�rio no SelectCard
        selectCard.InitializeCards(allCards);

        // Configurar os bot�es
        leftButton.onClick.AddListener(() => ChangePage(-1));
        rightButton.onClick.AddListener(() => ChangePage(1));

        // Atualizar a exibi��o inicial
        UpdateCardDisplay();
    }

    void ChangePage(int direction)
    {
        // Calcula o n�mero total de p�ginas
        int totalPages = Mathf.CeilToInt((float)allCards.Length / cardsPerPage);

        // Atualiza o �ndice da p�gina atual, com limites
        currentPage = Mathf.Clamp(currentPage + direction, 0, totalPages - 1);

        // Atualiza a exibi��o das cartas
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

                // Verifica se o usu�rio possui a carta
                if (userCards.Exists(c => c.id == card.id))
                {
                    // Mostra a carta se o usu�rio possui
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
                    // Mostra um slot vazio se o usu�rio n�o possui a carta
                    cardSlots[i].sprite = null; // Opcional: coloque uma imagem "vazia" aqui
                    cardSlots[i].gameObject.SetActive(false);
                }
            }
            else
            {
                // Desativa o slot se n�o houver carta para esse �ndice
                cardSlots[i].gameObject.SetActive(false);
            }
        }

        // Atualizar estado dos bot�es de navega��o
        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        // Total de p�ginas dispon�veis
        int totalPages = Mathf.CeilToInt((float)allCards.Length / cardsPerPage);

        // Desativa o bot�o esquerdo se estiver na primeira p�gina
        leftButton.interactable = currentPage > 0;

        // Desativa o bot�o direito se estiver na �ltima p�gina
        rightButton.interactable = currentPage < totalPages - 1;
    }

    void LoadUserCards()
    {
        // Limpa a lista de cartas do usu�rio
        userCards.Clear();

        // Carrega os IDs das cartas do usu�rio
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