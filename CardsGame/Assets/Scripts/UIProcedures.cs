using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIProcedures : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton; //кнопка перезапуска

    [SerializeField]
    private CanvasGroup dimScreen; //затемняющий экран

    [SerializeField]
    private CanvasGroup taskGroup; //объект задачи

    [SerializeField]
    private string taskText = "Find"; //текст задачи

    [SerializeField]
    private CanvasGroup loadScreen; //загрузочный экран


    /// <summary>
    /// Запуск игры
    /// </summary>
    private void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(DOTweenProcedures.FadeOutGroup(loadScreen));
        seq.Join(DOTweenProcedures.CustomFadeGroup(dimScreen, 0f, 0f));
        dimScreen.gameObject.SetActive(false);
        seq.Join(DOTweenProcedures.CustomFadeGroup(taskGroup, 0f, 0f));
        seq.Join(DOTweenProcedures.FadeInGroup(taskGroup));
    }

    /// <summary>
    /// Назначение задачи
    /// </summary>
    /// <param name="text">правильный ответ</param>
    public void SetTask(string text)
    {
        taskGroup.GetComponent<Text>().text = taskText+" "+ text;
    }

    /// <summary>
    /// Завершение игры с затемнением и отображением кнопки перезапуска
    /// </summary>
    public void EndGame()
    {
        dimScreen.gameObject.SetActive(true);
        DOTweenProcedures.CustomFadeGroup(dimScreen, .5f, 1f);
        restartButton.SetActive(true);
    }

    /// <summary>
    /// Перезапуск игры с загрузочным экраном
    /// </summary>
    public void RestartGame()
    {
        DOTweenProcedures.FadeInGroup(loadScreen).OnComplete(()=>
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        });
    }
}
