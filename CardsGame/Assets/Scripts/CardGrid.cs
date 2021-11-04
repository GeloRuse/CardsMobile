using System.Collections.Generic;
using UnityEngine;

public class CardGrid
{
    [SerializeField]
    private List<GameObject> _grid = new List<GameObject>(); //ячейки текущей сложности

    [SerializeField]
    private List<CardData> _cards = new List<CardData>(); //набор карточек для текущей сетки

    [SerializeField]
    private CardData _correctCard; //правильная карточка

    public List<GameObject> Grid => _grid;

    public List<CardData> Cards => _cards;

    public CardData CorrectCard => _correctCard;

    /// <summary>
    /// Создание сетки
    /// </summary>
    /// <param name="size">размер сетки</param>
    /// <param name="difficulty">сложность сетки</param>
    /// <param name="cellPrefab">Prefab ячейки</param>
    /// <param name="parent">Transform-родитель, которому принадлежат ячейки</param>
    public void GenerateGrid(int size, int difficulty, GameObject cellPrefab, Transform parent)
    {
        for (int i = 0; i < size * difficulty; i++)
        {
            _grid.Add(Object.Instantiate(cellPrefab, parent, false));
        }
    }

    /// <summary>
    /// Создание набора карточек
    /// </summary>
    /// <param name="cardBundleData">тип карточек для текущего набора</param>
    public void GenerateCards(CardBundleData cardBundleData)
    {
        List<CardData> tempCards = new List<CardData>(cardBundleData.CardData);
        for (int i = 0; i < _grid.Count; i++)
        {
            int cardIndex = Random.Range(0, tempCards.Count);
            CardData selectedCard = tempCards[cardIndex];
            tempCards.Remove(selectedCard);
            _cards.Add(selectedCard);
            if (tempCards.Count == 0)
                break;
        }
    }

    /// <summary>
    /// Создание карточки-ответа
    /// </summary>
    public void GenerateCorrectCard()
    {
        int correctIndex = Random.Range(0, _cards.Count);
        _correctCard = _cards[correctIndex];
    }

    /// <summary>
    /// Очистка сетки
    /// </summary>
    public void ClearGrid()
    {
        foreach (GameObject cell in _grid)
        {
            cell.SetActive(false);
        }
        _grid.Clear();
    }

    /// <summary>
    /// Очиста набора карточек
    /// </summary>
    public void ClearCards()
    {
        _cards.Clear();
    }
}
