using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridProcedures : MonoBehaviour
{
    [SerializeField]
    private CardBundleData[] cardTypes; //типы карточек

    [SerializeField]
    private GameObject cellPrefab; //Prefab ячейки

    [SerializeField]
    private string transformName = "Button"; //название объекта карточки

    [SerializeField]
    private Transform gridPanel; //Transform сетки

    [SerializeField]
    private ParticleSystem prts; //частицы, появляющиеся при выборе правильного ответа

    [SerializeField]
    private int gridSize = 3; //размер сетки

    [SerializeField]
    private int difficulty = 1; //сложность

    [SerializeField]
    private int finalDifficulty = 3; //максимальная сложность

    [SerializeField]
    private UnityEvent endGame; //событие завершения игры

    private CardGrid cardGrid; //сетка ячеек с карточками

    [SerializeField]
    private UIProcedures uiProcedures; //процедуры UI

    /// <summary>
    /// Начальная настройка сетки с эффектом Bounce
    /// </summary>
    private void Start()
    {
        uiProcedures = GetComponent<UIProcedures>();
        GenerationProcedure();
        BounceCards();
    }

    /// <summary>
    /// Процедура создания сетки с карточками
    /// </summary>
    private void GenerationProcedure()
    {
        cardGrid = new CardGrid();
        cardGrid.GenerateGrid(gridSize, difficulty, cellPrefab, gridPanel);
        int dataSet = Random.Range(0, cardTypes.Length);
        cardGrid.GenerateCards(cardTypes[dataSet]);
        cardGrid.GenerateCorrectCard();
        AssignCardData(cardGrid);
        uiProcedures.SetTask(cardGrid.CorrectCard.Identifier);
    }

    /// <summary>
    /// Процедура очистки сетки
    /// </summary>
    private void CleanupProcedure()
    {
        cardGrid.ClearGrid();
        cardGrid.ClearCards();
    }

    /// <summary>
    /// Назначение карточек ячейкам
    /// </summary>
    /// <param name="selectedCardGrid">сетка ячеек с карточками</param>
    private void AssignCardData(CardGrid selectedCardGrid)
    {
        for (int i = 0; i < selectedCardGrid.Grid.Count; i++)
        {
            Transform prefabButton = selectedCardGrid.Grid[i].transform.Find(transformName);
            CardData tempCardData = selectedCardGrid.Cards[i];
            prefabButton.GetComponent<Image>().sprite = tempCardData.Sprite;
            prefabButton.GetComponent<Button>().onClick.AddListener(delegate { CheckCorrect(tempCardData, prefabButton); });
        }
    }

    /// <summary>
    /// Сравнение выбранной карточки с правильным ответом
    /// </summary>
    /// <param name="card">выбранная карточка</param>
    /// <param name="button">Transform карточки</param>
    private void CheckCorrect(CardData card, Transform button)
    {
        //если идентификаторы совпадают
        if (card.Identifier.Equals(cardGrid.CorrectCard.Identifier))
        {
            CorrectMatch(button);
        }
        else
        {
            WrongMatch(button);
        }
    }

    /// <summary>
    /// Выбрана правильная карточка
    /// </summary>
    /// <param name="button">карточка</param>
    private void CorrectMatch(Transform button)
    {
        PlayParticles(button); //запуск звездочек
        difficulty++; //увеличение сложности
        //если пройдены все уровни
        if (difficulty > finalDifficulty)
            endGame.Invoke(); //завершение игры
        else
        {
            //очистка и генерация сетки более высокой сложности
            CleanupProcedure();
            GenerationProcedure();
        }
    }

    /// <summary>
    /// Выбрана неправильная карточка
    /// </summary>
    /// <param name="button">карточка</param>
    private void WrongMatch(Transform button)
    {
        DOTweenProcedures.ShakeTransform(button); //дергание карточки с неправильным ответом
    }

    /// <summary>
    /// Запуск звездочек
    /// </summary>
    /// <param name="target">место запуска</param>
    private void PlayParticles(Transform target)
    {
        prts.transform.position = target.position;
        prts.Play();
    }

    /// <summary>
    /// Процедура запуска эффекта Bounce для сетки
    /// </summary>
    /// <param name="grid">сетка</param>
    public void BounceCards()
    {
        foreach (GameObject go in cardGrid.Grid)
        {
            DOTweenProcedures.BounceTransform(go.transform);
        }
    }
}
